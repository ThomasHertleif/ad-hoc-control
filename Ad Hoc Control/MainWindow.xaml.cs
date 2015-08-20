using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ad_Hoc_Control
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setStatus();
            txtName.Text = CmdNetSH.getName().Replace("\"", "");

        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //Starte Befehle
            if (txtPW.Password.Length >= 8 && txtName.Text.Length != 0)
            {
                CmdNetSH.setNet(txtName.Text, txtPW.Password);
                await Task.Delay(200);
                CmdNetSH.startNet();
                await Task.Delay(200);
                setStatus();
            }
            else if (txtPW.Password.Length < 8 && txtName.Text.Length == 0)
            {
                lblStat.Background = Brushes.Red;
                lblStat.Content = "Bitte Passwort und Name eingeben.";
            }
            else if (txtPW.Password.Length < 8)
            {
                lblStat.Background = Brushes.Red;
                lblStat.Content = "Passwort muss mindestens\n8 Zeichen enthalten.";
            }
            else if (txtName.Text.Length == 0)
            {
                lblStat.Background = Brushes.Red;
                lblStat.Content = "Bitte Name eingeben.";
            }

            else
            {
                lblStat.Background = Brushes.Red;
                lblStat.Content = "Alarm! Alarm!";
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            CmdNetSH.stopNet();
            setStatus();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            setStatus();
        }

        private void setStatus()
        {
            if (CmdNetSH.statusOnOff() == true)
            {
                lblStat.Content = "Status: Netzwerk gestartet\nName: " + CmdNetSH.getName();
                lblStat.Background = Brushes.Green;
                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;
            }
            else if (CmdNetSH.statusOnOff() == false)
            {
                lblStat.Content = "Status: Netzwerk gestoppt\nName: " + CmdNetSH.getName();
                lblStat.Background = Brushes.Orange;
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
            }
        }


    }
}
