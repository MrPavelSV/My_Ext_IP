using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System;

namespace Windowless_Sample
{
    public partial class App : Application
    {
        private static TaskbarIcon notifyIcon;
        public static App GetApp;
        public static string ipaddr;

        private System.Threading.Timer timer;

        public App()
        {
            GetApp = this;
        }

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
            ipaddr = new System.IO.StreamReader(System.Net.WebRequest.Create("https://spitcin.ru/getip/").GetResponse().GetResponseStream()).ReadToEnd();
            return ipaddr;
        }

        public void OnTimedEvent(object state)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                notifyIcon.ToolTipText = GetIP();
            }
            ));           
        }

        public void UpdateIP()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                notifyIcon.ToolTipText = GetIP();
            }
            ));
        }
    }
}
