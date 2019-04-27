using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using SmartHome.Properties;

namespace SmartHome
{
    // Объект для работы с устройствами nooLite
    // ReSharper disable once InconsistentNaming
    class nooLite
    {
        private enum Mode : byte // Режим работы адаптера MTRF-64-USB
        {
          //TX      = 0,    // режим nooLite TX
          //RX      = 1,    // режим nooLite RX
            Txf     = 2,    // режим nooLite-F TX
          //RXF     = 3,    // режим nooLite-F RX
            Srv     = 4,    // сервисный режим работы с nooLite-F
          //Update  = 5,    // режим обновления ПО
        } // enum Mode

        private enum Tx : byte // Описание данных, отправляемых адаптеру MTRF-64-USB
        {
            St      = 0,    // стартовый байт(значение всегда = 171)
            Mode    = 1,    // режим работы адаптера
            Ctr     = 2,    // управление адаптером
            Res     = 3,    // зарезервирован, не используется
            Ch      = 4,    // адрес канала, ячейки привязки
            Cmd     = 5,    // код команды
            Fmt     = 6,    // формат
            D0      = 7,    // байт данных 0
            D1      = 8,    // байт данных 1
            D2      = 9,    // байт данных 2
            D3      = 10,   // байт данных 3
            Id0     = 11,   // идентификатор блока, бит 31…24
            Id1     = 12,   // идентификатор блока, бит 23…16
            Id2     = 13,   // идентификатор блока, бит 15…8
            Id3     = 14,   // идентификатор блока, бит 7…0
            Crc     = 15,   // контрольная сумма(младший байт суммы первых 15 байт)
            Sp      = 16,   // стоповый байт(значение всегда = 172)
        } // enum Tx

        private enum Rx : byte // Описание данных, получаемых с адаптера MTRF-64-USB
        {
            St      = 0,    // стартовый байт(значение всегда = 173)
          //Mode    = 1,    // режим работы адаптера
          //Ctr     = 2,    // код ответа
          //Togl    = 3,    // количество оставшихся ответов от адаптера, значение TOGL
            Ch      = 4,    // адрес канала, ячейки привязки
            Cmd     = 5,    // код команды
            Fmt     = 6,    // формат
          //D0      = 7,    // байт данных 0
          //D1      = 8,    // байт данных 1
            D2      = 9,    // байт данных 2
          //D3      = 10,   // байт данных 3
          //Id0     = 11,   // идентификатор блока, бит 31…24
          //Id1     = 12,   // идентификатор блока, бит 23…16
          //Id2     = 13,   // идентификатор блока, бит 15…8
          //Id3     = 14,   // идентификатор блока, бит 7…0
            Crc     = 15,   // контрольная сумма(младший байт суммы первых 15 байт)
            Sp      = 16,   // стоповый байт(значение всегда = 174)
        } // enum Rx

        private enum Command : byte // Список команд:
        {
            Off             = 0,    // Выключить нагрузку
          //BrightDown      = 1,    // Запускает плавное понижение яркости
            On              = 2,    // Включить нагрузку
          //BrightUp        = 3,    // Запускает плавное повышение яркости
            Switch          = 4,    // Включает или выключает нагрузку
          //BrightBack      = 5,    // Запускает плавное изменение яркости в обратном направлении
          //SetBrightness   = 6,    // Установить заданную в расширении команды яркость
            LoadPreset      = 7,    // Вызвать записанный сценарий
          //SavePreset      = 8,    // Записать сценарий в память
          //Unbind          = 9,    // Стирание адреса управ. устройства из памяти исполнит.
          //StopReg         = 10,   // Прекращает действие команд Bright_Down, Bright_Up, Bright_Back
          //BrightStepDown  = 11,   // Понизить яркость на шаг
          //BrightStepUp    = 12,   // Повысить яркость на шаг
          //BrightReg       = 13,   // Запускает плавное изменение яркости
          //Bind            = 15,   // Сообщает исполнительному устройству об активации режима привязки
          //RollColour      = 16,   // Запускает плавное изменение цвета в RGB-контроллере по радуге
          //SwitchColour    = 17,   // Переключение между стандартными цветами в RGB-контроллере
          //SwitchMode      = 18,   // Переключение между режимами RGB-контроллера
          //SpeedModeBack   = 19,   // Запускает изменение скорости работы режимов RGB контроллера в обратном направлении
          //BatteryLow      = 20,   // У устройства, которое передало данную команду, разрядился элемент питания
          //SensTempHumi    = 21,   // Передает данные о температуре, влажности и состоянии элементов
            TemporaryOn     = 25,   // Включить свет на заданное время (в 5-секундных тактах)
          //Modes           = 26,   // Установка режимов работы исполнительного устройства
            ReadState       = 128,  // Получение состояния исполнительного устройства
          //WriteState      = 129,  // Установка состояния исполнительного устройства
            SendState       = 130,  // Ответ от исполнительного устройства
          //Service         = 131,  // Включение сервисного режима на заранее привязанном устройстве
          //ClearMemory     = 132,  // Очистка памяти устройства nooLite
        } // enum Command

