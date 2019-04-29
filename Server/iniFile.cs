using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace SmartHome
{
    // Объект для работы с файлом конфигурации программы
    class IniFile
    {
        private const string IniFileName = "SmartHomeServer.ini";   // Имя файла конфигурации программы
        private static string _fileName = "";                       // Полное файла конфигурации программы (с каталогом)

//===============================================================================================================
// Name...........:	ReadConfig
// Description....:	Чтение настроек программы из файла конфигурации
// Syntax.........:	ReadConfig()
//===============================================================================================================
        public static void ReadConfig()
        {
            _fileName = AppDomain.CurrentDomain.BaseDirectory + IniFileName;
            if (!File.Exists(_fileName))
            {
                _fileName = "";
                return;
            }
            Program.AppWindowPosX     = ReadInt("Window", "Pos_X", 0);
            Program.AppWindowPosY     = ReadInt("Window", "Pos_Y", 0);
            Program.AppWindowWidth    = ReadInt("Window", "Size_X", 450);
            Program.AppWindowHeight   = ReadInt("Window", "Size_Y", 300);
            Program.DatabaseAddress   = ReadString("Database", "Host", "localhost");
            Program.DatabasePort      = ReadInt("Database", "Port", 3306);
            Program.DatabaseName      = ReadString("Database", "Name", "smart_home");
            Program.DatabaseUser      = ReadString("Database", "User", "root");
            Program.DatabasePassword  = ReadString("Database", "Password", "");
            Program.MqttBrokerAddress = ReadString("MQTT", "Host", "localhost");
            Program.MqttBrokerPort    = ReadInt("MQTT", "Port", 1883);
            Program.MqttUserName      = ReadString("MQTT", "User", "");
            Program.MqttPassword      = ReadString("MQTT", "Password", "");
            Program.EventsLogEnable   = ReadBool("Log", "Events", false);
            Program.PingLogEnable     = ReadBool("Log", "Ping", false);
            Program.NooLiteLogEnable  = ReadBool("Log", "nooLite", false);
            Program.MqttLogEnable     = ReadBool("Log", "MQTT", false);
    } // void ReadConfig()

//===============================================================================================================
// Name...........:	SaveConfig
// Description....:	Запись настроек программы в файл конфигурации
// Syntax.........:	SaveConfig()
//===============================================================================================================
    public static void SaveConfig()
        {
            WriteString("Window", "Pos_X", Program.AppWindow.Location.X.ToString());
            WriteString("Window", "Pos_Y", Program.AppWindow.Location.Y.ToString());
            WriteString("Window", "Size_X", Program.AppWindow.Size.Width.ToString());
            WriteString("Window", "Size_Y", Program.AppWindow.Size.Height.ToString());
            WriteString("Log", "Events", Program.EventsLogEnable.ToString());
            WriteString("Log", "Ping", Program.PingLogEnable.ToString());
            WriteString("Log", "nooLite", Program.NooLiteLogEnable.ToString());
            WriteString("Log", "MQTT", Program.MqttLogEnable.ToString());
        } // void saveConfig()

//===============================================================================================================
// Name...........:	ReadString
// Description....:	Чтение строки из файла конфигурации программы
// Syntax.........:	ReadString(section, key, value)
// Parameters.....:	section     - имя секции в ini-файле
//                  key         - имя параметра в ini-файле
//                  value       - значение по умолчанию
// Return value(s):	Success:    - значение считанного параметра
//                  Failure:    - значение по умолчанию (value)
//===============================================================================================================
        private static string ReadString(string section, string key, string value)
        {
            if (_fileName == "") return value;
            const int bufferSize = 255;
            StringBuilder temp = new StringBuilder(bufferSize);
            GetPrivateProfileString(section, key, value, temp, bufferSize, _fileName);
            return temp.ToString();
        } // string ReadString(section, key, value)

//===============================================================================================================
// Name...........:	ReadInt
// Description....:	Чтение целочисленного значения из файла конфигурации программы
// Syntax.........:	ReadInt(section, key, value)
// Parameters.....:	section     - имя секции в ini-файле
//                  key         - имя параметра в ini-файле
//                  value       - значение по умолчанию 
// Return value(s):	Success:    - значение считанного параметра
//                  Failure:    - значение по умолчанию (value)
//===============================================================================================================
        private static int ReadInt(string section, string key, int value)
        {
            if (_fileName == "") return value;
            int result;
            const int bufferSize = 255;
            StringBuilder temp = new StringBuilder(bufferSize);
            GetPrivateProfileString(section, key, "", temp, bufferSize, _fileName);
            if (!int.TryParse(temp.ToString(), out result)) result = value;
            return result;
        } // string ReadInt(section, key, value)

//===============================================================================================================
// Name...........:	ReadBool
// Description....:	Чтение логического (boolean) значения из файла конфигурации программы
// Syntax.........:	ReadBool(section, key, value)
// Parameters.....:	section     - имя секции в ini-файле
//                  key         - имя параметра в ini-файле
//                  value       - значение по умолчанию 
// Return value(s):	Success:    - значение считанного параметра
//                  Failure:    - значение по умолчанию (value)
//===============================================================================================================
        private static bool ReadBool(string section, string key, bool value)
        {
            return ReadString(section, key, value.ToString()) == true.ToString();
        } // string ReadBool(section, key, value)

//===============================================================================================================
// Name...........:	WriteString
// Description....:	Запись строки в файл конфигурации программы
// Syntax.........:	WriteString(section, key, value)
// Parameters.....:	section     - имя секции в ini-файле
//                  key         - имя параметра в ini-файле
//                  value       - значение параметра
//===============================================================================================================
        public static void WriteString(string section, string key, string value)
        {
            String newFileName = _fileName != "" 
                ? _fileName
                : AppDomain.CurrentDomain.BaseDirectory + IniFileName;
            WritePrivateProfileString(section, key, value, newFileName);
        } // void WriteString(section, key, value)

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
            int size, string filePath);

        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key, string str, string filePath);

    } // class IniFile
} // namespace SmartHome
