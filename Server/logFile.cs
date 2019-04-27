using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SmartHome.Properties;

namespace SmartHome
{
    // Объект для работы с файлом журнала работы программы
    class LogFile
    {
        private const string LogFileName = "Logs\\SmartHomeServer.log"; // Имя файла журнала
        private static string _fileName;
        private static List<String> _buffer;
        private static bool _busy;

//===============================================================================================================
// Name...........:	LogFile
// Description....:	Инициализация объекта
// Syntax.........:	new LogFile()
//===============================================================================================================
        public LogFile()
        {
            _buffer = new List<String>();
            _fileName = AppDomain.CurrentDomain.BaseDirectory + LogFileName;
            StreamWriter stream = null;
            try
            {
                stream = new StreamWriter(_fileName, false, System.Text.Encoding.Default);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.LogFileCreateError, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                stream?.Close();
            }
            _busy = false;
        } // LogFile()

//===============================================================================================================
// Name...........:	Add
// Description....:	Добавление сообщения в буфер журнала работы программы
// Syntax.........:	Add(message)
// Parameters.....:	message     - текст сообщения
//===============================================================================================================
        public static void Add(string message)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (_busy)
            {
                Thread.Sleep(10);
                if (stopWatch.Elapsed.Milliseconds > 1000) break;
            }
            stopWatch.Stop();
            if (_busy) return;
            _buffer.Add(DateTime.Now.ToString("dd.MM HH:mm:ss: ") + message);
        } // void Add(message)

//===============================================================================================================
// Name...........:	SaveFile
// Description....:	Сохранение буфера журнала работы программы в log-файл
// Syntax.........:	SaveFile()
//===============================================================================================================
        public static void SaveFile()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (_busy)
            {
                Thread.Sleep(10);
                if (stopWatch.Elapsed.Milliseconds > 1000) break;
            }
            stopWatch.Stop();
            if (_busy) return;
            _busy = true;
            StreamWriter stream = null;
            try
            {
                stream = new StreamWriter(_fileName, true, System.Text.Encoding.Default);
                foreach (var line in _buffer) stream.WriteLine(line);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                stream?.Close();
            }
            if (Program.AppWindow != null)
            {
                foreach (var line in _buffer) Program.AppWindow.WriteToLog(line);
            }
            _buffer.Clear();
            _busy = false;
        } // void SaveFile()

//===============================================================================================================
// Name...........:	LoadFile
// Description....:	Чтение журнала работы программы во вкладку журнала главного окна программы
// Syntax.........:	LoadFile()
//===============================================================================================================
        public static void LoadFile()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (_busy)
            {
                Thread.Sleep(10);
                if (stopWatch.Elapsed.Milliseconds > 1000) break;
            }
            stopWatch.Stop();
            if (_busy)
            {
                Program.AppWindow.WriteToLog("Error: файл '" + LogFileName + "' не может быть прочитан");
                return;
            }
            _busy = true;
            StreamReader stream = null;
            try
            {
                stream = new StreamReader(_fileName, System.Text.Encoding.Default);
                String line;
                while ((line = stream.ReadLine()) != null) Program.AppWindow.WriteToLog(line);
            }
            catch (Exception)
            {
                Program.AppWindow.WriteToLog("Error: ошибка чтения из файла '" + LogFileName  + "'");
            }
            finally
            {
                stream?.Close();
            }
            _busy = false;
        } // void LoadFile()

    } // class LogFile
} // namespace SmartHome
