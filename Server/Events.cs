using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace SmartHome
{
    // Обработчик событий
    public class Events
    {
        private enum SheduleMode : byte // Режимы повторения событий
        {
            Once        = 0x00,         // Один раз (без повторений)
            Interval    = 0x01,         // С заданым интервалом (в секундах)
            Hourly      = 0x02,         // Каждый час
            Daily       = 0x03,         // Каждый день
            Weekly      = 0x04,         // Каждую неделю
            Monthly     = 0x05,         // Каждый месяц
            Yearly      = 0x06,         // Каждый год
        } // enum SheduleMode

        private static string[] Expressions = { // Список обрабатываемых выражений
            "device", "sensor"
        }; // string[] Expressions

        public class Event              // Запись о событии
        {
            public int Id;              // ID события
            public String Application;  // Приложение (драйвер события)
            public String Command;      // Команда
            public String Device;       // Устройство
            public String Parameters;   // Дополнительные параметры
        } // class Event

        public class Shedule: Event     // Запись в расписании
        {
            public byte Mode;
            public uint Period;
            public DateTime NextTime;
        } // class Shedule
        public static List<Shedule> SheduleList;

        public class Script : Event     // Запись о правиле скрипта
        {
            public String Rules;
            public uint Delay;
            public uint Timeout;
            public DateTime Timer;
        } // class Script
        public static List<Script> ScriptsList;

//===============================================================================================================
// Name...........:	Events
// Description....:	Инициализация объекта
// Syntax.........:	new Events()
//===============================================================================================================
        public Events()
        {
            SheduleList = new List<Shedule>();
            LoadShedule();
            ScriptsList = new List<Script>();
            LoadScripts();
            MySql.SaveTo("events", "status", "-1", "status = 0");
            Program.AppWindow.timerEvents.Enabled = true;
        } // Events()

//===============================================================================================================
// Name...........:	LoadShedule
// Description....:	Загрузка таблицы с рассписанием
// Syntax.........:	LoadShedule()
//===============================================================================================================
        public static void LoadShedule()
        {
            var table = MySql.ReadTable("shedule");
            if (table == null) return;
            foreach (var record in table)
            {
                var item = new Shedule();
                if (!int.TryParse(record[0], out item.Id)) item.Id = 0;
                item.Application = record[1];
                item.Command = record[2];
                item.Device = record[3];
                item.Parameters = record[4];
                if (!byte.TryParse(record[5], out item.Mode)) item.Mode = 0;
                if (!uint.TryParse(record[6], out item.Period)) item.Period = 0;
                DateTime.TryParse(record[7], out item.NextTime);
                SheduleList.Add(item);
            }
        } // void LoadShedule()

//===============================================================================================================
// Name...........:	LoadScripts
// Description....:	Загрузка таблицы скриптов
// Syntax.........:	LoadScripts()
//===============================================================================================================
        public static void LoadScripts()
        {
            var table = MySql.ReadTable("scripts", "*", "enable = 1", "metric ASC");
            if (table == null) return;
            foreach (var record in table)
            {
                var item = new Script();
                if (!int.TryParse(record[0], out item.Id)) item.Id = 0;
                item.Rules = record[2];
                item.Application = record[3];
                item.Command = record[4];
                item.Device = record[5];
                item.Parameters = record[6];
                if (!uint.TryParse(record[8], out item.Delay)) item.Delay = 0;
                if (!uint.TryParse(record[9], out item.Timeout)) item.Timeout = 0;
                item.Timer = DateTime.MinValue;
                ScriptsList.Add(item);
            }
        } // void LoadScripts()
        
//===============================================================================================================
// Name...........:	ChekEvents
// Description....:	Проверка и запуск событий из таблицы 'events' в базе данных
// Syntax.........:	ChekEvents()
//===============================================================================================================
        public static void ChekEvents()
        {
            var table = MySql.ReadTable("events", "*", "status = 0", "updated");
            if (table == null) return;
            foreach (var record in table)
            {
                var newevent = new Event();
                if (!int.TryParse(record[0], out newevent.Id)) newevent.Id = 0;
                newevent.Application = record[2];
                newevent.Command = record[3];
                newevent.Device = record[4];
                newevent.Parameters = record[5];
                if (IniFile.EventsLogEnable)
                    LogFile.Add("Event: " +
                        newevent.Application + " " +
                        newevent.Command + " " +
                        newevent.Device + " " +
                        newevent.Parameters);
                HandleEvent(newevent);
            }
        } // void ChekEvents()

//===============================================================================================================
// Name...........:	ChekShedule
// Description....:	Проверка и запуск событий по рассписанию
// Syntax.........:	ChekShedule()
//===============================================================================================================
        public static void ChekShedule()
        {
            foreach (var shedule in SheduleList)
            {
                if ((shedule.NextTime == DateTime.MinValue) || (shedule.NextTime > DateTime.Now)) continue;
                if (IniFile.EventsLogEnable)
                    LogFile.Add("Shedule event: " +
                        shedule.Application + " " +
                        shedule.Command + " " +
                        shedule.Device + " " +
                        shedule.Parameters);
                HandleEvent(shedule);
                switch (shedule.Mode)
                {
                    case (byte) SheduleMode.Once:
                        shedule.NextTime = DateTime.MinValue;
                        break;
                    case (byte) SheduleMode.Interval:
                        shedule.NextTime = shedule.NextTime.AddSeconds(shedule.Period);
                        break;
                    case (byte) SheduleMode.Hourly:
                        shedule.NextTime = shedule.NextTime.AddHours(1);
                        break;
                    case (byte) SheduleMode.Daily:
                        shedule.NextTime = shedule.NextTime.AddDays(1);
                        break;
                    case (byte) SheduleMode.Weekly:
                        shedule.NextTime = shedule.NextTime.AddDays(7);
                        break;
                    case (byte) SheduleMode.Monthly:
                        shedule.NextTime = shedule.NextTime.AddMonths(1);
                        break;
                    case (byte) SheduleMode.Yearly:
                        shedule.NextTime = shedule.NextTime.AddYears(1);
                        break;
                }
                String nextime = shedule.NextTime != DateTime.MinValue 
                    ? "'" + shedule.NextTime.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                    : "NULL";
                MySql.SaveTo("shedule", "next_time", nextime, "id = '" + shedule.Id + "'");
            }
        } //  ChekShedule()

//===============================================================================================================
// Name...........:	ChekScripts
// Description....:	Проверка и запуск событий по сценариям, для заданного датчика
// Syntax.........:	ChekScripts(sensor)
// Parameters.....:	sensor      - ссылка на объект типа датчик (Sensor)
//===============================================================================================================
        public static void ChekScripts(Sensors.Sensor sensor)
        {
            if (ScriptsList == null) return;
            foreach (var script in ScriptsList)
            {
                if ((script.Timer != DateTime.MinValue) && (script.Timeout > 0))
                    if (DateTime.Now < script.Timer) continue;
                if (script.Rules.IndexOf("sensor(" + sensor.Topic + ")", StringComparison.Ordinal) < 0) continue;
                if (!ChekLexeme(ReplaceExpressions(script.Rules))) continue;
                if (IniFile.EventsLogEnable)
                    LogFile.Add("Script event: " +
                        script.Application + " " +
                        script.Command + " " +
                        script.Device + " " +
                        script.Parameters);
                HandleEvent(script);
                script.Timer = DateTime.Now.AddSeconds(script.Timeout);
            }
        } //  ChekScripts()

//===============================================================================================================
// Name...........:	HandleEvent
// Description....:	Обработка события
// Syntax.........:	HandleEvent(newevent)
//===============================================================================================================
        public static void HandleEvent(Event newevent)
        {
            bool status = false;
            switch (newevent.Application)
            {
                case "@":
                    switch (newevent.Command)
                    {
                        case "reload":
                            switch (newevent.Parameters)
                            {
                                case "devices":
                                    Program.AppWindow.GridViewDevices.Rows.Clear();
                                    Devices.DevicesList.Clear();
                                    Devices.LoadTable();
                                    break;
                                case "sensors":
                                    Program.AppWindow.GridViewSensors.Rows.Clear();
                                    Sensors.SensorsList.Clear();
                                    Sensors.LoadTable();
                                    break;
                                case "shedule":
                                    Program.AppWindow.timerEvents.Enabled = false;
                                    SheduleList.Clear();
                                    LoadShedule();
                                    Program.AppWindow.timerEvents.Enabled = true;
                                    break;
                                case "scripts":
                                    Program.AppWindow.timerEvents.Enabled = false;
                                    ScriptsList.Clear();
                                    LoadScripts();
                                    Program.AppWindow.timerEvents.Enabled = true;
                                    break;
                            }
                            status = true;
                            break;
                        //case "turn off all":
                            //Devices.TurnOffAll();
                            //break;
                    }
                    break;
                case "nooLite":
                    var device = Devices.Find(newevent.Device);
                    if (device != null)
                        status = nooLite.SendCommand(device.Channel, newevent.Command, newevent.Parameters);
                    break;
                default:
                    status = ExecuteFile(newevent);
                    break;
            }
            MySql.SaveTo("events", "status", status.ToString(), "id = '" + newevent.Id + "'");
        } // void HandleEvent(newevent)

//===============================================================================================================
// Name...........:	ExecuteFile
// Description....:	Запуск внешнего файла обработки события (драйвера)
// Syntax.........:	ExecuteFile(newevent)
// Return value(s):	Success:    - true
//                  Failure:    - false
//===============================================================================================================
        public static bool ExecuteFile(Event newevent)
        {
            string exeFile = AppDomain.CurrentDomain.BaseDirectory + "Utils\\" + newevent.Application + ".exe";
            if (!File.Exists(exeFile))
            {
                LogFile.Add("Error: файл " + exeFile + " - не найден");
                MySql.SaveTo("events", "status", "-1", "id = '" + newevent.Id + "'");
                return false;
            }
            Process iStartProcess = new Process();
            iStartProcess.StartInfo.FileName = exeFile;
            iStartProcess.StartInfo.Arguments = "";
            if (newevent.Command.Length > 0)
                iStartProcess.StartInfo.Arguments += " " + newevent.Command;
            if (newevent.Device.Length > 0)
                iStartProcess.StartInfo.Arguments += " " + newevent.Device;
            if (newevent.Parameters.Length > 0)
                iStartProcess.StartInfo.Arguments += " " + ReplaceExpressions(newevent.Parameters);
            if (iStartProcess.Start()) return true;
            return false;
        } // bool ExecuteFile(newevent)

//===============================================================================================================
// Name...........:	ReplaceExpressions
// Description....:	Замена всех выражений в исходной строке на их значения
// Syntax.........:	ReplaceExpressions(source)
// Parameters.....:	source      - исходная срока
// Return value(s):	            - строка, в которой все выражения заменены на их значения
//===============================================================================================================
        public static String ReplaceExpressions(String source)
        {
            foreach (var expression in Expressions)
            {
                int namepos = source.ToLower().IndexOf(expression + "(", StringComparison.Ordinal);
                if (namepos < 0) continue;
                int namelen = source.Substring(namepos + expression.Length + 1).IndexOf(")", StringComparison.Ordinal);
                if (namelen < 0) continue;
                String identifier = source.Substring(namepos + expression.Length + 1, namelen);
                if (expression == Expressions[0]) // device
                {
                    var device = Devices.Find(identifier);
                    identifier = "";
                    if (device != null) identifier = device.State.ToString();
                }
                else if (expression == Expressions[1]) // sensor
                {
                    var sensor = Sensors.Find(identifier);
                    identifier = "";
                    if (sensor != null) identifier = sensor.Value;
                }
                if (identifier.Length > 0)
                    return ReplaceExpressions(source.Substring(0, namepos) + identifier +
                        source.Substring(namepos + expression.Length + namelen + 2));
            }
            return source;
        } // String ReplaceExpressions(String source)

//===============================================================================================================
// Name...........:	ChekLexeme
// Description....:	Рекурсивная проверка логического выражения в исходной строке
// Syntax.........:	ChekLexeme(source)
// Parameters.....:	source      - исходная строка
// Return value(s):	true        - логическое выражение верно
//                  false       - логическое выражение неверно
//===============================================================================================================
        public static bool ChekLexeme(String source)
        {
            var substring = new String[2];
            var values = new decimal[2];
            if (SplitLexeme(source, "&", substring)) return ChekLexeme(substring[0]) && ChekLexeme(substring[1]);
            if (SplitLexeme(source, "|", substring)) return ChekLexeme(substring[0]) || ChekLexeme(substring[1]);
            if (SplitLexeme(source, ">", values)) return values[0] > values[1];
            if (SplitLexeme(source, "<", values)) return values[0] < values[1];
            if (SplitLexeme(source, "=", values)) return values[0] == values[1];
            if (SplitLexeme(source, ">=", values)) return values[0] >= values[1];
            if (SplitLexeme(source, "<=", values)) return values[0] <= values[1];
            if (SplitLexeme(source, "<>", values)) return values[0] != values[1];
            return false;
        } // bool ChekLexeme(source)

//===============================================================================================================
// Name...........:	SplitLexeme
// Description....:	Разбиение логического выражения на две части по первому вхожению разделителя
// Syntax.........:	SplitLexeme(source, splitter, result)
// Parameters.....:	source      - исходная строка
//                  splitter    - разделитель (строка)
//                  result      - массив из двух чисел, в котором возвращается результат разбиения выражения
// Return value(s):	true        - разделитель найден, в result возвращеются две подстроки
//                  false       - разделитель не найден, result не изменен
//===============================================================================================================
        public static bool SplitLexeme(String lexeme, String splitter, decimal[] result)
        {
            var substring = new String[2];
            if (!SplitLexeme(lexeme, splitter, substring)) return false;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (!decimal.TryParse(substring[0], style, culture, out result[0])) return false;
            if (!decimal.TryParse(substring[1], style, culture, out result[1])) return false;
            return true;
        } // bool SplitLexeme(source, splitter, decimal[])

//===============================================================================================================
// Name...........:	SplitLexeme
// Description....:	Разбиение логического выражения на две части по первому вхожению разделителя
// Syntax.........:	SplitLexeme(source, splitter, result)
// Parameters.....:	source      - исходная строка
//                  splitter    - разделитель (строка)
//                  result      - массив из двух строк, в котором возвращается результат разбиения выражения
// Return value(s):	true        - разделитель найден, в result возвращеются две подстроки
//                  false       - разделитель не найден, result не изменен
//===============================================================================================================
        public static bool SplitLexeme(String source, String splitter, String[] result)
        {
            int pos = source.IndexOf(splitter, StringComparison.Ordinal);
            if (pos < 0) return false;
            result[0] = source.Substring(0, pos).Trim();
            result[1] = source.Substring(pos + splitter.Length).Trim();
            return true;
        } // bool SplitLexeme(source, splitter, String[])

    } // class Events
} // namespace SmartHome
