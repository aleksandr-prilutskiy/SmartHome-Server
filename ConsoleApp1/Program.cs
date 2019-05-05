using System;

namespace LgTv
{
    class Program
    {
        public static string DatabaseAddress;               // Адрес сервера базы данных
        public static int DatabasePort = 3306;              // Порт сервера базы данных
        public static string DatabaseName = "smart_home";   // Имя базы данных
        public static string DatabaseUser = "root";         // Имя пользователя для подключения к базе данных
        public static string DatabasePassword;              // Пароль пользователя для подключения к базе данных

        static void Main(string[] args)
        {
            IniFile.ReadConfig();
            Console.WriteLine("Hello World!");
        }
    }
}
