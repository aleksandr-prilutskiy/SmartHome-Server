using System;
using System.Collections.Generic;
using SmartHome.Properties;

namespace SmartHome
{
    // Объект для работы с событиями системы "Умный дом" (включая события по расписанию и сценарии)
    public class Events
    {
        public enum EventMode : byte    // Режимы событий
        {
            Interface   = 0x00,         // Интерфейс
            Shedule     = 0x01,         // Расписание
            Script      = 0x02,         // Сценарий
        } // enum EventMode

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
            public byte Mode;           // Режим повторения события
            public uint Period;         // Период повторения события (в секундах)
            public DateTime NextTime;   // Время следующего события
        } // class Shedule
        public static List<Shedule> SheduleList; // Список событий по расписанию

        public class Script : Event     // Запись о правиле скрипта
        {
            public String Rules;        // Правило скрипта
            public uint Delay;          // Задержка срабатывания скрипта
            public uint Timeout;        // Таймаут перед повторным срабатыванием скрипта
            public DateTime Timer;      // Таймер повторного срабатывания скрипта
        } // class Script
        public static List<Script> ScriptsList; // Список скриптов

//===============================================================================================================
// Name...........:	Events
// Description....:	Инициализация объекта
// Syntax.........:	new Events()
//===============================================================================================================
        public Events()
        {
            SheduleList = new List<Shedule>();
            ScriptsList = new List<Script>();
            LoadShedule();
            LoadScripts();
            MySQL.SaveTo("events", "status", "-1", "status = 0"); // отметки о пропущенных событиях
            Program.AppWindow.timerEvents.Enabled = true;
        } // Events()

//===============================================================================================================
// Name...........:	LoadShedule
// Description....:	Загрузка таблицы с рассписанием
// Syntax.........:	LoadShedule()
//===============================================================================================================
        public static void LoadShedule()
        {
            var table = MySQL.ReadTable("shedule", "id,application,command,device,parameters,mode,period,next_time",
                "enable = 1", "id ASC");
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
            var table = MySQL.ReadTable("scripts", "id,rules,application,command,device,parameters,delay,timeout",
                "enable = 1", "id ASC");
            if (table == null) return;
            foreach (var record in table)
            {
                var item = new Script();
                if (!int.TryParse(record[0], out item.Id)) item.Id = 0;
                item.Rules = record[1];
                item.Application = record[2];
                item.Command = record[3];
                item.Device = record[4];
                item.Parameters = record[5];
                if (!uint.TryParse(record[6], out item.Delay)) item.Delay = 0;
                if (!uint.TryParse(record[7], out item.Timeout)) item.Timeout = 0;
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
            var table = MySQL.ReadTable("events", "id,application,command,device,parameters",
                "status = 0", "updated ASC");
            if (table == null) return;
            foreach (var record in table)
            {
                var newevent = new Event();
                if (!int.TryParse(record[0], out newevent.Id)) newevent.Id = 0;
                newevent.Application = record[1];
                newevent.Command = record[2];
                newevent.Device = record[3];
                newevent.Parameters = record[4];
                if (Program.EventsLogEnable)
                    LogFile.Add(Resources.LogMsgEvent +
                        (newevent.Application.Length > 0 ? newevent.Application + " " : "") +
                        (newevent.Command.Length > 0 ? newevent.Command + " " : "") +
                        (newevent.Device.Length > 0 ? newevent.Device + " " : "") +
                        (newevent.Parameters.Length > 0 ? newevent.Parameters : ""));
                HandleEvent.Execute(newevent, (byte)EventMode.Interface);
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
                if (Program.EventsLogEnable)
                    LogFile.Add(Resources.LogMsgShedule +
                        (shedule.Application.Length > 0 ? shedule.Application + " " : "") +
                        (shedule.Command.Length > 0 ? shedule.Command + " " : "") +
                        (shedule.Device.Length > 0 ? shedule.Device + " " : "") +
                        (shedule.Parameters.Length > 0 ? shedule.Parameters : ""));
                HandleEvent.Execute(shedule, shedule.Id < 0 ? (byte)EventMode.Script : (byte)EventMode.Shedule);
                switch (shedule.Mode)
                {
                    case (byte)SheduleMode.Once:
                        shedule.NextTime = DateTime.MinValue;
                        break;
                    case (byte)SheduleMode.Interval:
                        shedule.NextTime = shedule.NextTime.AddSeconds(shedule.Period);
                        break;
                    case (byte)SheduleMode.Hourly:
                        shedule.NextTime = shedule.NextTime.AddHours(1);
                        break;
                    case (byte)SheduleMode.Daily:
                        shedule.NextTime = shedule.NextTime.AddDays(1);
                        break;
                    case (byte)SheduleMode.Weekly:
                        shedule.NextTime = shedule.NextTime.AddDays(7);
                        break;
                    case (byte)SheduleMode.Monthly:
                        shedule.NextTime = shedule.NextTime.AddMonths(1);
                        break;
                    case (byte)SheduleMode.Yearly:
                        shedule.NextTime = shedule.NextTime.AddYears(1);
                        break;
                }
                String nextime = shedule.NextTime != DateTime.MinValue 
                    ? "'" + shedule.NextTime.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                    : "NULL";
                MySQL.SaveTo("shedule", "next_time", nextime, "id = '" + shedule.Id + "'");
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
                if (!HandleEvent.ChekLexeme(HandleEvent.ReplaceExpressions(script.Rules))) continue;
                if (Program.EventsLogEnable)
                    LogFile.Add(Resources.LogMsgScript +
                        (script.Application.Length > 0 ? script.Application + " " : "") +
                        (script.Command.Length > 0 ? script.Command + " " : "") +
                        (script.Device.Length > 0 ? script.Device + " " : "") +
                        (script.Parameters.Length > 0 ? script.Parameters : ""));
                if (script.Delay > 0)
                {
                    var item = new Shedule();
                    item.Id = -1;
                    item.Application = script.Application;
                    item.Command = script.Command;
                    item.Device = script.Device;
                    item.Parameters = script.Parameters;
                    item.Mode = (byte)SheduleMode.Once;
                    item.Period = 0;
                    item.NextTime = DateTime.Now.AddSeconds(script.Delay);
                    SheduleList.Add(item);
                }
                else HandleEvent.Execute(script, (byte)EventMode.Script);
                script.Timer = DateTime.Now.AddSeconds(script.Timeout);
            }
        } //  ChekScripts()

    } // class Events
} // namespace SmartHome
