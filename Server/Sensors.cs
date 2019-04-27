using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using SmartHome.Properties;

namespace SmartHome
{
    // Объект для работы с датчиками "Умного дрма"
    public class Sensors
    {

        public class Sensor         // Запись о датчике
        {
            public uint Id;         // ID датчика
            public String Topic;    // Топик MQTT
            public String Name;     // Наименование датчика
            public String Value;    // Показания датчика
            public int InList;      // Индекс в таблице датчиков
        } // class Sensor
        public static List<Sensor> SensorsList;

        public static string MqttBrokerAddress = "";
        public static int MqttBrokerPort;
        public static string MqttUserName;
        public static string MqttPassword;
        public static MqttClient ClientMqtt;

//===============================================================================================================
// Name...........:	Sensors
// Description....:	Инициализация объекта
// Syntax.........:	new Sensors()
//===============================================================================================================
        public Sensors()
        {
            SensorsList = new List<Sensor>();
            LoadTable();
            MqttConnect();
        } // Sensors()

//===============================================================================================================
// Name...........:	LoadTable
// Description....:	Чтение таблицы с информацией о датчиках из базы данных
// Syntax.........:	LoadTable()
//===============================================================================================================
        public static void LoadTable()
        {
            var table = MySql.ReadTable("sensors");
            if (table == null) return;
            foreach (var record in table)
            {
                var item = new Sensor();
                if (!uint.TryParse(record[0], out item.Id)) item.Id = 0;
                item.Topic = record[1];
                item.Name = record[2];
                item.InList = Program.AppWindow.GridViewSensors.Rows.Add(item.Name, item.Topic);
                SensorsList.Add(item);
            }
        } // void LoadTable()

//===============================================================================================================
// Name...........:	Find
// Description....:	Поиск датчика по имени
// Syntax.........:	Find(topic)
// Parameters.....:	topic       - идентификатор топика
// Return value(s):	Success:    - найденный экземпляр объекта типа Sensor
//                  Failure:    - null
//===============================================================================================================
        public static Sensor Find(String topic)
        {
            return SensorsList.FirstOrDefault(sensor => sensor.Topic == topic);
        } // Sensor Find(name)

//===============================================================================================================
// Name...........:	SystemMonitoring
// Description....:	Мониторинг системных ресурсов
// Syntax.........:	SystemMonitoring()
//===============================================================================================================
        public static void SystemMonitoring()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM WIN32_Processor");
            ManagementObjectCollection mObject = searcher.Get();
            foreach (var item in mObject)
            {
                var obj = (ManagementObject)item;
                SaveToDatabase("cpu_load", obj["LoadPercentage"].ToString());
            }
            searcher = new ManagementObjectSearcher("Select * FROM Win32_OperatingSystem");
            mObject = searcher.Get();
            foreach (var item in mObject)
            {
                var obj = (ManagementObject)item;
                SaveToDatabase("ram_free", obj["TotalVisibleMemorySize"].ToString());
            }
        } // void SystemMonitoring()

//===============================================================================================================
// Name...........:	SaveToDatabase
// Description....:	Сохранение показаний датчика в базу данных
// Syntax.........:	SaveToDatabase(topic, value)
// Parameters.....:	topic       - идентификатор топика
//                  value       - новое значение
//===============================================================================================================
        public static void SaveToDatabase(String topic, String value)
        {
            Sensor sensor = Find(topic);
            if (sensor == null) return;
            sensor.Value = value;
            SaveToDatabase(sensor);
        } // void SaveToDatabase(topic, value)

//===============================================================================================================
// Name...........:	SaveToDatabase
// Description....:	Сохранение показаний датчика в базу данных
// Syntax.........:	SaveToDatabase(sensor)
// Parameters.....:	sensor      - запись типа датчик
//===============================================================================================================
        public static void SaveToDatabase(Sensor sensor)
        {
            MySql.SaveTo("sensors_data", "sensor,value,status", "'" + sensor.Topic + "','" + sensor.Value + "','0'");
            MySql.SaveTo("sensors", "value", sensor.Value, "topic = '" + sensor.Topic + "'");
            //for (int i = 0; i < _appWindow.GridViewSensors.Rows.Count; i++)
            //{
                //if (_appWindow.GridViewSensors[1, i].Value.ToString() != sensor.Topic) continue;
                //_appWindow.GridViewSensors[2, i].Value = sensor.Value;
                //_appWindow.GridViewSensors[3, i].Value = DateTime.Now.ToString("HH:mm:ss");
                //break;
            //}
            Program.AppWindow.GridViewSensors[2, sensor.InList].Value = sensor.Value;
            Program.AppWindow.GridViewSensors[3, sensor.InList].Value = DateTime.Now.ToString("HH:mm:ss");
            Events.ChekScripts(sensor);
        } // void SaveToDatabase(sensor)

