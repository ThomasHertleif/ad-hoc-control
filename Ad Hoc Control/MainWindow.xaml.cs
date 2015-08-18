using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ad_Hoc_Control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                txtName.Text = "Bitte Name Eintragen";
                txtName.Background = Brushes.Red;
            }
            else
            {
                //Setze cmd auf Hidden
                ProcessStartInfo setNet = new ProcessStartInfo("cmd.exe", "/c Netsh wlan set hostednetwork mode=allow ssid=" + txtName.Text + " key=" + txtPW.Password);
                setNet.WindowStyle = ProcessWindowStyle.Hidden;
                ProcessStartInfo startNet = new ProcessStartInfo("cmd.exe", "/c Netsh wlan start hostednetwork");
                startNet.WindowStyle = ProcessWindowStyle.Hidden;

                //Starte Befehle
                Process.Start(setNet);
                await Task.Delay(1000);
                Process.Start(startNet);

                //UI Reaktion
                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;
                lblStat.Background = Brushes.Green;
                lblStat.Content = "Status: Ad-hoc Gestartet Name: " + txtName.Text;
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            //Setze CMD auf Hidden
            ProcessStartInfo stopNet = new ProcessStartInfo("cmd.exe", " /c Netsh wlan stop hostednetwork");
            stopNet.WindowStyle = ProcessWindowStyle.Hidden;

            //Starte Befehle
            Process.Start(stopNet);

            //UI Reaktion
            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;
            lblStat.Background = Brushes.Red;
            lblStat.Content = "Status: Ad-hoc gestoppt";


        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            //Erstelle ProcessStart Info
            ProcessStartInfo getNetStatStart = new ProcessStartInfo("cmd.exe", "/c netsh wlan show hostednetwork");
            getNetStatStart.CreateNoWindow = true;
            getNetStatStart.UseShellExecute = false;
            getNetStatStart.RedirectStandardOutput = true;
            getNetStatStart.RedirectStandardError = true;
            
            //Erstelle neuen CMD Process
            Process getNetStat = Process.Start(getNetStatStart);
            getNetStat.WaitForExit();

            //Lese String ein
            string StatOut = getNetStat.StandardOutput.ReadToEnd();

            // string ssid = StatOut.lines().map(trim).filter(startsWith("SSID"))[0];
            //Regex ssidPattern = new Regex("(.*?) : \"(.*?)\"");

            lblStat.Content = StatOut;
            
        }

        private void txtName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            txtName.Background = Brushes.White;
        }
    }
}
