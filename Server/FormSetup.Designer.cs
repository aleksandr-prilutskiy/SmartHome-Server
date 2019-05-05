namespace SmartHome
{
    partial class FormSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetup));
            this.checkBoxPingToLog = new System.Windows.Forms.CheckBox();
            this.checkBoxMqttToLog = new System.Windows.Forms.CheckBox();
            this.tabControlSetup = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.checkBoxStartMinimized = new System.Windows.Forms.CheckBox();
            this.tabPageMySQL = new System.Windows.Forms.TabPage();
            this.labelMySQLServerPortNote = new System.Windows.Forms.Label();
            this.textBoxMySQLServerPort = new System.Windows.Forms.TextBox();
            this.labelMySQLServerPort = new System.Windows.Forms.Label();
            this.labelMySQLPassword = new System.Windows.Forms.Label();
            this.labelMySQLLogin = new System.Windows.Forms.Label();
            this.labelMySQLDataBase = new System.Windows.Forms.Label();
            this.textBoxMySQLPassword = new System.Windows.Forms.TextBox();
            this.textBoxMySQLLogin = new System.Windows.Forms.TextBox();
            this.textBoxMySQLDataBase = new System.Windows.Forms.TextBox();
            this.textBoxMySQLServerAddr = new System.Windows.Forms.TextBox();
            this.labelMySQLServerAddr = new System.Windows.Forms.Label();
            this.tabPageMQTT = new System.Windows.Forms.TabPage();
            this.textBoxMQTTBrokerPort = new System.Windows.Forms.TextBox();
            this.labelMQTTBrokerPort = new System.Windows.Forms.Label();
            this.labelMQTTBrokerPassword = new System.Windows.Forms.Label();
            this.labelMQTTBrokerUser = new System.Windows.Forms.Label();
            this.textBoxMQTTPassword = new System.Windows.Forms.TextBox();
            this.textBoxMQTTLogin = new System.Windows.Forms.TextBox();
            this.textBoxMQTTBrokerAddr = new System.Windows.Forms.TextBox();
            this.labelMQTTBrokerAddr = new System.Windows.Forms.Label();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.checkBoxNooLiteToLog = new System.Windows.Forms.CheckBox();
            this.checkBoxEventsToLog = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelMQTTBrokerPortNote = new System.Windows.Forms.Label();
            this.tabControlSetup.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageMySQL.SuspendLayout();
            this.tabPageMQTT.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxPingToLog
            // 
            this.checkBoxPingToLog.Location = new System.Drawing.Point(15, 45);
            this.checkBoxPingToLog.Name = "checkBoxPingToLog";
            this.checkBoxPingToLog.Size = new System.Drawing.Size(295, 17);
            this.checkBoxPingToLog.TabIndex = 1;
            this.checkBoxPingToLog.Text = "Отображать результаты опроса устройств";
            this.checkBoxPingToLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxMqttToLog
            // 
            this.checkBoxMqttToLog.Location = new System.Drawing.Point(15, 95);
            this.checkBoxMqttToLog.Name = "checkBoxMqttToLog";
            this.checkBoxMqttToLog.Size = new System.Drawing.Size(295, 17);
            this.checkBoxMqttToLog.TabIndex = 2;
            this.checkBoxMqttToLog.Text = "Отображать получение данных от брокера MQTT";
            this.checkBoxMqttToLog.UseVisualStyleBackColor = true;
            // 
            // tabControlSetup
            // 
            this.tabControlSetup.Controls.Add(this.tabPageGeneral);
            this.tabControlSetup.Controls.Add(this.tabPageMySQL);
            this.tabControlSetup.Controls.Add(this.tabPageMQTT);
            this.tabControlSetup.Controls.Add(this.tabPageLog);
            this.tabControlSetup.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlSetup.Location = new System.Drawing.Point(0, 0);
            this.tabControlSetup.Name = "tabControlSetup";
            this.tabControlSetup.SelectedIndex = 0;
            this.tabControlSetup.Size = new System.Drawing.Size(329, 170);
            this.tabControlSetup.TabIndex = 3;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.checkBoxStartMinimized);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(321, 144);
            this.tabPageGeneral.TabIndex = 2;
            this.tabPageGeneral.Text = "Общие";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartMinimized
            // 
            this.checkBoxStartMinimized.Location = new System.Drawing.Point(15, 20);
            this.checkBoxStartMinimized.Name = "checkBoxStartMinimized";
            this.checkBoxStartMinimized.Size = new System.Drawing.Size(295, 17);
            this.checkBoxStartMinimized.TabIndex = 4;
            this.checkBoxStartMinimized.Text = "Запускать программу в свернутом виде";
            this.checkBoxStartMinimized.UseVisualStyleBackColor = true;
            // 
            // tabPageMySQL
            // 
            this.tabPageMySQL.Controls.Add(this.labelMySQLServerPortNote);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLServerPort);
            this.tabPageMySQL.Controls.Add(this.labelMySQLServerPort);
            this.tabPageMySQL.Controls.Add(this.labelMySQLPassword);
            this.tabPageMySQL.Controls.Add(this.labelMySQLLogin);
            this.tabPageMySQL.Controls.Add(this.labelMySQLDataBase);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLPassword);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLLogin);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLDataBase);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLServerAddr);
            this.tabPageMySQL.Controls.Add(this.labelMySQLServerAddr);
            this.tabPageMySQL.Location = new System.Drawing.Point(4, 22);
            this.tabPageMySQL.Name = "tabPageMySQL";
            this.tabPageMySQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMySQL.Size = new System.Drawing.Size(321, 144);
            this.tabPageMySQL.TabIndex = 1;
            this.tabPageMySQL.Text = "База данных";
            this.tabPageMySQL.UseVisualStyleBackColor = true;
            // 
            // labelMySQLServerPortNote
            // 
            this.labelMySQLServerPortNote.Location = new System.Drawing.Point(190, 39);
            this.labelMySQLServerPortNote.Name = "labelMySQLServerPortNote";
            this.labelMySQLServerPortNote.Size = new System.Drawing.Size(120, 15);
            this.labelMySQLServerPortNote.TabIndex = 10;
            this.labelMySQLServerPortNote.Text = "(по умолчанию = 3306)";
            this.labelMySQLServerPortNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMySQLServerPort
            // 
            this.textBoxMySQLServerPort.Location = new System.Drawing.Point(105, 36);
            this.textBoxMySQLServerPort.Name = "textBoxMySQLServerPort";
            this.textBoxMySQLServerPort.Size = new System.Drawing.Size(80, 20);
            this.textBoxMySQLServerPort.TabIndex = 9;
            // 
            // labelMySQLServerPort
            // 
            this.labelMySQLServerPort.Location = new System.Drawing.Point(10, 39);
            this.labelMySQLServerPort.Name = "labelMySQLServerPort";
            this.labelMySQLServerPort.Size = new System.Drawing.Size(85, 15);
            this.labelMySQLServerPort.TabIndex = 8;
            this.labelMySQLServerPort.Text = "Порт сервера";
            this.labelMySQLServerPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMySQLPassword
            // 
            this.labelMySQLPassword.Location = new System.Drawing.Point(10, 117);
            this.labelMySQLPassword.Name = "labelMySQLPassword";
            this.labelMySQLPassword.Size = new System.Drawing.Size(85, 15);
            this.labelMySQLPassword.TabIndex = 7;
            this.labelMySQLPassword.Text = "Пароль";
            this.labelMySQLPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMySQLLogin
            // 
            this.labelMySQLLogin.Location = new System.Drawing.Point(10, 91);
            this.labelMySQLLogin.Name = "labelMySQLLogin";
            this.labelMySQLLogin.Size = new System.Drawing.Size(85, 15);
            this.labelMySQLLogin.TabIndex = 6;
            this.labelMySQLLogin.Text = "Логин";
            this.labelMySQLLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMySQLDataBase
            // 
            this.labelMySQLDataBase.Location = new System.Drawing.Point(10, 66);
            this.labelMySQLDataBase.Name = "labelMySQLDataBase";
            this.labelMySQLDataBase.Size = new System.Drawing.Size(85, 15);
            this.labelMySQLDataBase.TabIndex = 5;
            this.labelMySQLDataBase.Text = "Название БД";
            this.labelMySQLDataBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxMySQLPassword
            // 
            this.textBoxMySQLPassword.Location = new System.Drawing.Point(105, 114);
            this.textBoxMySQLPassword.Name = "textBoxMySQLPassword";
            this.textBoxMySQLPassword.PasswordChar = '*';
            this.textBoxMySQLPassword.Size = new System.Drawing.Size(196, 20);
            this.textBoxMySQLPassword.TabIndex = 4;
            // 
            // textBoxMySQLLogin
            // 
            this.textBoxMySQLLogin.Location = new System.Drawing.Point(105, 88);
            this.textBoxMySQLLogin.Name = "textBoxMySQLLogin";
            this.textBoxMySQLLogin.Size = new System.Drawing.Size(196, 20);
            this.textBoxMySQLLogin.TabIndex = 3;
            // 
            // textBoxMySQLDataBase
            // 
            this.textBoxMySQLDataBase.Location = new System.Drawing.Point(105, 63);
            this.textBoxMySQLDataBase.Name = "textBoxMySQLDataBase";
            this.textBoxMySQLDataBase.Size = new System.Drawing.Size(196, 20);
            this.textBoxMySQLDataBase.TabIndex = 2;
            // 
            // textBoxMySQLServerAddr
            // 
            this.textBoxMySQLServerAddr.Location = new System.Drawing.Point(105, 10);
            this.textBoxMySQLServerAddr.Name = "textBoxMySQLServerAddr";
            this.textBoxMySQLServerAddr.Size = new System.Drawing.Size(196, 20);
            this.textBoxMySQLServerAddr.TabIndex = 1;
            // 
            // labelMySQLServerAddr
            // 
            this.labelMySQLServerAddr.Location = new System.Drawing.Point(10, 13);
            this.labelMySQLServerAddr.Name = "labelMySQLServerAddr";
            this.labelMySQLServerAddr.Size = new System.Drawing.Size(85, 15);
            this.labelMySQLServerAddr.TabIndex = 0;
            this.labelMySQLServerAddr.Text = "Адрес сервера";
            this.labelMySQLServerAddr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageMQTT
            // 
            this.tabPageMQTT.Controls.Add(this.labelMQTTBrokerPortNote);
            this.tabPageMQTT.Controls.Add(this.textBoxMQTTBrokerPort);
            this.tabPageMQTT.Controls.Add(this.labelMQTTBrokerPort);
            this.tabPageMQTT.Controls.Add(this.labelMQTTBrokerPassword);
            this.tabPageMQTT.Controls.Add(this.labelMQTTBrokerUser);
            this.tabPageMQTT.Controls.Add(this.textBoxMQTTPassword);
            this.tabPageMQTT.Controls.Add(this.textBoxMQTTLogin);
            this.tabPageMQTT.Controls.Add(this.textBoxMQTTBrokerAddr);
            this.tabPageMQTT.Controls.Add(this.labelMQTTBrokerAddr);
            this.tabPageMQTT.Location = new System.Drawing.Point(4, 22);
            this.tabPageMQTT.Name = "tabPageMQTT";
            this.tabPageMQTT.Size = new System.Drawing.Size(321, 144);
            this.tabPageMQTT.TabIndex = 3;
            this.tabPageMQTT.Text = "MQTT";
            this.tabPageMQTT.UseVisualStyleBackColor = true;
            // 
            // textBoxMQTTBrokerPort
            // 
            this.textBoxMQTTBrokerPort.Location = new System.Drawing.Point(105, 36);
            this.textBoxMQTTBrokerPort.Name = "textBoxMQTTBrokerPort";
            this.textBoxMQTTBrokerPort.Size = new System.Drawing.Size(80, 20);
            this.textBoxMQTTBrokerPort.TabIndex = 19;
            // 
            // labelMQTTBrokerPort
            // 
            this.labelMQTTBrokerPort.Location = new System.Drawing.Point(10, 39);
            this.labelMQTTBrokerPort.Name = "labelMQTTBrokerPort";
            this.labelMQTTBrokerPort.Size = new System.Drawing.Size(85, 15);
            this.labelMQTTBrokerPort.TabIndex = 18;
            this.labelMQTTBrokerPort.Text = "Порт сервера";
            this.labelMQTTBrokerPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMQTTBrokerPassword
            // 
            this.labelMQTTBrokerPassword.Location = new System.Drawing.Point(10, 91);
            this.labelMQTTBrokerPassword.Name = "labelMQTTBrokerPassword";
            this.labelMQTTBrokerPassword.Size = new System.Drawing.Size(85, 15);
            this.labelMQTTBrokerPassword.TabIndex = 17;
            this.labelMQTTBrokerPassword.Text = "Пароль";
            this.labelMQTTBrokerPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMQTTBrokerUser
            // 
            this.labelMQTTBrokerUser.Location = new System.Drawing.Point(10, 66);
            this.labelMQTTBrokerUser.Name = "labelMQTTBrokerUser";
            this.labelMQTTBrokerUser.Size = new System.Drawing.Size(85, 15);
            this.labelMQTTBrokerUser.TabIndex = 16;
            this.labelMQTTBrokerUser.Text = "Логин";
            this.labelMQTTBrokerUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxMQTTPassword
            // 
            this.textBoxMQTTPassword.Location = new System.Drawing.Point(105, 88);
            this.textBoxMQTTPassword.Name = "textBoxMQTTPassword";
            this.textBoxMQTTPassword.Size = new System.Drawing.Size(196, 20);
            this.textBoxMQTTPassword.TabIndex = 13;
            // 
            // textBoxMQTTLogin
            // 
            this.textBoxMQTTLogin.Location = new System.Drawing.Point(105, 63);
            this.textBoxMQTTLogin.Name = "textBoxMQTTLogin";
            this.textBoxMQTTLogin.Size = new System.Drawing.Size(196, 20);
            this.textBoxMQTTLogin.TabIndex = 12;
            // 
            // textBoxMQTTBrokerAddr
            // 
            this.textBoxMQTTBrokerAddr.Location = new System.Drawing.Point(105, 10);
            this.textBoxMQTTBrokerAddr.Name = "textBoxMQTTBrokerAddr";
            this.textBoxMQTTBrokerAddr.Size = new System.Drawing.Size(196, 20);
            this.textBoxMQTTBrokerAddr.TabIndex = 11;
            // 
            // labelMQTTBrokerAddr
            // 
            this.labelMQTTBrokerAddr.Location = new System.Drawing.Point(10, 13);
            this.labelMQTTBrokerAddr.Name = "labelMQTTBrokerAddr";
            this.labelMQTTBrokerAddr.Size = new System.Drawing.Size(85, 15);
            this.labelMQTTBrokerAddr.TabIndex = 10;
            this.labelMQTTBrokerAddr.Text = "Адрес сервера";
            this.labelMQTTBrokerAddr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.checkBoxNooLiteToLog);
            this.tabPageLog.Controls.Add(this.checkBoxEventsToLog);
            this.tabPageLog.Controls.Add(this.checkBoxPingToLog);
            this.tabPageLog.Controls.Add(this.checkBoxMqttToLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(321, 144);
            this.tabPageLog.TabIndex = 0;
            this.tabPageLog.Text = "Журнал";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxNooLiteToLog
            // 
            this.checkBoxNooLiteToLog.Location = new System.Drawing.Point(15, 70);
            this.checkBoxNooLiteToLog.Name = "checkBoxNooLiteToLog";
            this.checkBoxNooLiteToLog.Size = new System.Drawing.Size(295, 17);
            this.checkBoxNooLiteToLog.TabIndex = 4;
            this.checkBoxNooLiteToLog.Text = "Отображать обмен данными с nooLite MTRF-64-USB";
            this.checkBoxNooLiteToLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxEventsToLog
            // 
            this.checkBoxEventsToLog.Location = new System.Drawing.Point(15, 20);
            this.checkBoxEventsToLog.Name = "checkBoxEventsToLog";
            this.checkBoxEventsToLog.Size = new System.Drawing.Size(295, 17);
            this.checkBoxEventsToLog.TabIndex = 3;
            this.checkBoxEventsToLog.Text = "Отображать информацию о событиях";
            this.checkBoxEventsToLog.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(82, 172);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(163, 172);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelMQTTBrokerPortNote
            // 
            this.labelMQTTBrokerPortNote.Location = new System.Drawing.Point(190, 39);
            this.labelMQTTBrokerPortNote.Name = "labelMQTTBrokerPortNote";
            this.labelMQTTBrokerPortNote.Size = new System.Drawing.Size(120, 15);
            this.labelMQTTBrokerPortNote.TabIndex = 20;
            this.labelMQTTBrokerPortNote.Text = "(по умолчанию = 1883)";
            this.labelMQTTBrokerPortNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 201);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControlSetup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SmartHome Server - Настройки";
            this.tabControlSetup.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageMySQL.ResumeLayout(false);
            this.tabPageMySQL.PerformLayout();
            this.tabPageMQTT.ResumeLayout(false);
            this.tabPageMQTT.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxPingToLog;
        private System.Windows.Forms.CheckBox checkBoxMqttToLog;
        private System.Windows.Forms.TabControl tabControlSetup;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TabPage tabPageMySQL;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxEventsToLog;
        private System.Windows.Forms.CheckBox checkBoxNooLiteToLog;
        private System.Windows.Forms.Label labelMySQLPassword;
        private System.Windows.Forms.Label labelMySQLLogin;
        private System.Windows.Forms.Label labelMySQLDataBase;
        private System.Windows.Forms.TextBox textBoxMySQLPassword;
        private System.Windows.Forms.TextBox textBoxMySQLLogin;
        private System.Windows.Forms.TextBox textBoxMySQLDataBase;
        private System.Windows.Forms.TextBox textBoxMySQLServerAddr;
        private System.Windows.Forms.Label labelMySQLServerAddr;
        private System.Windows.Forms.TextBox textBoxMySQLServerPort;
        private System.Windows.Forms.Label labelMySQLServerPort;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.CheckBox checkBoxStartMinimized;
        private System.Windows.Forms.TabPage tabPageMQTT;
        private System.Windows.Forms.TextBox textBoxMQTTBrokerPort;
        private System.Windows.Forms.Label labelMQTTBrokerPort;
        private System.Windows.Forms.Label labelMQTTBrokerPassword;
        private System.Windows.Forms.Label labelMQTTBrokerUser;
        private System.Windows.Forms.TextBox textBoxMQTTPassword;
        private System.Windows.Forms.TextBox textBoxMQTTLogin;
        private System.Windows.Forms.TextBox textBoxMQTTBrokerAddr;
        private System.Windows.Forms.Label labelMQTTBrokerAddr;
        private System.Windows.Forms.Label labelMySQLServerPortNote;
        private System.Windows.Forms.Label labelMQTTBrokerPortNote;
    }
}