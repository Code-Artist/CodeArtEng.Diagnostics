using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using Microsoft.Win32;

namespace CodeArtEng.Diagnostics.Controls
{
    /// <summary>
    /// TextBox with TraceListener for WPF
    /// </summary>
    public class DiagnosticsTextBox : TextBox
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
        ~DiagnosticsTextBox()
        {
            refreshTimer.IsEnabled = false;
            Tracer.Enabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticsTextBox"/> class.
        /// </summary>
        public DiagnosticsTextBox()
        {
            InitializeComponent();

            // Default control property
            IsReadOnly = true;
            AcceptsReturn = true;
            TextWrapping = TextWrapping.NoWrap;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            DisplayBufferSize = 0;
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
                ThemeValue = TextBoxTheme.UserDefined;
            }
        }

        private bool UpdatingTheme = false;
        private TextBoxTheme ThemeValue = TextBoxTheme.Windows;

        /// <summary>
        /// Get or set Diagnostic TextBox Theme. <see cref="TextBoxTheme"/>
        /// </summary>
        public TextBoxTheme Theme
        {
            get => ThemeValue;
            set
            {
                UpdatingTheme = true;
                try { ThemeValue = this.SetTheme(value); }
                finally { UpdatingTheme = false; }
            }
        }

        #endregion

        /// <summary>
        /// Enable / Disable trace listener to capture message from trace source.
        /// </summary>
        public bool ListenerEnabled
        {
            get { return Tracer.Enabled; }
            set
            {
                Tracer.Enabled = value;
                ConfigureFileWritter();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether control should response to Flush() command from trace source.
        /// </summary>
        public bool FlushEnabled { get; set; }

        /// <summary>
        /// Enable / Disable printing time stamp in front of each message.
        /// </summary>
        public bool ShowTimeStamp
        {
            get { return Tracer.ShowTimeStamp; }
            set
            {
                Tracer.ShowTimeStamp = value;
                OutputFileWriter.ShowTimeStamp = value;
            }
        }

        /// <summary>
        /// Define time stamp string format when <see cref="TimeStampStyle"/> set as <see cref="TraceTimeStampStyle.DateTimeString"/> 
        /// </summary>
        public string TimeStampFormat
        {
            get { return Tracer.TimeStampFormat; }
            set
            {
                Tracer.TimeStampFormat = value;
                OutputFileWriter.TimeStampFormat = value;
            }
        }

        /// <summary>
        /// Define time stamp style.
        /// </summary>
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
        public int DisplayBufferSize { get; set; }

        /// <summary>
        /// Define how often the control shall be updated. Value in ms.
        /// </summary>
        public int RefreshInterval
        {
            get => refreshTimer.Interval.Milliseconds;
            set => refreshTimer.Interval = TimeSpan.FromMilliseconds(value);
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
            if (FlushEnabled) Clear();
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

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            menuItemEnabled.IsChecked = ListenerEnabled;
        }

        private void MenuItemEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListenerEnabled = !ListenerEnabled;
        }

        private void MenuItemClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
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

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            lock (LockObject)
            {
                if (MessageBuffer.Length == 0) return;

                Dispatcher.Invoke(() =>
                {
                    AppendText(MessageBuffer);
                    MessageBuffer = "";

                    if (DisplayBufferSize <= 0) return;

                    var lines = Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    if (lines.Length > DisplayBufferSize)
                    {
                        string[] data = new string[DisplayBufferSize];
                        Array.Copy(lines, lines.Length - DisplayBufferSize, data, 0, DisplayBufferSize);
                        Text = string.Join(Environment.NewLine, data);
                    }

                    CaretIndex = Text.Length;
                    ScrollToEnd();
                });
            }
        }

        /// <summary>
        /// Output trace to file. Set to a valid path to enable this option.
        /// </summary>
        public string OutputFile
        {
            get { return OutputFileWriter.OutputFile; }
            set { OutputFileWriter.OutputFile = value; }
        }

        /// <summary>
        /// Configure secondary output path for trace output as backup when failed to write to OutputFile.
        /// </summary>
        public string OutputFileBackup
        {
            get { return OutputFileWriter.BackupOutputFile; }
            set { OutputFileWriter.BackupOutputFile = value; }
        }

        private bool _WriteToFile = false;
        /// <summary>
        /// Set this flag to true to enable writing log to OutputFile.
        /// </summary>
        public bool WriteToFile
        {
            get { return _WriteToFile; }
            set
            {
                _WriteToFile = value;
                ConfigureFileWritter();
            }
        }

        private void ConfigureFileWritter()
        {
            OutputFileWriter.ListenerEnabled = (ListenerEnabled && _WriteToFile);
        }
    }
}