        //===============================================================================================================
        // Name...........:	MqttConnect
        // Description....:	Подключение к брокеру MQTT
        // Syntax.........:	MqttConnect()
        //===============================================================================================================
        public static void MqttConnect()
        {
            ClientMqtt = null;
            if (MqttBrokerAddress == "") return;
            try
            {
                ClientMqtt = new MqttClient(MqttBrokerAddress, MqttBrokerPort, false, null);
                ClientMqtt.MqttMsgPublishReceived += MqttMsgPublishReceived;
                //_client.MqttMsgPublished += client_MqttMsgPublished;
                //_client.MqttMsgSubscribed += client_MqttMsgSubscribed;
                //_client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
                string clientId = "MQTT Client: " + Guid.NewGuid().ToString("N");
                ClientMqtt.Connect(clientId, MqttUserName, MqttPassword);
            }
            catch (Exception)
            {
                ClientMqtt = null;
            }
            if (ClientMqtt != null && ClientMqtt.IsConnected)
            {
                MqttSubscribeToAll();
                Program.AppWindow.pictureBoxConnectMQTT.Image = Resources.green;
                LogFile.Add("Установлено подключение к брокеру MQTT: " + MqttBrokerAddress + ":" + MqttBrokerPort.ToString());
            }
            else
            {
                LogFile.Add("Error: Ошибка подключения к брокеру MQTT");
            }
        } // void MqttConnect()

//===============================================================================================================
// Name...........:	MqttReConnect
// Description....:	Переподключение к брокеру MQTT
// Syntax.........:	MqttReConnect()
//===============================================================================================================
        public static void MqttReConnect()
        {
            if ((MqttBrokerAddress == "") || (ClientMqtt == null)) return;
            if (ClientMqtt.IsConnected) return;
            string clientId = "MQTT Client: " + Guid.NewGuid().ToString("N");
            ClientMqtt.Connect(clientId, MqttUserName, MqttPassword);
        } // void MqttReConnect()

//===============================================================================================================
// Name...........:	MqttSubscribeToAll
// Description....:	Подписка на все топики брокера MQTT
// Syntax.........:	MqttSubscribeToAll()
//===============================================================================================================
        public static void MqttSubscribeToAll()
        {
            if (ClientMqtt == null) return;
            ClientMqtt.Subscribe(new[] { "#" }, new[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        } // void MqttSubscribeToAll()

//===============================================================================================================
// Name...........:	MqttMsgPublishReceived
// Description....:	Обработка сообщения по подписке от брокера MQTT
// Syntax.........:	MqttMsgPublishReceived()
//===============================================================================================================
        private static void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            byte[] message = e.Message;
            Sensor sensor = Find(e.Topic);
            if (sensor == null) return;
            sensor.Value = Encoding.UTF8.GetString(message);
            SaveToDatabase(sensor);
            if (IniFile.MqttLogEnable) LogFile.Add("MQTT: " + sensor.Topic + " = " + sensor.Value);
        } // void MqttMsgPublishReceived(sender, e)

        //===============================================================================================================
        // Name...........:	MqttDisconnect
        // Description....:	Отключение от брокера MQTT
        // Syntax.........:	MqttDisconnect()
        //===============================================================================================================
        public static void MqttDisconnect()
        {
            if (ClientMqtt == null) return;
            ClientMqtt.Disconnect();
            ClientMqtt = null;
        } // void MqttDisconnect()

    } // class
} // namespace SmartHome
