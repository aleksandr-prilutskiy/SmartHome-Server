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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMySQL = new System.Windows.Forms.TabPage();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.checkBoxNooLiteToLog = new System.Windows.Forms.CheckBox();
            this.checkBoxEventsToLog = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelMySQLServer = new System.Windows.Forms.Label();
            this.textBoxMySQLServer = new System.Windows.Forms.TextBox();
            this.textBoxMySQLDataBase = new System.Windows.Forms.TextBox();
            this.textBoxMySQLLogin = new System.Windows.Forms.TextBox();
            this.textBoxMySQLPassword = new System.Windows.Forms.TextBox();
            this.labelMySQLDataBase = new System.Windows.Forms.Label();
            this.labelMySQLLogin = new System.Windows.Forms.Label();
            this.labelMySQLPassword = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageMySQL.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxPingToLog
            // 
            this.checkBoxPingToLog.AutoSize = true;
            this.checkBoxPingToLog.Location = new System.Drawing.Point(15, 45);
            this.checkBoxPingToLog.Name = "checkBoxPingToLog";
            this.checkBoxPingToLog.Size = new System.Drawing.Size(243, 17);
            this.checkBoxPingToLog.TabIndex = 1;
            this.checkBoxPingToLog.Text = "Отображать результаты опроса устройств";
            this.checkBoxPingToLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxMqttToLog
            // 
            this.checkBoxMqttToLog.AutoSize = true;
            this.checkBoxMqttToLog.Location = new System.Drawing.Point(15, 95);
            this.checkBoxMqttToLog.Name = "checkBoxMqttToLog";
            this.checkBoxMqttToLog.Size = new System.Drawing.Size(276, 17);
            this.checkBoxMqttToLog.TabIndex = 2;
            this.checkBoxMqttToLog.Text = "Отображать получение данных от брокера MQTT";
            this.checkBoxMqttToLog.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageMySQL);
            this.tabControl1.Controls.Add(this.tabPageLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(329, 166);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPageMySQL
            // 
            this.tabPageMySQL.Controls.Add(this.labelMySQLPassword);
            this.tabPageMySQL.Controls.Add(this.labelMySQLLogin);
            this.tabPageMySQL.Controls.Add(this.labelMySQLDataBase);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLPassword);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLLogin);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLDataBase);
            this.tabPageMySQL.Controls.Add(this.textBoxMySQLServer);
            this.tabPageMySQL.Controls.Add(this.labelMySQLServer);
            this.tabPageMySQL.Location = new System.Drawing.Point(4, 22);
            this.tabPageMySQL.Name = "tabPageMySQL";
            this.tabPageMySQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMySQL.Size = new System.Drawing.Size(321, 140);
            this.tabPageMySQL.TabIndex = 1;
            this.tabPageMySQL.Text = "База данных";
            this.tabPageMySQL.UseVisualStyleBackColor = true;
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
            this.tabPageLog.Size = new System.Drawing.Size(321, 140);
            this.tabPageLog.TabIndex = 0;
            this.tabPageLog.Text = "Журнал";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxNooLiteToLog
            // 
            this.checkBoxNooLiteToLog.AutoSize = true;
            this.checkBoxNooLiteToLog.Location = new System.Drawing.Point(15, 70);
            this.checkBoxNooLiteToLog.Name = "checkBoxNooLiteToLog";
            this.checkBoxNooLiteToLog.Size = new System.Drawing.Size(292, 17);
            this.checkBoxNooLiteToLog.TabIndex = 4;
            this.checkBoxNooLiteToLog.Text = "Отображать обмен данными с nooLite MTRF-64-USB";
            this.checkBoxNooLiteToLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxEventsToLog
            // 
            this.checkBoxEventsToLog.AutoSize = true;
            this.checkBoxEventsToLog.Location = new System.Drawing.Point(15, 20);
            this.checkBoxEventsToLog.Name = "checkBoxEventsToLog";
            this.checkBoxEventsToLog.Size = new System.Drawing.Size(217, 17);
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
            // labelMySQLServer
            // 
            this.labelMySQLServer.Location = new System.Drawing.Point(15, 13);
            this.labelMySQLServer.Name = "labelMySQLServer";
            this.labelMySQLServer.Size = new System.Drawing.Size(80, 15);
            this.labelMySQLServer.TabIndex = 0;
            this.labelMySQLServer.Text = "Адрес сервера";
            this.labelMySQLServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxMySQLServer
            // 
            this.textBoxMySQLServer.Location = new System.Drawing.Point(105, 10);
            this.textBoxMySQLServer.Name = "textBoxMySQLServer";
            this.textBoxMySQLServer.Size = new System.Drawing.Size(196, 20);
            this.textBoxMySQLServer.TabIndex = 1;
            // 
            // textBoxMySQLDataBase
            // 
            this.textBoxMySQLDataBase.Location = new System.Drawing.Point(105, 36);
            this.textBoxMySQLDataBase.Name = "textBoxMySQLDataBase";
            this.textBoxMySQLDataBase.Size = new System.Drawing.Size(196, 20);
            this.textBoxMySQLDataBase.TabIndex = 2;
            // 
            // textBoxMySQLLogin
            // 
            this.textBoxMySQLLogin.Location = new System.Drawing.Point(105, 62);
            this.textBoxMySQLLogin.Name = "textBoxMySQLLogin";
            this.textBoxMySQLLogin.Size = new System.Drawing.Size(196, 20);
            this.textBoxMySQLLogin.TabIndex = 3;
            // 
            // textBoxMySQLPassword
            // 
            this.textBoxMySQLPassword.Location = new System.Drawing.Point(105, 88);
            this.textBoxMySQLPassword.Name = "textBoxMySQLPassword";
            this.textBoxMySQLPassword.PasswordChar = '*';
            this.textBoxMySQLPassword.Size = new System.Drawing.Size(196, 20);
            this.textBoxMySQLPassword.TabIndex = 4;
            // 
            // labelMySQLDataBase
            // 
            this.labelMySQLDataBase.Location = new System.Drawing.Point(15, 38);
            this.labelMySQLDataBase.Name = "labelMySQLDataBase";
            this.labelMySQLDataBase.Size = new System.Drawing.Size(80, 15);
            this.labelMySQLDataBase.TabIndex = 5;
            this.labelMySQLDataBase.Text = "Название";
            this.labelMySQLDataBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMySQLLogin
            // 
            this.labelMySQLLogin.Location = new System.Drawing.Point(15, 63);
            this.labelMySQLLogin.Name = "labelMySQLLogin";
            this.labelMySQLLogin.Size = new System.Drawing.Size(80, 15);
            this.labelMySQLLogin.TabIndex = 6;
            this.labelMySQLLogin.Text = "Логин";
            this.labelMySQLLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMySQLPassword
            // 
            this.labelMySQLPassword.Location = new System.Drawing.Point(15, 88);
            this.labelMySQLPassword.Name = "labelMySQLPassword";
            this.labelMySQLPassword.Size = new System.Drawing.Size(80, 15);
            this.labelMySQLPassword.TabIndex = 7;
            this.labelMySQLPassword.Text = "Пароль";
            this.labelMySQLPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 201);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SmartHome Server - Настройки";
            this.tabControl1.ResumeLayout(false);
            this.tabPageMySQL.ResumeLayout(false);
            this.tabPageMySQL.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxPingToLog;
        private System.Windows.Forms.CheckBox checkBoxMqttToLog;
        private System.Windows.Forms.TabControl tabControl1;
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
        private System.Windows.Forms.TextBox textBoxMySQLServer;
        private System.Windows.Forms.Label labelMySQLServer;
    }
}