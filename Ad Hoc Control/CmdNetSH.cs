using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Ad_Hoc_Control
{
    class CmdNetSH
    {
        static public void setNet(string name, string pw)
        {
            ProcessStartInfo setNet = new ProcessStartInfo("cmd.exe", "/c Netsh wlan set hostednetwork mode=allow ssid=" + name + " key=" + pw);
            setNet.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(setNet);
        }

        static public void startNet()
        {
            ProcessStartInfo startNet = new ProcessStartInfo("cmd.exe", "/c Netsh wlan start hostednetwork");
            startNet.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(startNet);
        }

        static public void stopNet()
        {
            ProcessStartInfo stopNet = new ProcessStartInfo("cmd.exe", " /c Netsh wlan stop hostednetwork");
            stopNet.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(stopNet);
        }
        static public string getInfo()
        {
            ProcessStartInfo getNetStatStart = new ProcessStartInfo("cmd.exe", "/c netsh wlan show hostednetwork");
            getNetStatStart.CreateNoWindow = true;
            getNetStatStart.UseShellExecute = false;
            getNetStatStart.RedirectStandardOutput = true;
            getNetStatStart.RedirectStandardError = true;

            Process getNetStat = Process.Start(getNetStatStart);
            getNetStat.WaitForExit();

            return getNetStat.StandardOutput.ReadToEnd();
        }

        static private bool statusOnOff(string output)
        {
            if (output.Contains("Nicht gestartet"))
            {
                return false;
            }
            else if (output.Contains("Gestartet"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public string getName()
        {
            var info = getInfo();
            Regex reg = new Regex("\"(.*?)\"");
            var match = reg.Match(info);

            return match.Groups[0].Value;
        }

    }
}
