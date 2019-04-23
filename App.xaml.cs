using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System;

namespace Windowless_Sample
{
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;
        private System.Threading.Timer timer;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            notifyIcon = (TaskbarIcon) FindResource("NotifyIcon");
            notifyIcon.ToolTipText = GetIP();
            timer = new System.Threading.Timer(OnTimedEvent);
            timer.Change(0, 30000);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            timer.Dispose();
            notifyIcon.Dispose();
            base.OnExit(e);
        }

        private string GetIP()
        {
            string[] a = new System.IO.StreamReader(System.Net.WebRequest.Create("http://checkip.dyndns.org").GetResponse().GetResponseStream()).ReadToEnd().Trim().Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            return a3[0];
        }

        private void OnTimedEvent(object state)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                notifyIcon.ToolTipText = GetIP();
            }
            ));           
        }
    }
}
