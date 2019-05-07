namespace SmartHome
{
    partial class FormMain
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
        } // void Dispose

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolTrayMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTrayMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerEvents = new System.Windows.Forms.Timer(this.components);
            this.timerPing = new System.Windows.Forms.Timer(this.components);
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.labelAbout = new System.Windows.Forms.Label();
            this.pictureAppLogo = new System.Windows.Forms.PictureBox();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.tabPageSensors = new System.Windows.Forms.TabPage();
            this.GridViewSensors = new System.Windows.Forms.DataGridView();
            this.GridViewSensorsColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridViewSensorsColumnTopic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridViewSensorsColumnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridViewSensorsColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageDevices = new System.Windows.Forms.TabPage();
            this.GridViewDevices = new System.Windows.Forms.DataGridView();
            this.GridViewDevicesColumnState = new System.Windows.Forms.DataGridViewImageColumn();
            this.GridViewDevicesColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridViewDevicesColumnAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.buttonSetup = new System.Windows.Forms.Button();
            this.groupBoxConnections = new System.Windows.Forms.GroupBox();
            this.pictureBoxConnectNooLite = new System.Windows.Forms.PictureBox();
            this.labelConnectNooLite = new System.Windows.Forms.Label();
            this.pictureBoxConnectMQTT = new System.Windows.Forms.PictureBox();
            this.labelConnectMySQL = new System.Windows.Forms.Label();
            this.labelConnectMQTT = new System.Windows.Forms.Label();
            this.pictureBoxConnectMySQL = new System.Windows.Forms.PictureBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuTray.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppLogo)).BeginInit();
            this.tabPageLog.SuspendLayout();
            this.tabPageSensors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewSensors)).BeginInit();
            this.tabPageDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDevices)).BeginInit();
            this.tabPageMain.SuspendLayout();
            this.groupBoxConnections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnectNooLite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnectMQTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnectMySQL)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "SmartHome Server";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuTray
            // 
            this.contextMenuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTrayMenuItemOpen,
            this.toolTrayMenuItemExit});
            this.contextMenuTray.Name = "contextMenuTray";
            this.contextMenuTray.Size = new System.Drawing.Size(226, 48);
            // 
            // toolTrayMenuItemOpen
            // 
            this.toolTrayMenuItemOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolTrayMenuItemOpen.Name = "toolTrayMenuItemOpen";
            this.toolTrayMenuItemOpen.Size = new System.Drawing.Size(225, 22);
            this.toolTrayMenuItemOpen.Text = "Открыть окно программы";
            this.toolTrayMenuItemOpen.Click += new System.EventHandler(this.toolTrayMenuItemOpen_Click);
            // 
            // toolTrayMenuItemExit
            // 
            this.toolTrayMenuItemExit.Name = "toolTrayMenuItemExit";
            this.toolTrayMenuItemExit.Size = new System.Drawing.Size(225, 22);
            this.toolTrayMenuItemExit.Text = "Завершить работу";
            this.toolTrayMenuItemExit.Click += new System.EventHandler(this.toolTrayMenuItemExit_Click);
            // 
            // timerEvents
            // 
            this.timerEvents.Interval = 500;
            this.timerEvents.Tick += new System.EventHandler(this.timerEvents_Tick);
            // 
            // timerPing
            // 
            this.timerPing.Interval = 10000;
            this.timerPing.Tick += new System.EventHandler(this.timerPing_Tick);
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.labelAbout);
            this.tabPageAbout.Controls.Add(this.pictureAppLogo);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(426, 236);
            this.tabPageAbout.TabIndex = 1;
            this.tabPageAbout.Text = "О программе";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // labelAbout
            // 
            this.labelAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelAbout.Location = new System.Drawing.Point(10, 110);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(400, 100);
            this.labelAbout.TabIndex = 1;
            this.labelAbout.Text = "labelAbout";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureAppLogo
            // 
            this.pictureAppLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureAppLogo.Image = global::SmartHome.Properties.Resources.logo;
            this.pictureAppLogo.Location = new System.Drawing.Point(10, 10);
            this.pictureAppLogo.Name = "pictureAppLogo";
            this.pictureAppLogo.Size = new System.Drawing.Size(96, 96);
            this.pictureAppLogo.TabIndex = 2;
            this.pictureAppLogo.TabStop = false;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.richTextBoxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(426, 236);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Журнал";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(420, 230);
            this.richTextBoxLog.TabIndex = 1;
            this.richTextBoxLog.Text = "";
            // 
            // tabPageSensors
            // 
            this.tabPageSensors.Controls.Add(this.GridViewSensors);
            this.tabPageSensors.Location = new System.Drawing.Point(4, 22);
            this.tabPageSensors.Name = "tabPageSensors";
            this.tabPageSensors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSensors.Size = new System.Drawing.Size(426, 236);
            this.tabPageSensors.TabIndex = 4;
            this.tabPageSensors.Text = "Датчики";
            this.tabPageSensors.UseVisualStyleBackColor = true;
            // 
            // GridViewSensors
            // 
            this.GridViewSensors.AllowUserToAddRows = false;
            this.GridViewSensors.AllowUserToDeleteRows = false;
            this.GridViewSensors.AllowUserToResizeColumns = false;
            this.GridViewSensors.AllowUserToResizeRows = false;
            this.GridViewSensors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewSensors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridViewSensorsColumnName,
            this.GridViewSensorsColumnTopic,
            this.GridViewSensorsColumnData,
            this.GridViewSensorsColumnTime});
            this.GridViewSensors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridViewSensors.Location = new System.Drawing.Point(3, 3);
            this.GridViewSensors.MultiSelect = false;
            this.GridViewSensors.Name = "GridViewSensors";
            this.GridViewSensors.ReadOnly = true;
            this.GridViewSensors.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.GridViewSensors.RowHeadersVisible = false;
            this.GridViewSensors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewSensors.Size = new System.Drawing.Size(420, 230);
            this.GridViewSensors.TabIndex = 1;
            // 
            // GridViewSensorsColumnName
            // 
            this.GridViewSensorsColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GridViewSensorsColumnName.HeaderText = "Наименование";
            this.GridViewSensorsColumnName.Name = "GridViewSensorsColumnName";
            this.GridViewSensorsColumnName.ReadOnly = true;
            this.GridViewSensorsColumnName.Width = 160;
            // 
            // GridViewSensorsColumnTopic
            // 
            this.GridViewSensorsColumnTopic.HeaderText = "Топик";
            this.GridViewSensorsColumnTopic.Name = "GridViewSensorsColumnTopic";
            this.GridViewSensorsColumnTopic.ReadOnly = true;
            this.GridViewSensorsColumnTopic.Width = 80;
            // 
            // GridViewSensorsColumnData
            // 
            this.GridViewSensorsColumnData.HeaderText = "Значение";
            this.GridViewSensorsColumnData.Name = "GridViewSensorsColumnData";
            this.GridViewSensorsColumnData.ReadOnly = true;
            this.GridViewSensorsColumnData.Width = 80;
            // 
            // GridViewSensorsColumnTime
            // 
            this.GridViewSensorsColumnTime.HeaderText = "Время";
            this.GridViewSensorsColumnTime.Name = "GridViewSensorsColumnTime";
            this.GridViewSensorsColumnTime.ReadOnly = true;
            this.GridViewSensorsColumnTime.Width = 80;
            // 
            // tabPageDevices
            // 
            this.tabPageDevices.Controls.Add(this.GridViewDevices);
            this.tabPageDevices.Location = new System.Drawing.Point(4, 22);
            this.tabPageDevices.Name = "tabPageDevices";
            this.tabPageDevices.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDevices.Size = new System.Drawing.Size(426, 236);
            this.tabPageDevices.TabIndex = 3;
            this.tabPageDevices.Text = "Устройства";
            this.tabPageDevices.UseVisualStyleBackColor = true;
            // 
            // GridViewDevices
            // 
            this.GridViewDevices.AllowUserToAddRows = false;
            this.GridViewDevices.AllowUserToDeleteRows = false;
            this.GridViewDevices.AllowUserToResizeColumns = false;
            this.GridViewDevices.AllowUserToResizeRows = false;
            this.GridViewDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridViewDevicesColumnState,
            this.GridViewDevicesColumnName,
            this.GridViewDevicesColumnAddr});
            this.GridViewDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridViewDevices.Location = new System.Drawing.Point(3, 3);
            this.GridViewDevices.MultiSelect = false;
            this.GridViewDevices.Name = "GridViewDevices";
            this.GridViewDevices.ReadOnly = true;
            this.GridViewDevices.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.GridViewDevices.RowHeadersVisible = false;
            this.GridViewDevices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewDevices.Size = new System.Drawing.Size(420, 230);
            this.GridViewDevices.TabIndex = 0;
            this.GridViewDevices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewDevicesClick);
            // 
            // GridViewDevicesColumnState
            // 
            this.GridViewDevicesColumnState.HeaderText = "";
            this.GridViewDevicesColumnState.Name = "GridViewDevicesColumnState";
            this.GridViewDevicesColumnState.ReadOnly = true;
            this.GridViewDevicesColumnState.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridViewDevicesColumnState.Width = 20;
            // 
            // GridViewDevicesColumnName
            // 
            this.GridViewDevicesColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GridViewDevicesColumnName.HeaderText = "Наименование";
            this.GridViewDevicesColumnName.Name = "GridViewDevicesColumnName";
            this.GridViewDevicesColumnName.ReadOnly = true;
            this.GridViewDevicesColumnName.Width = 300;
            // 
            // GridViewDevicesColumnAddr
            // 
            this.GridViewDevicesColumnAddr.HeaderText = "Адрес";
            this.GridViewDevicesColumnAddr.Name = "GridViewDevicesColumnAddr";
            this.GridViewDevicesColumnAddr.ReadOnly = true;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.buttonSetup);
            this.tabPageMain.Controls.Add(this.groupBoxConnections);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(426, 236);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Главная";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // buttonSetup
            // 
            this.buttonSetup.Location = new System.Drawing.Point(21, 134);
            this.buttonSetup.Name = "buttonSetup";
            this.buttonSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonSetup.TabIndex = 7;
            this.buttonSetup.Text = "Настройки";
            this.buttonSetup.UseVisualStyleBackColor = true;
            this.buttonSetup.Click += new System.EventHandler(this.buttonSetup_Click);
            // 
            // groupBoxConnections
            // 
            this.groupBoxConnections.Controls.Add(this.pictureBoxConnectNooLite);
            this.groupBoxConnections.Controls.Add(this.labelConnectNooLite);
            this.groupBoxConnections.Controls.Add(this.pictureBoxConnectMQTT);
            this.groupBoxConnections.Controls.Add(this.labelConnectMySQL);
            this.groupBoxConnections.Controls.Add(this.labelConnectMQTT);
            this.groupBoxConnections.Controls.Add(this.pictureBoxConnectMySQL);
            this.groupBoxConnections.Location = new System.Drawing.Point(6, 6);
            this.groupBoxConnections.Name = "groupBoxConnections";
            this.groupBoxConnections.Size = new System.Drawing.Size(414, 112);
            this.groupBoxConnections.TabIndex = 5;
            this.groupBoxConnections.TabStop = false;
            this.groupBoxConnections.Text = "Состояние подключений";
            // 
            // pictureBoxConnectNooLite
            // 
            this.pictureBoxConnectNooLite.Image = global::SmartHome.Properties.Resources.gray;
            this.pictureBoxConnectNooLite.Location = new System.Drawing.Point(15, 75);
            this.pictureBoxConnectNooLite.Name = "pictureBoxConnectNooLite";
            this.pictureBoxConnectNooLite.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxConnectNooLite.TabIndex = 6;
            this.pictureBoxConnectNooLite.TabStop = false;
            // 
            // labelConnectNooLite
            // 
            this.labelConnectNooLite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelConnectNooLite.Location = new System.Drawing.Point(35, 75);
            this.labelConnectNooLite.Name = "labelConnectNooLite";
            this.labelConnectNooLite.Size = new System.Drawing.Size(371, 22);
            this.labelConnectNooLite.TabIndex = 5;
            this.labelConnectNooLite.Text = "Подключен модуль nooLite MTRF-64";
            // 
            // pictureBoxConnectMQTT
            // 
            this.pictureBoxConnectMQTT.Image = global::SmartHome.Properties.Resources.gray;
            this.pictureBoxConnectMQTT.Location = new System.Drawing.Point(15, 50);
            this.pictureBoxConnectMQTT.Name = "pictureBoxConnectMQTT";
            this.pictureBoxConnectMQTT.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxConnectMQTT.TabIndex = 4;
            this.pictureBoxConnectMQTT.TabStop = false;
            // 
            // labelConnectMySQL
            // 
            this.labelConnectMySQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelConnectMySQL.Location = new System.Drawing.Point(35, 25);
            this.labelConnectMySQL.Name = "labelConnectMySQL";
            this.labelConnectMySQL.Size = new System.Drawing.Size(371, 25);
            this.labelConnectMySQL.TabIndex = 1;
            this.labelConnectMySQL.Text = "Установлено подключение к базе данных";
            // 
            // labelConnectMQTT
            // 
            this.labelConnectMQTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelConnectMQTT.Location = new System.Drawing.Point(35, 50);
            this.labelConnectMQTT.Name = "labelConnectMQTT";
            this.labelConnectMQTT.Size = new System.Drawing.Size(371, 25);
            this.labelConnectMQTT.TabIndex = 3;
            this.labelConnectMQTT.Text = "Установлено подключение к брокеру MQTT";
            // 
            // pictureBoxConnectMySQL
            // 
            this.pictureBoxConnectMySQL.Image = global::SmartHome.Properties.Resources.gray;
            this.pictureBoxConnectMySQL.Location = new System.Drawing.Point(15, 25);
            this.pictureBoxConnectMySQL.Name = "pictureBoxConnectMySQL";
            this.pictureBoxConnectMySQL.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxConnectMySQL.TabIndex = 2;
            this.pictureBoxConnectMySQL.TabStop = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageMain);
            this.tabControl.Controls.Add(this.tabPageDevices);
            this.tabControl.Controls.Add(this.tabPageSensors);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Controls.Add(this.tabPageAbout);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(434, 262);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(434, 262);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "FormMain";
            this.Text = "SmartHome Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.contextMenuTray.ResumeLayout(false);
            this.tabPageAbout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppLogo)).EndInit();
            this.tabPageLog.ResumeLayout(false);
            this.tabPageSensors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewSensors)).EndInit();
            this.tabPageDevices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDevices)).EndInit();
            this.tabPageMain.ResumeLayout(false);
            this.groupBoxConnections.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnectNooLite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnectMQTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnectMySQL)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        } // void InitializeComponent

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon; // Значек в трее
        private System.Windows.Forms.ContextMenuStrip contextMenuTray;
        private System.Windows.Forms.ToolStripMenuItem toolTrayMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem toolTrayMenuItemExit;
        public System.Windows.Forms.Timer timerEvents;
        public System.Windows.Forms.Timer timerPing;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.PictureBox pictureAppLogo;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TabPage tabPageSensors;
        public System.Windows.Forms.DataGridView GridViewSensors;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridViewSensorsColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridViewSensorsColumnTopic;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridViewSensorsColumnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridViewSensorsColumnTime;
        private System.Windows.Forms.TabPage tabPageDevices;
        public System.Windows.Forms.DataGridView GridViewDevices;
        private System.Windows.Forms.DataGridViewImageColumn GridViewDevicesColumnState;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridViewDevicesColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridViewDevicesColumnAddr;
        private System.Windows.Forms.TabPage tabPageMain;
        public System.Windows.Forms.PictureBox pictureBoxConnectMQTT;
        private System.Windows.Forms.Label labelConnectMQTT;
        public System.Windows.Forms.PictureBox pictureBoxConnectMySQL;
        private System.Windows.Forms.Label labelConnectMySQL;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.GroupBox groupBoxConnections;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        public System.Windows.Forms.PictureBox pictureBoxConnectNooLite;
        private System.Windows.Forms.Label labelConnectNooLite;
        private System.Windows.Forms.Button buttonSetup;
    } // class FormMain
} // namespace SamrtHome

