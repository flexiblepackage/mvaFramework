using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MVAFW.Config;

namespace MVAFW.Common.Entity
{
    public static class eProductSetting
    {
        public static readonly string LogPath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "QC LOG FILE";
        public static string ModelName { get; internal set; }
        public static string PartNumber { get; internal set; }
        public static string FpgaVersion { get; internal set; }
        public static string McuVersion { get; internal set; }
        public static string FirmwareVersion { get; internal set; }
        public static string QcProgramVersion { get; internal set; }
        public static string MacID { get; internal set; }
        public static string ProductDescription { get; internal set; }
        public static string DriverVersion { get; internal set; }
        public static string Engineer { get; internal set; }
        public static string TestInstrument { get; internal set; }
        public static uint CardNumber { get; set; }
        public static int ProfileIndex { get; set; }
        public static string ProfileName { get; set; }
        public static int GlobalCycle { get; set; }
        public static double GlobalCycleDelay { get; set; }
        public static int SchedulerTime { get; set; }
        public static TurnOffType TurnOff { get; set; }
        public static int RestartIndex { get; set; }
        public static displayDataType Display { get; set; }
        public static bool Lock { get; set; }
        public static bool AutoClose { get; set; }
        public static string Result { get; set; }
        public static bool autoClose { get; set; }
        public static string result { get; set; }
        public static int SlotID { get; set; }
        public static string WebPath { get; set; }
        public static string WebID { get; set; }
        public static string WebPassword { get; set; }
        public static string WebDisk { get; set; }


        private static string sn = "";
        public static string SN  
        {
            get
            {
                return sn;
            }
            set
            {
                sn = value;
            }
        }
    }
}
