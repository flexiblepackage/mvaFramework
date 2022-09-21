using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using MVAFW.DB;
using MVAFW.Common.Entity;
using MVAFW.Config;

namespace MVAFW.Common.Output
{
    public class oProductSetting
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static Error GetProductSetting()
        {
            DataTable dt = null;
            dt = MvaDSManager.MainDB.getProductSetting();

            if (dt.Rows.Count == 1)
            {
                eProductSetting.ModelName = dt.Rows[0]["modelName"].ToString();
                eProductSetting.DriverVersion = dt.Rows[0]["driverVersion"].ToString();
                eProductSetting.Engineer = dt.Rows[0]["engineer"].ToString();
                eProductSetting.FirmwareVersion = dt.Rows[0]["firmwareVersion"].ToString();
                eProductSetting.FpgaVersion = dt.Rows[0]["fpgaVersion"].ToString();
                eProductSetting.MacID = dt.Rows[0]["macId"].ToString();
                eProductSetting.McuVersion = dt.Rows[0]["mcuVersion"].ToString();
                eProductSetting.PartNumber = dt.Rows[0]["partNumber"].ToString();
                eProductSetting.ProductDescription = dt.Rows[0]["productDescription"].ToString();
                eProductSetting.QcProgramVersion = dt.Rows[0]["qcProgramVersion"].ToString();
                eProductSetting.TestInstrument = dt.Rows[0]["testInstrument"].ToString();
                eProductSetting.CardNumber = uint.Parse(dt.Rows[0]["cardNumber"].ToString());
                eProductSetting.ProfileIndex = int.Parse(dt.Rows[0]["profileIndex"].ToString());
                eProductSetting.ProfileName = dt.Rows[0]["profileName"].ToString();
                eProductSetting.GlobalCycle = int.Parse(dt.Rows[0]["globalCycle"].ToString());
                eProductSetting.GlobalCycleDelay = double.Parse(dt.Rows[0]["globalCycleDelay"].ToString());
                eProductSetting.SchedulerTime = int.Parse(dt.Rows[0]["schedulerMinutes"].ToString());
                eProductSetting.TurnOff = (TurnOffType)Enum.Parse(typeof(TurnOffType), dt.Rows[0]["turnOffType"].ToString());
                eProductSetting.RestartIndex = int.Parse(dt.Rows[0]["restartIndex"].ToString());
                eProductSetting.Display =(displayDataType)Enum.Parse(typeof(displayDataType), dt.Rows[0]["dataTypeDisplay"].ToString());
                eProductSetting.Lock = bool.Parse(dt.Rows[0]["lock"].ToString());
                eProductSetting.AutoClose = bool.Parse(dt.Rows[0]["autoClose"].ToString());
            }
            else if (dt.Rows.Count == 0)
            {
                return Error.NoData;
            }
            else
            {
                return Error.DBError;
            }

            return Error.NoError;
        }

        public static Error UpdateProductSetting(string modelName, string fpgaVersion, string mcuVersion, string firmwareVersion, string qcProgramVersion, 
                                                string macId, string productDescription, string driverVersion, string engineer, string testInstrument)
        {

            return MvaDSManager.MainDB.updateProductSetting(modelName, fpgaVersion, mcuVersion, firmwareVersion, qcProgramVersion,
                                                                                   macId, productDescription, driverVersion, engineer, testInstrument);
        }

        public static List<string> GetAllTestItemClass()
        {
            List<string> classNames = new List<string>();
            DataTable dt = MvaDSManager.MainDB.getAllTestItemClass();
            foreach (DataRow dr in dt.Rows)
            {
                classNames.Add(dr["name"].ToString());
            }

            return classNames;
        }

        public static DataTable GetAllProfile()
        {
            return MvaDSManager.MainDB.getAllProfile();
        }

        public static void AddProfile(string profileName)
        {
            Error err = MvaDSManager.MainDB.addProfile(profileName);
            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting addProfile Error");
            }
        }

        public static void DelProfile(int profileIndex)
        {
            int count = 0;
            Error err = MvaDSManager.MainDB.delProfile(profileIndex, out count);
            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting delProfile DB Error");
            }
            else if (count == 0)
            {
                MessageBox.Show("Could not found this profile!");
            }
        }

        public static void UpdateCardNumber(uint cardNumber)
        {
            int count = 0;
            Error err = MvaDSManager.MainDB.updateCardNumber(cardNumber, out count);
            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting updateCardNumber DB Error");
            }
            else if (count == 0)
            {
                MessageBox.Show("Could not found cardNumber DB!");
            }
        }

        public static void UpdateProfile(int profileIndex)
        {
            int count = 0;
            Error err = MvaDSManager.MainDB.updateProfile(profileIndex, out count);
            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting updateProfile DB Error");
            }
            else if (count == 0)
            {
                MessageBox.Show("Could not found profileIndex DB!");
            }
        }

        public static void UpdateGlobalCycle(int globalCycle)
        {
            int count = 0;
            Error err = MvaDSManager.MainDB.updateGlobalCycle(globalCycle, out count);
            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting updateGlobalCycle DB Error");
            }
            else if (count == 0)
            {
                MessageBox.Show("Could not found globalCycle DB!");
            }
        }

        public static void UpdateGlobalCycleDelay(double globalCycleDelay)
        {
            Error err = MvaDSManager.MainDB.updateGlobalCycleDelay(globalCycleDelay);
            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting updateGlobalCycleDelay DB Error");
            }
        }

        public static Error UpdateSchedulerMiniutes(int miniutes, TurnOffType turnOffType)
        {
            Error err = MvaDSManager.MainDB.updateSchedulerTime(miniutes, turnOffType);

            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting updateSchedulerMiniutes DB Error");
            }

            return err;
        }

        public static Error UpdateRestartIndex(int restartIndex)
        {
            Error err = MvaDSManager.MainDB.updateRestartIndex(restartIndex);

            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting updateRestartIndex DB Error");
            }

            return err;
        }

        public static Error UpdatePartNumber(string partNumber)
        {
            Error err = MvaDSManager.MainDB.updatePartNumber(partNumber);

            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting updatePartNumber DB Error");
            }

            return err;
        }

        public static Error UpdateDataTypeDisplay(displayDataType dataTypeDisplay)
        {
            return MvaDSManager.MainDB.updateDataDisplayType(dataTypeDisplay);
        }

        public static void UpdateLock(int Lock)
        {
            Error err = MvaDSManager.MainDB.updateLock(Lock);

            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting UpdateLock DB Error");
            }
        }

        public static void UpdateAutoClose(int autoClose)
        {
            Error err = MvaDSManager.MainDB.updateAutoClose(autoClose);

            if (err != Error.NoError)
            {
                MessageBox.Show("oProductSetting UpdateAutoClose DB Error");
            }
        }

        public static void AttachDatabase(string sDBPath)
        {
            
        }
    }
}
