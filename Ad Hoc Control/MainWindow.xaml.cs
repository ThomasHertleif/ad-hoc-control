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
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //Starte Befehle
            CmdNetSH.setNet(txtName.Text, txtPW.Password);
            await Task.Delay(500);
            CmdNetSH.startNet();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            lblStat.Background = Brushes.Green;
            lblStat.Content = "Status: Ad-hoc Gestartet Name: " + txtName.Text;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

            CmdNetSH.stopNet();

            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;
            lblStat.Background = Brushes.Red;
            lblStat.Content = "Status: Ad-hoc gestoppt";


        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lblStat.Content = "Name: " + CmdNetSH.getName();

        }

        private void txtName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            txtName.Background = Brushes.White;
        }
    }
}
