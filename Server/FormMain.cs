using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SmartHome.Properties;

namespace SmartHome
{
    public partial class FormMain : Form
    {
        public bool EnableExit;
        private static bool _busy;

//===============================================================================================================
// Name...........:	FormMain
// Description....:	Инициализация объекта
// Syntax.........:	new FormMain()
//===============================================================================================================
        public FormMain()
        {
            InitializeComponent();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            labelAbout.Text = String.Format(Resources.AboutText, info.FileVersion);
        } // FormMain()

        private void FormMain_Load(object sender, EventArgs e)
        {
            IniFile.ReadConfig();
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            if (Program.AppWindowPosX + Program.AppWindowWidth > resolution.Width)
                Program.AppWindowPosX = resolution.Width - Program.AppWindowWidth;
            if (Program.AppWindowPosY + Program.AppWindowHeight > resolution.Height)
                Program.AppWindowPosY = resolution.Height - Program.AppWindowHeight;
            Location = new Point(Math.Max(Program.AppWindowPosX, 0), Math.Max(Program.AppWindowPosY, 0));
            Size = new Size(Program.AppWindowWidth, Program.AppWindowHeight);
            if (Program.StartMinimized)
            {
                WindowState = FormWindowState.Minimized;
                OnSizeChanged(null);
            }
            notifyIcon.ContextMenuStrip = contextMenuTray;
            pictureAppLogo.Location = new Point((tabPageAbout.Width - pictureAppLogo.Width) / 2, 10);
            labelAbout.Location = new Point((tabPageAbout.Width - labelAbout.Width) / 2, 110);
            EnableExit = false;
        } // void FormMain_Load()

        protected override sealed void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (WindowState == FormWindowState.Minimized)
            {
                toolTrayMenuItemOpen.Enabled = true;
                ShowInTaskbar = false;
                richTextBoxLog.Text = "";
            }
            else
            {
                toolTrayMenuItemOpen.Enabled = false;
                ShowInTaskbar = true;
                GridViewDevicesColumnName.Width = Width - 170;
                GridViewSensorsColumnName.Width = Width - 290;
                pictureAppLogo.Location = new Point((tabPageAbout.Width - pictureAppLogo.Width) / 2, 10);
                labelAbout.Location = new Point((tabPageAbout.Width - labelAbout.Width) / 2, 110);
            }
        } // void OnSizeChanged()

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!EnableExit)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                return;
            }
            timerEvents.Enabled = false;
        } // void FormMain_FormClosing()

        private void DataGridViewDevicesClick(object sender, DataGridViewCellEventArgs e)
        {
            var txt = GridViewDevices[1, e.RowIndex].Value.ToString();
            MessageBox.Show(txt, Resources.AppName, MessageBoxButtons.OK);
        } // void DataGridViewDevicesClick()

        private void timerPing_Tick(object sender, EventArgs e)
        {
            Devices.PingAll();
        } // void timerPing_Tick()

        private void timerEvents_Tick(object sender, EventArgs e)
        {
            SmartHome.Events.ChekEvents();
            SmartHome.Events.ChekShedule();
            LogFile.SaveFile();
        } // void timerEvents_Tick()

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            var control = sender as TabControl;
            if (control == null) return;
            if (control.SelectedTab == tabPageLog)
            {
                richTextBoxLog.Clear();
                LogFile.LoadFile();
            }
        } // void tabControl_Selected()

        public void WriteToLog(string message)
        {
            if (WindowState == FormWindowState.Minimized) return;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (_busy)
            {
                Thread.Sleep(10);
                if (stopWatch.Elapsed.Milliseconds > 500) break;
            }
            stopWatch.Stop();
            if (_busy) return;
            _busy = true;
            try
            {
                String text = message.Remove(0, 6);
                Color color = Color.Black;
                String msg = text.Remove(0, 10);
                if (msg.Remove(Resources.LogMsgEvent.Length) == Resources.LogMsgEvent) color = Color.Green;
                if (msg.Remove(Resources.LogMsgShedule.Length) == Resources.LogMsgShedule) color = Color.DarkOliveGreen;
                if (msg.Remove(Resources.LogMsgScript.Length) == Resources.LogMsgScript) color = Color.DarkGreen;
                if (msg.Remove(Resources.LogMsgError.Length) == Resources.LogMsgError) color = Color.Red;
                if (msg.Remove(Resources.LogMsgPing.Length) == Resources.LogMsgPing) color = Color.Blue;
                if (msg.Remove(Resources.LogMsgNooLite.Length) == Resources.LogMsgNooLite) color = Color.DarkBlue;
                if (msg.Remove(Resources.LogMsgMQTT.Length) == Resources.LogMsgMQTT) color = Color.Navy;
                richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                richTextBoxLog.SelectionLength = 0;
                richTextBoxLog.SelectionColor = color;
                richTextBoxLog.AppendText(text + Environment.NewLine);
                richTextBoxLog.SelectionColor = richTextBoxLog.ForeColor;
                richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                richTextBoxLog.ScrollToCaret();
            }
            catch (Exception)
            {
                // ignored
            }
            _busy = false;
        } // void WriteToLog(txt, color)

        private void buttonSetup_Click(object sender, EventArgs e)
        {
            var setupForm = new FormSetup();
            setupForm.Owner = this;
            setupForm.ShowDialog();
        } // buttonSetup_Click()

        private void toolTrayMenuItemOpen_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        } // void toolTrayMenuItemOpen_Click()

        private void toolTrayMenuItemExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.TerminateApp, Resources.AppName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            WindowState = FormWindowState.Normal;
            Devices.StopPing();
            MQTT.Disconnect();
            MySQL.Close();
            IniFile.SaveConfig();
            EnableExit = true;
            Application.Exit();
        } // void toolTrayMenuItemOpen_Click

    } // class FormMain
} // namespace SmartHome
