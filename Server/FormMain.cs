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
            IniFile.ReadConfig();
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            if (Program.AppWindowPosX + Program.AppWindowWidth > resolution.Width)
                Program.AppWindowPosX = resolution.Width - Program.AppWindowWidth;
            if (Program.AppWindowPosY + Program.AppWindowHeight > resolution.Height)
                Program.AppWindowPosY = resolution.Height - Program.AppWindowHeight;
            Location = new Point(Math.Max(Program.AppWindowPosX, 0), Math.Max(Program.AppWindowPosY, 0));
            Size = new Size(Program.AppWindowWidth, Program.AppWindowHeight);
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            labelAbout.Text = String.Format(Resources.AboutText, info.FileVersion);
            notifyIcon.ContextMenuStrip = contextMenuTray;
            EnableExit = false;
        } // FormMain()

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
            }
        } // void OnSizeChanged(e)

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!EnableExit)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                return;
            }
            timerEvents.Enabled = false;
        } // void FormMain_FormClosing(sender, e)

        private void DataGridViewDevicesClick(object sender, DataGridViewCellEventArgs e)
        {
            var txt = GridViewDevices[1, e.RowIndex].Value.ToString();
            MessageBox.Show(txt, Resources.AppName, MessageBoxButtons.OK);
        } // void DataGridViewDevicesClick(sender, e)

        private void timerPing_Tick(object sender, EventArgs e)
        {
            Devices.PingAll();
        } // void timerPing_Tick(sender, e)

        private void timerEvents_Tick(object sender, EventArgs e)
        {
            SmartHome.Events.ChekEvents();
            SmartHome.Events.ChekShedule();
            //SmartHome.Events.ChekScripts();
            LogFile.SaveFile();
        } // void timerEvents_Tick(sender, e)

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            var control = sender as TabControl;
            if (control == null) return;
            if (control.SelectedTab == tabPageAbout)
            {
                pictureAppLogo.Location = new Point((tabControl.Width - pictureAppLogo.Width) / 2, 10);
                labelAbout.Width = tabPageAbout.Width - 12;
            }
            else if (control.SelectedTab == tabPageLog)
            {
                richTextBoxLog.Clear();
                LogFile.LoadFile();
            }
        } // void tabControl_Selected(sender, e)

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
                if (text.Remove(0, 10).Remove(7) == "Event: ") color = Color.Green;
                if (text.Remove(0, 10).Remove(15) == "Shedule event: ") color = Color.DarkOliveGreen;
                if (text.Remove(0, 10).Remove(14) == "Script event: ") color = Color.DarkGreen;
                if (text.Remove(0, 10).Remove(Resources.LogMsgError.Length) == Resources.LogMsgError) color = Color.Red;
                if (text.Remove(0, 10).Remove(6) == "Ping: ") color = Color.Blue;
                if (text.Remove(0, 10).Remove(9) == "nooLite: ") color = Color.DarkBlue;
                if (text.Remove(0, 10).Remove(6) == "MQTT: ") color = Color.DeepPink;
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
        }

        private void toolTrayMenuItemOpen_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        } // void toolTrayMenuItemOpen_Click

        private void toolTrayMenuItemExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.TerminateApp, Resources.AppName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            WindowState = FormWindowState.Normal;
            Devices.StopPing();
            MQTT.Disconnect();
            MySql.Close();
            IniFile.SaveConfig();
            EnableExit = true;
            Application.Exit();
        } // void toolTrayMenuItemOpen_Click

    } // class FormMain
} // namespace SmartHome
