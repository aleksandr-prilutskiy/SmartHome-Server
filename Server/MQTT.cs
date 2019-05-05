using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using SmartHome.Properties;

namespace SmartHome
{
    // ReSharper disable once InconsistentNaming
    public class MQTT
    {

        private static MqttClient _client;
        private static string _clientId;
        private static DateTime _reconnectTimer;

//===============================================================================================================
// Name...........:	MQTT
// Description....:	Инициализация объекта
// Syntax.........:	new MQTT()
//===============================================================================================================
        public MQTT()
        {
            _clientId = "SmartHome Server: " + Guid.NewGuid().ToString("N");
            _reconnectTimer = DateTime.Now;
            _client = null;
            Connect();
        } // MQTT()

//===============================================================================================================
// Name...........:	Connect
// Description....:	Подключение к брокеру MQTT
// Syntax.........:	Connect()
//===============================================================================================================
        public static void Connect()
        {
            if (Program.MqttBrokerAddress == "") return;
            if (_client == null)
            {
                if (_reconnectTimer > DateTime.Now) return;
                _reconnectTimer = DateTime.Now.AddMinutes(5);
                try
                {
                    _client = new MqttClient(Program.MqttBrokerAddress, Program.MqttBrokerPort, false,
                        null, null, MqttSslProtocols.None);
                    _client.MqttMsgPublishReceived += MsgPublishReceived;
                }
                catch (Exception)
                {
                    _client = null;
                }
            }
            if ((_client != null) && (!_client.IsConnected))
            {
                try
                {
                    _client.Connect(_clientId, Program.MqttUserName, Program.MqttPassword);
                }
                catch (Exception)
                {
                    _client = null;
                }
                if ((_client != null) && (_client.IsConnected))
                {
                    SubscribeToAll();
                    LogFile.Add(Resources.LogMsgMQTT + "установлено подключение к брокеру MQTT: " +
                        Program.MqttBrokerAddress + ":" + Program.MqttBrokerPort.ToString());
                    Program.AppWindow.pictureBoxConnectMQTT.Image = Resources.green;
                }
                else
                {
                    LogFile.Add(Resources.LogMsgError + "ошибка подключения к брокеру MQTT");
                    Program.AppWindow.pictureBoxConnectMQTT.Image = Resources.gray;
                }
            }
        } // void Connect()

//===============================================================================================================
// Name...........:	SubscribeToAll
// Description....:	Подписка на все топики брокера MQTT
// Syntax.........:	SubscribeToAll()
//===============================================================================================================
        public static void SubscribeToAll()
        {
            if (_client == null) return;
            _client.Subscribe(new[] { "#" }, new[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        } // void SubscribeToAll()

//===============================================================================================================
// Name...........:	MsgPublishReceived
// Description....:	Обработка сообщения по подписке от брокера MQTT
// Syntax.........:	MsgPublishReceived()
//===============================================================================================================
        private static void MsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            byte[] message = e.Message;
            Sensors.Sensor sensor = Sensors.Find(e.Topic);
            if (sensor == null) return;
            sensor.Value = Encoding.UTF8.GetString(message);
            Sensors.SaveToDatabase(sensor);
            if (Program.MqttLogEnable) LogFile.Add(Resources.LogMsgMQTT + sensor.Topic + " = " + sensor.Value);
        } // void MsgPublishReceived(sender, e)

//===============================================================================================================
// Name...........:	Disconnect
// Description....:	Отключение от брокера MQTT
// Syntax.........:	Disconnect()
//===============================================================================================================
        public static void Disconnect()
        {
            if (_client == null) return;
            _client.Disconnect();
            _client = null;
            _reconnectTimer = DateTime.Now;
        } // void Disconnect()

    } // class
} // namespace SmartHome