        private const int BufferSize = 17;
        public static List<byte[]> Incoming;
        private static SerialPort _serial;
        private static byte _modeMtrf64;
        public static String PortMtrf64;
        //public static bool DebugToLog = true;


//===============================================================================================================
// Name...........:	nooLite
// Description....:	Инициализация объекта
// Syntax.........:	new nooLite()
//===============================================================================================================
        public nooLite()
        {
            _serial = new SerialPort();
            Incoming = new List<byte[]>();
            _modeMtrf64 = (byte) Mode.Txf;
            PortMtrf64 = FindPortMtrf();
            if (PortMtrf64.Length == 0)
            {
                LogFile.Add("Error: модуль nooLite MTRF-64-USB не обнаружен");
                return;
            }
            var packet = new Byte[BufferSize];
            for (int n = 0; n < BufferSize; n++) packet[n] = 0;
            Incoming.Add(packet);
            _serial.BaudRate = 9600;
            _serial.Parity = Parity.None;
            _serial.StopBits = StopBits.One;
            _serial.DataBits = 8;
            _serial.Handshake = Handshake.None;
            _serial.DataReceived += DataReceivedHandler;
            _serial.Open();
            if (_serial.IsOpen)
            {
                _serial.DiscardInBuffer();
                LogFile.Add("Модуль nooLite MTRF-64-USB подключен" + PortMtrf64);
                Program.AppWindow.pictureBoxConnectNooLite.Image = Resources.green;
            }
            else
            {
                LogFile.Add("Error: модуль nooLite MTRF-64-USB не подключен");
            }
        } // void nooLite()

//===============================================================================================================
// Name...........:	FindPortMtrf
// Description....:	Поиск порта, к которому подключен модуль nooLite MTRF-64 USB
// Syntax.........:	FindPortMtrf()
// Return value(s):	Success:    - возвращает номер COM-порта в виде строки, например: "COM1"
//                  Failure:    - возвращает пустую строку
//===============================================================================================================
        private static String FindPortMtrf()
        {
            var bufferInit = new Byte[BufferSize];
            for (int i = 0; i < bufferInit.Length; i++) bufferInit[i] = 0;
            bufferInit[(byte) Tx.St] = 171;
            bufferInit[(byte) Tx.Mode] = (byte) Mode.Srv;
            bufferInit[(byte) Tx.Sp] = 172;
            var bufferTx = bufferInit;
            bufferInit[(byte) Tx.Mode] = _modeMtrf64;
            bufferTx[(byte) Tx.Cmd] = (byte) Command.ReadState;
            uint crc = 0;
            for (int i = 0; i < (byte) Tx.Crc; i++) crc += bufferTx[i];
            bufferTx[(byte) Tx.Crc] = (byte) (crc & 0xFF);
            var bufferRx = new Byte[BufferSize];
            _serial.BaudRate = 9600;
            _serial.Parity = Parity.None;
            _serial.StopBits = StopBits.One;
            _serial.DataBits = 8;
            _serial.ReadTimeout = 500;
            _serial.WriteTimeout = 500;
            string[] portnames = SerialPort.GetPortNames();
            foreach (var portname in portnames)
            {
                _serial.PortName = portname;
                try
                {
                    _serial.Open();
                    if (!_serial.IsOpen) continue;
                    _serial.Write(bufferInit, 0, bufferInit.Length);
                    _serial.Write(bufferTx, 0, bufferTx.Length);
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    while (_serial.BytesToRead < BufferSize)
                    {
                        Thread.Sleep(10);
                        if (stopWatch.Elapsed.Milliseconds > 500) break;
                    }
                    stopWatch.Stop();
                    if (_serial.BytesToRead == BufferSize)
                        for (int i = 0; i < bufferRx.Length; i++) bufferRx[i] = (byte)_serial.ReadByte();
                    _serial.Close();
                    if ((bufferRx[(byte)Rx.St] == 173) && (bufferRx[(byte)Rx.Sp] == 174) &&
                        (bufferRx[(byte)Rx.Crc] == GetBufferCrc(bufferRx))) return portname;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return "";
        } // String FindPortMtrf()

//===============================================================================================================
// Name...........:	SendCommand
// Description....:	Отправка команды на устройство
// Syntax.........:	SendCommand(channel, command)
// Parameters.....:	channel     - номер канала
//                  command     - команда (в текстовом виде)
//===============================================================================================================
        public static bool SendCommand(short channel, String command)
        {
            if ((!_serial.IsOpen) || (channel < 0) || (channel > 63)) return false;
            return SendCommand(channel, command, "");
        } // bool SendCommand(channel, command)

//===============================================================================================================
// Name...........:	SendCommand
// Description....:	Отправка команды на устройство
// Syntax.........:	SendCommand(channel, command)
// Parameters.....:	channel     - номер канала
//                  command     - команда (в текстовом виде)
//                  parameters  - дополнительные параметы
//===============================================================================================================
        public static bool SendCommand(short channel, String command, String parameters)
        {
            if ((!_serial.IsOpen) || (channel < 0) || (channel > 63)) return false;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var bufferTx = new Byte[17];
            for (int i = 0; i < bufferTx.Length; i++) bufferTx[i] = 0;
            bufferTx[(byte)Tx.St] = 171;
            bufferTx[(byte)Tx.Mode] = _modeMtrf64;
            bufferTx[(byte)Tx.Ch] = (byte) channel;
            switch (command)
            {
                case "off":
                    bufferTx[(byte) Tx.Cmd] = (byte) Command.Off;
                    break;
                case "on":
                    bufferTx[(byte) Tx.Cmd] = (byte) Command.On;
                    break;
                case "switch":
                    bufferTx[(byte) Tx.Cmd] = (byte) Command.Switch;
                    break;
                case "temporary_on":
                    bufferTx[(byte) Tx.Cmd] = (byte) Command.TemporaryOn;
                    int data;
                    if (!int.TryParse(parameters, out data)) data = 0;
                    data = (int) Math.Ceiling(d: data / 5);
                    if (data < 1) data = 1;
                    if (data < 256)
                    {
                        bufferTx[(byte) Tx.Fmt] = 5;
                        bufferTx[(byte) Tx.D0] = (byte) data;
                    }
                    else
                    {
                        bufferTx[(byte) Tx.Fmt] = 6;
                        bufferTx[(byte) Tx.D0] = (byte) (data & 0x00FF);
                        bufferTx[(byte) Tx.D1] = (byte) ((int) Math.Floor(d: (decimal) (data / 256)) & 0x00FF);
                    }
                    break;
                case "readstate":
                    bufferTx[(byte) Tx.Cmd] = (byte) Command.ReadState;
                    break;
            }
            bufferTx[(byte) Tx.Crc] = GetBufferCrc(bufferTx);
            bufferTx[(byte) Tx.Sp] = 172;
            if (IniFile.NooLiteLogEnable) BufferToLog(bufferTx);
            _serial.Write(bufferTx, 0, bufferTx.Length);
            return true;
        } // bool SendCommand(channel, commandstr, parameters)

//===============================================================================================================
// Name...........:	DataReceivedHandler
// Description....:	Обработчик стобытий при получении данных через com-порт
//===============================================================================================================
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            int bytescount = port.BytesToRead;
            var buffer = Incoming[Incoming.Count - 1];
            for (int i = 0; i < bytescount; i++)
            {
                for (int j = 0; j < BufferSize - 1; j++) buffer[j] = buffer[j + 1];
                buffer[BufferSize - 1] = (byte)port.ReadByte();
                if ((buffer[(byte) Rx.St] == 173) &&
                    (buffer[(byte) Rx.Sp] == 174) &&
                    (buffer[(byte) Rx.Crc] == GetBufferCrc(buffer)))
                {
                    Incoming[Incoming.Count - 1] = buffer;
                    buffer = new Byte[BufferSize];
                    for (int n = 0; n < BufferSize; n++) buffer[n] = 0;
                    Incoming.Add(buffer);
                }
            }
            Incoming[Incoming.Count - 1] = buffer;
            while (Incoming.Count > 1)
            {
                if (IniFile.NooLiteLogEnable) BufferToLog(Incoming[0]);
                if (Incoming[0][(byte)Rx.Cmd] == (byte)Command.SendState)
                {
                    foreach (var device in Devices.DevicesList)
                    {
                        if (device.Type != (byte)Devices.DeviceType.NooLite) continue;
                        String addr = Incoming[0][(byte) Tx.Id0].ToString("X2") +
                            Incoming[0][(byte)Tx.Id1].ToString("X2") +
                            Incoming[0][(byte)Tx.Id2].ToString("X2") +
                            Incoming[0][(byte)Tx.Id3].ToString("X2");
                        if (device.Addr != addr) continue;
                        int state = 0;
                        if ((Incoming[0][(byte)Rx.Fmt] == 0) && (Incoming[0][(byte)Rx.D2] > 0)) state = 1;
                        Devices.SetState(device, state);
                        break;
                    }
                }
                else
                {
                    Sensors.SaveToDatabase ("@" + Incoming[0][(byte)Rx.Ch].ToString(), Incoming[0][(byte)Rx.Cmd].ToString());
                }
                Incoming.RemoveAt(0);
            }
        } // void DataReceivedHandler(sender, e)

//===============================================================================================================
// Name...........:	Close
// Description....:	Закрытие com-порта
// Syntax.........:	Close()
//===============================================================================================================
        public static void Close()
        {
            if (!_serial.IsOpen) return;
            _serial.Close();
        } // void Close()

//===============================================================================================================
// Name...........:	GetBufferCrc
// Description....:	Вычисление контрольной суммы (CRC) в массиве приема-передачи данных
// Syntax.........:	GetBufferCrc(Byte[] buffer)
// Parameters.....:	buffer      - массиве приема-передачи данных
// Return value(s):             - возвращает младший байт от суммы первых 15 байт в массиве
//===============================================================================================================
        private static byte GetBufferCrc(Byte[] buffer)
        {
            int crc = 0;
            for (int i = 0; i < (int)Rx.Crc; i++) crc += buffer[i];
            return (byte)(crc & 0x00FF);
        } // byte GetBufferCrc(Byte[])

//===============================================================================================================
// Name...........:	BufferToLog
// Description....:	Вывод массива приема-передачи данных в журнал работы программы
// Syntax.........:	BufferToLog(Byte[] buffer)
// Parameters.....:	buffer      - массиве приема-передачи данных
//===============================================================================================================
        public static void BufferToLog(Byte[] buffer)
        {
            String s = "nooLite: ";
            if (buffer[(byte)Tx.St] == 171) s += "OUT>>";
            else if (buffer[(byte)Tx.St] == 173) s += "IN<<";
            else
            {
                for (int i = 0; i < buffer.Length; i++) s += buffer[i].ToString() + ",";
                LogFile.Add(s);
                return;
            }
            s += buffer[(byte)Tx.St].ToString() + " |MODE=" + buffer[(byte)Tx.Mode].ToString() + "|CTR=" + buffer[(byte)Tx.Ctr].ToString() + "|";
            if (buffer[(byte)Tx.St] == 171) s += "RES=" + buffer[(byte)Tx.Res].ToString() + "|";
            if (buffer[(byte)Tx.St] == 173) s += "TOGL=" + buffer[(byte)Tx.Res].ToString() + "|";
            s += "CH=" + buffer[(byte)Tx.Ch].ToString() + "|CMD=" + buffer[(byte)Tx.Cmd].ToString() + "|FMT=" + buffer[(byte)Tx.Fmt].ToString() + " |";
            s += "D=" + buffer[(byte)Tx.D0].ToString() + " " + buffer[(byte)Tx.D1].ToString() + " " + buffer[(byte)Tx.D2].ToString() + " " + buffer[(byte)Tx.D3].ToString() + "|";
            s += "ID=" + buffer[(byte)Tx.Id0].ToString("X2") + buffer[(byte)Tx.Id1].ToString("X2") + buffer[(byte)Tx.Id2].ToString("X2") + buffer[(byte)Tx.Id3].ToString("X2") + "|";
            s += "CRC=" + buffer[(byte)Tx.Crc].ToString() + "|" + buffer[(byte)Tx.Sp].ToString() + "|";
            LogFile.Add(s);
        } // void BufferToLog(Byte[])

    } // class LogFile
} // namespace SmartHome
