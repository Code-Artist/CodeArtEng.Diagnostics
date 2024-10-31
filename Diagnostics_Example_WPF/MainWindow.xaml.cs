using CodeArtEng.Diagnostics;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Diagnostics_Example_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CbTheme.ItemsSource = Enum.GetValues(typeof(TextBoxTheme));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < 20; x++)
                Trace.Write(x.ToString() + " ");

            Trace.WriteLine(" ");
            Trace.WriteLine(" ");

            for (int x = 0; x < 20; x++)
                Trace.WriteLine("Line " + x.ToString());


            Trace.WriteLine("Multiline String:\r\nLine 1\rLine2\nLine3");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Trace.Flush();
        }

        private async Task DoWorkAsync()
        {
            Trace.WriteLine("Write message from worker thread...");
            for (int x = 0; x < 200; x++)
            {
                Trace.WriteLine($"Thread: Trace Message {x}");
                await Task.Delay(10); // Modern replacement for Thread.Sleep
            }
        }
        
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable the button while processing
                var button = (Button)sender;
                button.IsEnabled = false;

                CodeProfiler.Start("Thread Write");

                // Start the background work
                var workTask = DoWorkAsync();

                // Main thread work
                for (int x = 0; x < 100; x++)
                {
                    Trace.WriteLine($"Main: Trace Message {x}");
                    await Task.Delay(20); // Modern replacement for Thread.Sleep
                                          // No need for DoEvents in WPF as it has a proper message pump
                }

                // Wait for the background work to complete
                await workTask;

                CodeProfiler.Stop("Thread Write");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Re-enable the button
                ((Button)sender).IsEnabled = true;
            }
        }
    }
}