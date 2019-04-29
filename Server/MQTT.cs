using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using SmartHome.Properties;

namespace SmartHome
{
    public class MQTT
    {

        private static MqttClient Client;
        private static string clientId;
        private static DateTime ReconnectTimer;

//===============================================================================================================
// Name...........:	MQTT
// Description....:	Инициализация объекта
// Syntax.........:	new MQTT()
//===============================================================================================================
        public MQTT()
        {
            clientId = "SmartHome Server: " + Guid.NewGuid().ToString("N");
            ReconnectTimer = DateTime.Now;
            Client = null;
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
            if (Client == null)
            {
                if (ReconnectTimer > DateTime.Now) return;
                ReconnectTimer = DateTime.Now.AddMinutes(5);
                try
                {
                    Client = new MqttClient(Program.MqttBrokerAddress, Program.MqttBrokerPort, false,
                        null, null, MqttSslProtocols.None);
                    Client.MqttMsgPublishReceived += MsgPublishReceived;
                }
                catch (Exception)
                {
                    Client = null;
                }
            }
            if ((Client != null) && (!Client.IsConnected))
            {
                try
                {
                    Client.Connect(clientId, Program.MqttUserName, Program.MqttPassword);
                }
                catch (Exception)
                {
                    Client = null;
                }
                if ((Client != null) && (Client.IsConnected))
                {
                    SubscribeToAll();
                    LogFile.Add("Установлено подключение к брокеру MQTT: " +
                        Program.MqttBrokerAddress + ":" + Program.MqttBrokerPort.ToString());
                    Program.AppWindow.pictureBoxConnectMQTT.Image = Resources.green;
                }
                else
                {
                    LogFile.Add(Resources.LogMsgError + "Ошибка подключения к брокеру MQTT");
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
            if (Client == null) return;
            Client.Subscribe(new[] { "#" }, new[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
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
            if (Program.MqttLogEnable) LogFile.Add("MQTT: " + sensor.Topic + " = " + sensor.Value);
        } // void MsgPublishReceived(sender, e)

//===============================================================================================================
// Name...........:	Disconnect
// Description....:	Отключение от брокера MQTT
// Syntax.........:	Disconnect()
//===============================================================================================================
        public static void Disconnect()
        {
            if (Client == null) return;
            Client.Disconnect();
            Client = null;
            ReconnectTimer = DateTime.Now;
        } // void Disconnect()

    } // class
} // namespace SmartHome
