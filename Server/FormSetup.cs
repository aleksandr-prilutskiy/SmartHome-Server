﻿using System;
using System.Windows.Forms;

namespace SmartHome
{
    public partial class FormSetup : Form
    {

//===============================================================================================================
// Name...........:	FormSetup
// Description....:	Инициализация объекта
// Syntax.........:	new FormSetup()
//===============================================================================================================
        public FormSetup()
        {
            InitializeComponent();
            checkBoxStartMinimized.Checked  = Program.StartMinimized;
            textBoxMySQLServerAddr.Text     = Program.DatabaseAddress;
            textBoxMySQLServerPort.Text     = Program.DatabasePort.ToString();
            textBoxMySQLDataBase.Text       = Program.DatabaseName;
            textBoxMySQLLogin.Text          = Program.DatabaseUser;
            textBoxMySQLPassword.Text       = Program.DatabasePassword;
            textBoxMQTTBrokerAddr.Text      = Program.MqttBrokerAddress;
            textBoxMQTTBrokerPort.Text      = Program.MqttBrokerPort.ToString();
            textBoxMQTTLogin.Text           = Program.MqttUserName;
            textBoxMQTTPassword.Text        = Program.MqttPassword;
            checkBoxEventsToLog.Checked     = Program.EventsLogEnable;
            checkBoxPingToLog.Checked       = Program.PingLogEnable;
            checkBoxNooLiteToLog.Checked    = Program.NooLiteLogEnable;
            checkBoxMqttToLog.Checked       = Program.MqttLogEnable;
    } // FormSetup()

//===============================================================================================================
// Name...........:	buttonSave_Click
// Description....:	Обработка нажатия кнопки "Сохранить"
// Syntax.........:	buttonSave_Click(sender, e)
//===============================================================================================================
    private void buttonSave_Click(object sender, EventArgs e)
        {
            if ((textBoxMySQLServerAddr.Text != Program.DatabaseAddress) ||
                (textBoxMySQLServerPort.Text != Program.DatabasePort.ToString()) ||
                (textBoxMySQLDataBase.Text != Program.DatabaseName) ||
                (textBoxMySQLLogin.Text != Program.DatabaseUser) ||
                (textBoxMySQLPassword.Text != Program.DatabasePassword))
            {
                Program.DatabaseAddress = textBoxMySQLServerAddr.Text;
                if (!int.TryParse(textBoxMySQLServerPort.Text, out Program.DatabasePort)) Program.DatabasePort = 3306;
                Program.DatabaseName = textBoxMySQLDataBase.Text;
                Program.DatabaseUser = textBoxMySQLLogin.Text;
                Program.DatabasePassword = textBoxMySQLPassword.Text;
                IniFile.WriteString("Database", "Host", Program.DatabaseAddress);
                IniFile.WriteString("Database", "Port", Program.DatabasePort.ToString());
                IniFile.WriteString("Database", "Name", Program.DatabaseName);
                IniFile.WriteString("Database", "User", Program.DatabaseUser);
                IniFile.WriteString("Database", "Password", Program.DatabasePassword);
                MySQL.Close();
                MySQL.Connect();
            }
            if ((textBoxMQTTBrokerAddr.Text != Program.MqttBrokerAddress) ||
                (textBoxMQTTBrokerPort.Text != Program.MqttBrokerPort.ToString()) ||
                (textBoxMQTTLogin.Text != Program.MqttUserName) ||
                (textBoxMQTTPassword.Text != Program.MqttPassword))
            {
                Program.MqttBrokerAddress = textBoxMQTTBrokerAddr.Text;
                if (!int.TryParse(textBoxMQTTBrokerPort.Text, out Program.MqttBrokerPort)) Program.MqttBrokerPort = 1883;
                Program.MqttUserName = textBoxMQTTLogin.Text;
                Program.MqttPassword = textBoxMQTTPassword.Text;
                IniFile.WriteString("MQTT", "Host", Program.MqttBrokerAddress);
                IniFile.WriteString("MQTT", "Port", Program.MqttBrokerPort.ToString());
                IniFile.WriteString("MQTT", "User", Program.MqttUserName);
                IniFile.WriteString("MQTT", "Password", Program.MqttPassword);
                MQTT.Disconnect();
                MQTT.Connect();
            }
            Program.StartMinimized = checkBoxStartMinimized.Checked;
            Program.EventsLogEnable = checkBoxEventsToLog.Checked;
            Program.PingLogEnable = checkBoxPingToLog.Checked;
            Program.NooLiteLogEnable = checkBoxNooLiteToLog.Checked;
            Program.MqttLogEnable = checkBoxMqttToLog.Checked;
            Hide();
        } // void buttonSave_Click()

//===============================================================================================================
// Name...........:	buttonCancel_Click
// Description....:	Обработка нажатия кнопки "Отменить"
// Syntax.........:	buttonCancel_Click(sender, e)
//===============================================================================================================
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        } // void buttonCancel_Click()

    } // class FormSetup
} // namespace SmartHome
