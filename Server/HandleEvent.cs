using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using SmartHome.Properties;

namespace SmartHome
{
    // Обработчик событий
    public class HandleEvent
    {
        private static string[] Expressions = { // Список обрабатываемых выражений
            "device", "sensor"
        }; // string[] Expressions

        private const string UtilsSubDir = "utils";               // Подкаталог утилит (драйверов устройств)

//===============================================================================================================
// Name...........:	Execute
// Description....:	Обработка события
// Syntax.........:	Execute(newevent, mode)
//===============================================================================================================
        public static void Execute(Events.Event newevent, byte mode)
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
                                    Events.SheduleList.Clear();
                                    Events.LoadShedule();
                                    Program.AppWindow.timerEvents.Enabled = true;
                                    break;
                                case "scripts":
                                    Program.AppWindow.timerEvents.Enabled = false;
                                    Events.ScriptsList.Clear();
                                    Events.LoadScripts();
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
            if (mode == (byte)Events.EventMode.Interface)
                MySql.SaveTo("events", "status", status.ToString(), "id = '" + newevent.Id + "'");
        } // void Execute(newevent, mode)

//===============================================================================================================
// Name...........:	ExecuteFile
// Description....:	Запуск внешнего файла обработки события (драйвера)
// Syntax.........:	ExecuteFile(newevent)
// Return value(s):	Success:    - true
//                  Failure:    - false
//===============================================================================================================
        public static bool ExecuteFile(Events.Event newevent)
        {
            string exeFile = AppDomain.CurrentDomain.BaseDirectory + UtilsSubDir + "\\" + newevent.Application + ".exe";
            if (!File.Exists(exeFile))
            {
                LogFile.Add(Resources.LogMsgError + "файл " + exeFile + " - не найден");
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

    } // class HandleEvent
} // namespace SmartHome
