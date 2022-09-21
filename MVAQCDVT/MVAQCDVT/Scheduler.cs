using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

using MVAFW.Common.Output;
using MVAFW.Common.Entity;
using MVAFW.Config;

namespace MVAQCDVT
{
    public partial class Scheduler : Form
    {
        public static RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static readonly string PgmName = "MVAQCDVT";

        public Scheduler()
        {
            InitializeComponent();

            if (eProductSetting.TurnOff == TurnOffType.AutoRestart)
            {
                this.rb_autoRestart.Checked = true;
            }
            else if (eProductSetting.TurnOff == TurnOffType.AutoShoutDown)
            {
                this.rb_autoShutDown.Checked = true;
            }
            else if (eProductSetting.TurnOff == TurnOffType.None)
            {
                this.rb_none.Checked = true;
            }

            this.nud_minutes.Value = eProductSetting.SchedulerTime;
            displayChkAutoStart();

            this.rb_autoRestart.CheckedChanged += new System.EventHandler(this.rb_turnOffType_CheckedChanged);
            this.rb_autoShutDown.CheckedChanged += new System.EventHandler(this.rb_turnOffType_CheckedChanged);
            this.rb_none.CheckedChanged += new System.EventHandler(this.rb_turnOffType_CheckedChanged);            
        }

        private void nud_minutes_ValueChanged(object sender, EventArgs e)
        {
            int miniutes = (int)this.nud_minutes.Value;
            oProductSetting.UpdateSchedulerMiniutes(miniutes, eProductSetting.TurnOff);
            oProductSetting.GetProductSetting();
        }

        private void rb_turnOffType_CheckedChanged(object sender, EventArgs e)
        {
            TurnOffType turnOffType = TurnOffType.None;

            if (rb_none.Checked == true)
            {
                this.nud_minutes.Enabled = false;
                turnOffType = TurnOffType.None;
                rkApp.DeleteValue(PgmName, false);
            }
            else if (rb_autoRestart.Checked == true)
            {
                this.nud_minutes.Enabled = true;
                turnOffType = TurnOffType.AutoRestart;
                rkApp.SetValue(PgmName, Application.ExecutablePath.ToString() + " start");
            }
            else if (rb_autoShutDown.Checked == true)
            {
                this.nud_minutes.Enabled = true;
                turnOffType = TurnOffType.AutoShoutDown;
                rkApp.SetValue(PgmName, Application.ExecutablePath.ToString() + " start");
            }

            oProductSetting.UpdateSchedulerMiniutes(eProductSetting.SchedulerTime, turnOffType);
            oProductSetting.GetProductSetting();

            displayChkAutoStart();
        }

        private void displayChkAutoStart()
        {
            if (rkApp.GetValue(PgmName) == null)
            {
                this.chk_autoStartWhenBoot.Checked = false;
            }
            else
            {
                this.chk_autoStartWhenBoot.Checked = true;
            }
        }

        private void chk_autoStartWhenBoot_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_autoStartWhenBoot.Checked == true)
            {
                rkApp.SetValue(PgmName, Application.ExecutablePath.ToString() + " start");
            }
            else
            {
                rkApp.DeleteValue(PgmName, false);
            }
        }
    }
}
