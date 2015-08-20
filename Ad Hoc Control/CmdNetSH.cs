using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Ad_Hoc_Control
{
    class CmdNetSH
    {
        static public void setNet(string name, string pw)
        {
            if (name.Length < 1)
            {
                throw new System.Exception("fuuuuuuuuu");
            }
            ProcessStartInfo setNet = new ProcessStartInfo("Netsh", "wlan set hostednetwork mode=allow ssid=\"" + name + "\" key=" + pw);

            setNet.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(setNet);
        }

        static public void startNet()
        {
            ProcessStartInfo startNet = new ProcessStartInfo("Netsh", "wlan start hostednetwork");
            startNet.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(startNet);
        }

        static public void stopNet()
        {
            ProcessStartInfo stopNet = new ProcessStartInfo("Netsh", " wlan stop hostednetwork");
            stopNet.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(stopNet);
        }
        static public string getInfo()
        {
            ProcessStartInfo getNetStatStart = new ProcessStartInfo("Netsh", "wlan show hostednetwork");
            getNetStatStart.CreateNoWindow = true;
            getNetStatStart.UseShellExecute = false;
            getNetStatStart.RedirectStandardOutput = true;
            getNetStatStart.RedirectStandardError = true;

            Process getNetStat = Process.Start(getNetStatStart);
            getNetStat.WaitForExit();

            return getNetStat.StandardOutput.ReadToEnd();
        }

        static public bool statusOnOff()
        {
            if (getInfo().Contains("Nicht gestartet"))
            {
                return false;
            }
            else if (getInfo().Contains("Gestartet"))
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
