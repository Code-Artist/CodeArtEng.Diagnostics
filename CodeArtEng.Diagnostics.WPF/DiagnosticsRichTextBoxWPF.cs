using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace CodeArtEng.Diagnostics.Controls
{
    public class DiagnosticsRichTextBox : RichTextBox, INotifyPropertyChanged
    {
        private static readonly object LockObject = new object();
        private string MessageBuffer;

        private ContextMenu contextMenu;
        private MenuItem menuItemEnabled;
        private MenuItem menuItemClear;
        private MenuItem menuItemSaveToFile;
        private MenuItem menuItemCopyAll;
        private MenuItem menuItemCopySelected;
        private DispatcherTimer refreshTimer;
        private TraceLogger Tracer;
        private TraceFileWriter OutputFileWriter;

        /// <summary>
        /// Cleanup resources
        /// </summary>
        ~DiagnosticsRichTextBox()
        {
            refreshTimer.IsEnabled = false;
            Tracer.Enabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticsTextBox"/> class.
        /// </summary>
        public DiagnosticsRichTextBox()
        {
            InitializeComponent();

            // Default control property

            //NOTE: WPF TextBox always wraps text in RichTextBox
            //https://stackoverflow.com/questions/1368047/c-wpf-disable-text-wrap-of-richtextbox
            //https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.richtextbox?view=windowsdesktop-8.0&redirectedfrom=MSDN
            //TextWrapping = TextWrapping.NoWrap;
            VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            DisplayBufferSize = 0;

            AcceptsReturn = false;
            IsReadOnly = true;
            Width = Height = 100;
            MessageBuffer = "";

            OutputFileWriter = new TraceFileWriter();

            // Setup listener
            Tracer = new TraceLogger(Tracer_OnWriteMessage, Tracer_OnFlush, Tracer_OnMessageReceived);
            ConfigureFileWritter();
        }

        private static bool IsInDesignMode(DependencyObject element)
        {
            return DesignerProperties.GetIsInDesignMode(element);
        }

        #region [ Hide Base Class Property ]

        [Browsable(false)]
        public new bool AcceptsReturn { get; set; }

        [Browsable(false)]
        public new bool AcceptsTab { get; set; }

        [Browsable(false)]
        public new bool ReadOnly { get; set; }

        [Browsable(false)]
        public new bool Multiline { get; set; } = true;
        #endregion

        #region [ Theme ]

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // Update theme if appearance-related properties change
            if (e.Property == ForegroundProperty ||
                e.Property == BackgroundProperty ||
                e.Property == FontFamilyProperty ||
                e.Property == FontSizeProperty ||
                e.Property == FontWeightProperty)
            {
                if (!IsLoaded) return; // Skip these properties until control is fully loaded.
                ThemeValue = TextBoxTheme.UserDefined;
            }
        }

        private bool UpdatingTheme = false;
        private TextBoxTheme ThemeValue = TextBoxTheme.Windows;

        /// <summary>
        /// Get or set Diagnostic TextBox Theme. <see cref="TextBoxTheme"/>
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(TextBoxTheme), "Windows")]
        [Description("Get or set Diagnostic TextBox Theme.")]
        public TextBoxTheme Theme
        {
            get => ThemeValue;
            set
            {
                UpdatingTheme = true;
                try
                {
                    ThemeValue = this.SetTheme(value);
                    OnPropertyChanged(nameof(Theme));
                }
                finally { UpdatingTheme = false; }
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Enable / Disable trace listener to capture message from trace source.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("ListenerEnabled")]
        [Description("Enable / Disable trace listener to capture message from trace source.")]
        [DefaultValue(true)]
        public bool ListenerEnabled
        {
            get { return Tracer.Enabled; }
            set
            {
                Tracer.Enabled = value;
                ConfigureFileWritter();
                OnPropertyChanged(nameof(ListenerEnabled));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether control should response to Flush() command from trace source.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("AutoFlushEnabled")]
        [Description("Indicate whether control should response to Flush() command from trace source.")]
        [DefaultValue(true)]
        public bool FlushEnabled
        {
            get => _FlushEnabled;
            set
            {
                _FlushEnabled = value;
                OnPropertyChanged(nameof(FlushEnabled));
            }
        }
        private bool _FlushEnabled;

        /// <summary>
        /// Enable / Disable printing time stamp in front of each message.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("ShowTimeStamp")]
        [Description("Append time stamp in front of each log message.")]
        [DefaultValue(false)]
        public bool ShowTimeStamp
        {
            get { return Tracer.ShowTimeStamp; }
            set
            {
                Tracer.ShowTimeStamp = value;
                OutputFileWriter.ShowTimeStamp = value;
                OnPropertyChanged(nameof(ShowTimeStamp));
            }
        }

        /// <summary>
        /// Define time stamp string format when <see cref="TimeStampStyle"/> set as <see cref="TraceTimeStampStyle.DateTimeString"/> 
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("TimeStampFormat")]
        [Description("Define time stamp string format when TimeStampStyle set as DateTimeString")]
        public string TimeStampFormat
        {
            get { return Tracer.TimeStampFormat; }
            set
            {
                Tracer.TimeStampFormat = value;
                OutputFileWriter.TimeStampFormat = value;
                OnPropertyChanged(TimeStampFormat);
            }
        }

        /// <summary>
        /// Define time stamp style.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("TimeStampStyle")]
        [Description("Define time stamp style")]
        [DefaultValue(TraceTimeStampStyle.DateTimeString)]
        public TraceTimeStampStyle TimeStampStyle
        {
            get { return Tracer.TimeStampStyle; }
            set
            {
                Tracer.TimeStampStyle = value;
                OutputFileWriter.TimeStampStyle = value;
            }
        }

        /// <summary>
        /// Define number of lines to be keep in text box. Set to 0 to keep all lines.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("DisplayBufferSize")]
        [Description("Define how many lines of logs to keep in text box. Set to 0 to keep all lines.")]
        [DefaultValue(0)]
        public int DisplayBufferSize { get; set; }

        /// <summary>
        /// Define how often the control shall be updated. Value in ms.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("Refresh Interval (ms).")]
        [Description("Define how often the control shall be updated")]
        [DefaultValue(10)]
        public int RefreshInterval
        {
            get => refreshTimer.Interval.Milliseconds;
            set
            {
                refreshTimer.Interval = TimeSpan.FromMilliseconds(value);
                OnPropertyChanged(nameof(RefreshInterval));
            }
        }

        private void Tracer_OnWriteMessage(string message)
        {
            if (IsInDesignMode(this)) return;
            lock (LockObject)
            {
                MessageBuffer += message;
            }
        }

        private void Tracer_OnFlush()
        {
            if (FlushEnabled) Document.Blocks.Clear();
        }

        private void Tracer_OnMessageReceived(ref string message)
        {
            if (MessageReceived != null)
            {
                TextEventArgs eArg = new TextEventArgs() { Message = message };
                MessageReceived.Invoke(this, eArg);
                message = eArg.Message;
            }
        }

        /// <summary>
        /// Occur when message is received by Trace Listener.
        /// </summary>
        public event EventHandler<TextEventArgs> MessageReceived;

        private void InitializeComponent()
        {
            // Initialize Context Menu
            contextMenu = new ContextMenu();
            menuItemEnabled = new MenuItem() { Header = "Enabled" };
            menuItemClear = new MenuItem() { Header = "Clear" };
            menuItemSaveToFile = new MenuItem() { Header = "Save to File..." };
            menuItemCopySelected = new MenuItem() { Header = "Copy Selected Text" };
            menuItemCopyAll = new MenuItem() { Header = "Copy All" };

            contextMenu.Items.Add(menuItemEnabled);
            contextMenu.Items.Add(menuItemClear);
            contextMenu.Items.Add(menuItemSaveToFile);
            contextMenu.Items.Add(menuItemCopySelected);
            contextMenu.Items.Add(menuItemCopyAll);

            this.ContextMenu = contextMenu;

            // Setup event handlers
            contextMenu.Opened += ContextMenu_Opened;
            menuItemEnabled.Click += MenuItemEnabled_Click;
            menuItemClear.Click += MenuItemClear_Click;
            menuItemSaveToFile.Click += MenuItemSaveToFile_Click;
            menuItemCopySelected.Click += MenuItemCopySelected_Click;
            menuItemCopyAll.Click += MenuItemCopyAll_Click;

            // Initialize refresh timer
            refreshTimer = new DispatcherTimer();
            refreshTimer.Interval = TimeSpan.FromMilliseconds(50);
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();

            this.IsVisibleChanged += DiagnosticsTextBox_IsVisibleChanged;
        }

        private void DiagnosticsTextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ListenerEnabled = !IsInDesignMode(this);
        }

        private void MenuItemEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListenerEnabled = !ListenerEnabled;
        }

        private void MenuItemClear_Click(object sender, RoutedEventArgs e)
        {
            Document.Blocks.Clear();
        }

        private void MenuItemSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(dlg.FileName, this.Text);
            }
        }

        private void MenuItemCopyAll_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Text);
        }

        private void MenuItemCopySelected_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedText))
                Clipboard.SetText(SelectedText);
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            menuItemEnabled.IsChecked = ListenerEnabled;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            lock (LockObject)
            {
                if (MessageBuffer.Length == 0) return;

                AppendText(MessageBuffer);
                MessageBuffer = "";

                if (DisplayBufferSize <= 0) return;

                var lines = Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                if (lines.Length > DisplayBufferSize)
                {
                    string[] data = new string[DisplayBufferSize];
                    Array.Copy(lines, lines.Length - DisplayBufferSize, data, 0, DisplayBufferSize);
                    //Text = string.Join(Environment.NewLine, data);
                }
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            ScrollToEnd();
            FormatText();
        }

        private string Text => new TextRange(Document.ContentStart, Document.ContentEnd).Text;
        private string SelectedText => new TextRange(Selection.Start, Selection.End).Text;

        #region [ File Writer ]

        /// <summary>
        /// Output trace to file. Set to a valid path to enable this option.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("OutputFile")]
        [Description("Configure output file for traces. Set WriteToFile property to true to enable trace output to file.")]
        [DefaultValue("")]
        public string OutputFile
        {
            get { return OutputFileWriter.OutputFile; }
            set
            {
                OutputFileWriter.OutputFile = value;
                OnPropertyChanged(nameof(OutputFile));
            }
        }

        /// <summary>
        /// Configure secondary output path for trace output as backup when failed to write to OutputFile.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("OutputFileBackup")]
        [Description("Configure secondary output path for trace output as backup when failed to write to OutputFile. Recommend to use local path which is guaranteed to be available.")]
        [DefaultValue("")]
        public string OutputFileBackup
        {
            get { return OutputFileWriter.BackupOutputFile; }
            set
            {
                OutputFileWriter.BackupOutputFile = value;
                OnPropertyChanged(nameof(OutputFileBackup));
            }
        }

        private bool _WriteToFile = false;
        /// <summary>
        /// Set this flag to true to enable writing log to OutputFile.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("WriteToFile")]
        [Description("Enable / Disable trace output to file.")]
        [DefaultValue(false)]
        public bool WriteToFile
        {
            get { return _WriteToFile; }
            set
            {
                _WriteToFile = value;
                ConfigureFileWritter();
                OnPropertyChanged(nameof(WriteToFile));
            }
        }

        private void ConfigureFileWritter()
        {
            OutputFileWriter.ListenerEnabled = (ListenerEnabled && _WriteToFile);
        }

        #endregion

        #region [ Syntax Highlighting ]

        private Dictionary<string, Color> SyntaxTable = new Dictionary<string, Color>();

        /// <summary>
        /// Add formatting rule to set font color based on matching string.
        /// </summary>
        /// <param name="containString"></param>
        /// <param name="color"></param>
        public void AddFormattingRule(string containString, Color color)
        {
            if (SyntaxTable.ContainsKey(containString)) return;
            SyntaxTable.Add(containString, color);
        }

        /// <summary>
        /// Clear all formatting rule
        /// </summary>
        public void ClearFormattingRule() { SyntaxTable.Clear(); }

        /// <summary>
        /// When enabled, reset format to default font color for next line if no matching found. 
        /// Otherwise, apply last font color.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("Auto Reset Format")]
        [Description("When enabled, reset format to default font color for next line if no matching found. Otherwise, apply last font color. Use AddFormattingRule() to define font color rule.")]
        [DefaultValue(true)]
        public bool AutoResetFormat { get; set; } = true;
        private Color LastFontColor;
        private bool Updating = false;
        private int LastSelection = 0;

        private void FormatText()
        {
            if (Updating) return;
            Updating = true;
            try
            {
                // Remove extra spacing between paragraphs
                foreach (Block block in Document.Blocks)
                {
                    if (block is Paragraph paragraph)
                    {
                        paragraph.Margin = new Thickness(0);
                    }
                }

                // Get the document's text content
                TextRange documentRange = new TextRange(
                    Document.ContentStart,
                    Document.ContentEnd
                );
                string documentText = documentRange.Text;

                // Split text into lines
                string[] lines = documentText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                // Iterate through lines and apply formatting
                TextPointer currentPosition = Document.ContentStart;
                for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
                {
                    string currentLine = lines[lineIndex];

                    // Find the start and end of the current line
                    TextPointer lineStart = currentPosition;
                    TextPointer lineEnd = GetLineEnd(lineStart);

                    // Create a text range for the current line
                    TextRange lineRange = new TextRange(lineStart, lineEnd);

                    // Apply default color
                    Color defaultColor = Colors.Black; // Equivalent to ForeColor
                    ApplyColorToRange(lineRange, AutoResetFormat ? defaultColor : LastFontColor);

                    // Check for syntax highlighting
                    foreach (var entry in SyntaxTable)
                    {
                        if (currentLine.Contains(entry.Key))
                        {
                            ApplyColorToRange(lineRange, entry.Value);
                            LastFontColor = entry.Value;
                            break;
                        }
                    }

                    // Move to next line
                    currentPosition = lineEnd.GetNextInsertionPosition(LogicalDirection.Forward);
                    if (currentPosition == null) break;
                }

                // Scroll to end (equivalent to ScrollToCaret())
                ScrollToEnd();

                // Update last selection (using document length)
                LastSelection = documentText.Length;
            }
            finally
            {
                Updating = false;
            }
        }

        // Helper method to get the end of a line
        private TextPointer GetLineEnd(TextPointer lineStart)
        {
            return null;
            //return lineStart.GetLineEndPosition(0);
        }

        // Helper method to apply color to a text range
        private void ApplyColorToRange(TextRange range, Color color)
        {
            range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }
        #endregion
    }
}
