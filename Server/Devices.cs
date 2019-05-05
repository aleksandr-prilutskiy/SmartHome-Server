using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using SmartHome.Properties;

namespace SmartHome
{
    // Объект для работы с устройствами "Умного дома"
    class Devices
    {

        public enum DeviceOption : ushort // Маски флагов свойств устройства
        {
            Ping    = 0x0001,             // Устройство может быть опрошено (ping)
            OffAll  = 0x0008,             // Устройство входит в группу "Выключить все"
        } // enum DeviceOption

        public enum DeviceType : byte     // Типы устройств
        {
          //Unknown = 0,                  // Прочее устройство
            Server  = 1,                  // Сервер
            NooLite = 2,                  // Устройство nooLite
          //TV      = 3,                  // Телевизор
          //CCTV    = 4,                  // Камера видеонаблюдения
        } // enum DeviceType

        public class Device               // Запись об устройстве
        {
            public int Id;                // ID устройства
            public String Name;           // Наименование устройства
            public byte Type;             // Тип устройства
            public String Driver;         // Имя драйвера устройства
            public String Addr;           // Адрес устройства
            public short Channel;         // Канал привязки (для устройств nooLite)
            public ushort Options;        // Опции настройки устройства
            public int State;             // Код состояния устройства
            public Ping PingHandle;       // Указатель на объект Ping
            public int InList;            // Индекс в таблице устройств
        } // class Device
        public static List<Device> DevicesList; // Список устройств

//===============================================================================================================
// Name...........:	Devices
// Description....:	Инициализация объекта
// Syntax.........:	new Devices()
//===============================================================================================================
        public Devices()
        {
            DevicesList = new List<Device>();
            LoadTable();
            Program.AppWindow.timerPing.Enabled = true;
        } // Devices()

//===============================================================================================================
// Name...........:	LoadTable
// Description....:	Загрузка таблицы устройств (devices) из базы данных в список DevicesList
// Syntax.........:	LoadTable()
//===============================================================================================================
        public static void LoadTable()
        {
            var table = MySQL.ReadTable("devices", "id,name,type,driver,addr,options,parameters",
                "(options & " + (ushort)DeviceOption.Ping + ") > 0");
            if (table == null) return;
            foreach (var record in table)
            {
                var item = new Device();
                if (!int.TryParse(record[0], out item.Id)) item.Id = 0;
                item.Name = record[1];
                if (!byte.TryParse(record[2], out item.Type)) item.Type = 0;
                item.Driver = record[3];
                item.Addr = record[4];
                if (!ushort.TryParse(record[5], out item.Options)) item.Options = 0;
                if (!short.TryParse(record[6], out item.Channel)) item.Channel = -1;
                if (item.Type == (byte)DeviceType.NooLite) item.Driver = "nooLite";
                item.State = -1;
                item.PingHandle = null;
                item.InList = Program.AppWindow.GridViewDevices.Rows.Add(Resources.gray, item.Name, item.Addr);
                DevicesList.Add(item);
            }
        } // void LoadTable()

//===============================================================================================================
// Name...........:	Find
// Description....:	Поиск устройства по имени
// Syntax.........:	Find(name)
// Parameters.....:	name        - имя устройства
// Return value(s):	Success:    - найденный экземпляр объекта типа Device
//                  Failure:    - null
//===============================================================================================================
        public static Device Find(String name)
        {
            return DevicesList.FirstOrDefault(device => device.Name == name);
        } // Device Find(name)

//===============================================================================================================
// Name...........:	SetState
// Description....:	Запись состояния устройства в базу данных
// Syntax.........:	SetState(device, state)
//===============================================================================================================
        public static void SetState(Device device, int state)
        {
            device.State = state;
            MySQL.SaveTo("devices", "state,updated", state + ",NOW()", "id = '" + device.Id + "'");
            Program.AppWindow.GridViewDevices[0, device.InList].Value = state > 0
                ? Resources.green
                : Resources.gray;
        } // void SetState(device, state)

//===============================================================================================================
// Name...........:	PingAll
// Description....:	Проверка и запись в базу данных стотояния всех устройств
// Syntax.........:	PingAll()
//===============================================================================================================
        public static void PingAll()
        {
            nooLite.MtrfConnect();
            MQTT.Connect();
            Monitoring.System();
            if (DevicesList.Count == 0) return;
            nooLite.SendCommand(0, "readstate");
            foreach (var device in DevicesList)
            {
                switch (device.Type)
                {
                    case (byte)DeviceType.Server:
                        SetState(device, 1);
                        break;
                    case (byte)DeviceType.NooLite:
                        break;
                    default:
                        if ((device.Options & (ushort)DeviceOption.Ping) > 0) Ping(device);
                        break;
                }
            }
        } // void PingAll()

//===============================================================================================================
// Name...........:	Ping
// Description....:	Проверка стотояния (асинхронный пинг) указанного устройства
// Syntax.........:	Ping(device)
//===============================================================================================================
        private static void Ping(Device device)
        {
            if ((device.PingHandle != null) ||
                ((device.Options & (ushort)DeviceOption.Ping) == 0)) return;
            IPAddress address;
            if (!IPAddress.TryParse(device.Addr, out address)) return;
            device.PingHandle = new Ping();
            device.PingHandle.PingCompleted += PingCompletedCallback;
            PingOptions options = new PingOptions(64, true);
            try
            {
                device.PingHandle.SendAsync(device.Addr, 1000, options);
            }
            catch (Exception)
            {
                // ignored
            }
        } // void Ping(device)

//===============================================================================================================
// Name...........:	PingCompletedCallback
// Description....:	Обработка результатов асинхронного пинга устройства
//===============================================================================================================
        private static void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply == null) return;
            foreach (var device in DevicesList)
            {
                if (device.PingHandle != sender) continue;
                int state = -1;
                if (e.Reply.Status.ToString() == "Success")
                {
                    state = 1;
                    if (Program.PingLogEnable)
                        LogFile.Add(Resources.LogMsgPing + device.Addr + " .. " + e.Reply.RoundtripTime + " ms");
                }
                SetState(device, state);
                device.PingHandle = null;
                break;
            }
        } // void PingCompletedCallback(sender, e)

//===============================================================================================================
// Name...........:	StopPing
// Description....:	Прекращение асинхронного пинга всех устройств
// Syntax.........:	StopPing()
//===============================================================================================================
        public static void StopPing()
        {
            Program.AppWindow.timerPing.Enabled = false;
            foreach (var device in DevicesList)
                if (device.PingHandle != null)
                    device.PingHandle.SendAsyncCancel();
        } // void StopPing()

//===============================================================================================================
// Name...........:	TurnOffAll
// Description....:	Выключение всех устройств (для всех, отмеченных в свойствах соотвествующим битом)
// Syntax.........:	TurnOffAll()
//===============================================================================================================
        public static void TurnOffAll()
        {
            foreach (var device in DevicesList)
            {
                if ((device.State > 0) && ((device.Options & (ushort)DeviceOption.OffAll) > 0))
                {
                    var newevent = new Events.Event();
                    newevent.Application = device.Driver;
                    newevent.Command = "off";
                    newevent.Device = device.Name;
                    HandleEvent.Execute(newevent, (byte)Events.EventMode.Script);
                    Thread.Sleep(500);
                }
            }
        } // void TurnOffAll()

    } // class Devices
} // namespace SmartHome
