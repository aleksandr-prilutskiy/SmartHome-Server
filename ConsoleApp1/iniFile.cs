using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace LgTv
{
    // Объект для работы с файлом конфигурации программы
    class IniFile
    {
        private const string IniFileName = "SmartHomeServer.ini";   // Имя файла конфигурации программы
        private static string _fileName = "";                       // Имя файла конфигурации с полным путем

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
            Program.DatabaseAddress   = ReadString("Database", "Host", Program.DatabaseAddress);
            Program.DatabasePort      = ReadInt("Database", "Port", Program.DatabasePort);
            Program.DatabaseName      = ReadString("Database", "Name", Program.DatabaseName);
            Program.DatabaseUser      = ReadString("Database", "User", Program.DatabaseUser);
            Program.DatabasePassword  = ReadString("Database", "Password", Program.DatabasePassword);
    } // void ReadConfig()

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

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
            int size, string filePath);

    } // class IniFile
} // namespace LgTv
