using System;
using System.Threading;
using System.Windows.Forms;
using SmartHome.Properties;

namespace SmartHome
{
    static class Program
    {
        public static int AppWindowPosX;                    // Положение окна прогаммы по горизонтали
        public static int AppWindowPosY;                    // Положение окна прогаммы по вертикали
        public static int AppWindowWidth;                   // Размер окна прогаммы по горизонтали
        public static int AppWindowHeight;                  // Размер окна прогаммы по вертикали
        public static bool StartMinimized = false;          // Запускать программу в свернутом виде
        public static string DatabaseAddress;               // Адрес сервера базы данных
        public static int DatabasePort = 3306;              // Порт сервера базы данных
        public static string DatabaseName = "smart_home";   // Имя базы данных
        public static string DatabaseUser = "root";         // Имя пользователя для подключения к базе данных
        public static string DatabasePassword;              // Пароль пользователя для подключения к базе данных
        public static string MqttBrokerAddress = "";        // Адрес брокера MQTT
        public static int MqttBrokerPort = 1883;            // Порт брокера MQTT
        public static string MqttUserName;                  // Имя пользователя для подключения к брокеру MQTT
        public static string MqttPassword;                  // Пароль пользователя для подключения к брокеру MQTT
        public static bool EventsLogEnable = false;         // Отображать в журнале обработку событий
        public static bool PingLogEnable = false;           // Отображать в журнале опрос состояния устройств
        public static bool NooLiteLogEnable = false;        // Отображать в журнале обмен с nooLite MTRF-64-USB
        public static bool MqttLogEnable = false;           // Отображать в журнале данные от брокера MQTT

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
                MessageBox.Show(Resources.AppAlreadyRunning, Resources.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IniFile.ReadConfig();
            AppWindow = new FormMain();
            new LogFile();
            new MySQL();
            new nooLite();
            new Sensors();
            new Devices();
            new MQTT();
            new Events();
            LogFile.Add(Resources.StartServer);
            Devices.PingAll();
            Application.Run(AppWindow);
        } // void Main()

    } // class Program
} // namespace SmartHome
