using System;
using System.Windows.Forms;

namespace SmartHome
{
    public partial class FormSetup : Form
    {
        public FormSetup()
        {
            InitializeComponent();
            textBoxMySQLServer.Text = IniFile.DatabaseAddress;
            textBoxMySQLDataBase.Text = IniFile.DatabaseName;
            textBoxMySQLLogin.Text = IniFile.DatabaseUser;
            textBoxMySQLPassword.Text = IniFile.DatabasePassword;
            checkBoxEventsToLog.Checked = IniFile.EventsLogEnable;
            checkBoxPingToLog.Checked = IniFile.PingLogEnable;
            checkBoxNooLiteToLog.Checked = IniFile.NooLiteLogEnable;
            checkBoxMqttToLog.Checked = IniFile.MqttLogEnable;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            IniFile.EventsLogEnable = checkBoxEventsToLog.Checked;
            IniFile.PingLogEnable = checkBoxPingToLog.Checked;
            IniFile.NooLiteLogEnable = checkBoxNooLiteToLog.Checked;
            IniFile.MqttLogEnable = checkBoxMqttToLog.Checked;
            Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
