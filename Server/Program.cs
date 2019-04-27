using System;
using System.Threading;
using System.Windows.Forms;
using SmartHome.Properties;

namespace SmartHome
{
    static class Program
    {
        public static FormMain AppWindow;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            bool isSingleInstance;
            new Mutex(true, "MY_UNIQUE_MUTEX_NAME", out isSingleInstance);
            if (!isSingleInstance)
            {
                MessageBox.Show(Resources.AppAlreadyRunning, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppWindow = new FormMain();
            new LogFile();
            new MySql();
            new nooLite();
            new Sensors();
            new Devices();
            new Events();
            LogFile.Add("Сервер запущен");
            Devices.PingAll();
            Application.Run(AppWindow);
        } // void Main()

    } // class Program
} // namespace SmartHome
