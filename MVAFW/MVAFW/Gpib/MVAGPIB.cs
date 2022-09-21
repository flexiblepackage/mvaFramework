using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Windows.Forms;


using MVAFW.API;
//using M1909A;
//using MVAFW.TestItemColls.RS232;

namespace MVAFW.Gpib
{
    public class MVAGPIB
    {
        SerialPort port; //might be implemented to RegisterDevice to store multiple port in the future.
        public string spValue; //should be implemented again in the future
        private ManualResetEvent done = new ManualResetEvent(false);

        #region RegisterDevice and INI

        private Dictionary<string, int> dictDevice = new Dictionary<string, int>();

        internal int RegisterDevice(string deviceType, ushort deviceNum, ushort gpibNum, bool forceRegister)
        {
            string key = deviceType.ToString() + deviceNum.ToString() + gpibNum.ToString();
            int dev;
            if (forceRegister == false)
            {
                if (dictDevice.ContainsKey(key) == false)
                {
                    dev = iniDevice(deviceType, deviceNum, gpibNum);
                    if (dev > 0)
                    {
                        dictDevice.Add(key, dev);
                    }
                }
            }
            else
            {
                dev = iniDevice(deviceType, deviceNum, gpibNum);
                if (dictDevice.ContainsKey(key) == true)
                {
                    dictDevice[key] = dev;
                }
                else
                {
                    dictDevice.Add(key, dev);
                }
            }

            return dictDevice[key];
        }

        private int iniDevice(string deviceType, ushort deviceNum, ushort gpibNum)
        {
            int dev;

            switch (deviceType)
            {
                case "33250":
                    dev = ini33250(deviceNum, gpibNum);
                    break;
                case "81150":
                    dev = ini81150(deviceNum, gpibNum);
                    break;
                default:
                    dev = iniDevice(deviceNum, gpibNum);
                    break;
            }
            return dev;
        }

        private int iniDevice(ushort deviceNum, ushort gpibNum)
        {
            int devDMM = gpibIni(deviceNum, gpibNum);

            gpibClear(devDMM);

            string strWrite = "*RST";
            gpibWrite(devDMM, strWrite);

            return devDMM;
        }

        #endregion

        #region AP 2700
        internal void devAP2700OutputFrequency(double frequency, double vpp)
        {
            AP.GlobalClass APObj = new AP.GlobalClass();

            APObj.Gen().Output = true;
            APObj.Gen().FreqAccuracy = 1;        //High ACC.
            APObj.Gen().set_ChAAmpl("Vpp", vpp); //Amplitude 
            APObj.Gen().set_ChAFreq("Hz", frequency); //Frequency
            APObj.Gen().Config = 3;              //Unbalance Ground
            //APObj.Gen().Config = 1;              //Balance Ground

            Thread.Sleep(1000);
        }

        internal void devAP2700OutputFrequency(double frequency, double vpp, MvaInstrumentController.APOutputConfig output_config)
        {
            AP.GlobalClass APObj = new AP.GlobalClass();

            APObj.Gen().Output = true;
            APObj.Gen().FreqAccuracy = 1;        //High ACC.
            APObj.Gen().set_ChAAmpl("Vpp", vpp); //Amplitude 
            APObj.Gen().set_ChAFreq("Hz", frequency); //Frequency
            APObj.Gen().Config = (short)(int)output_config; //Blance GND

            Thread.Sleep(1000);
        }

        internal void devAP2700_AI_THDN(out double Freq_A, out double Freq_B, out double AMP_A, out double AMP_B, out double THDN_A, out double THDN_B)
        {
            AP.GlobalClass APObj = new AP.GlobalClass();

            APObj.Anlr().FuncMode = 4; // function mode --> "THD+N" 
            APObj.Anlr().FuncDetector = 0; // function detector --> "RMS" 

            APObj.Anlr().FuncInput = 0; // switch to channel A
            Thread.Sleep(3000);
            Freq_A = APObj.Anlr().get_ChAFreqRdg("Hz");
            THDN_A = APObj.Anlr().get_FuncRdg("dB");

            APObj.Anlr().FuncInput = 1; // switch to channel B
            Thread.Sleep(2000);
            Freq_B = APObj.Anlr().get_ChBFreqRdg("Hz");
            THDN_B = APObj.Anlr().get_FuncRdg("dB");

            APObj.Anlr().FuncMode = 0; // function mode --> "Amplitude" 
            APObj.Anlr().FuncDetector = 2; // function detector --> "Peak" 

            APObj.Anlr().FuncInput = 0; // switch to channel A
            Thread.Sleep(1000);
            AMP_A = APObj.Anlr().get_FuncRdg("V");

            APObj.Anlr().FuncInput = 1; // switch to channel B
            Thread.Sleep(1000);
            AMP_B = APObj.Anlr().get_FuncRdg("V");

            Thread.Sleep(1000);
        }


        #endregion

        #region Agilent DMM 34410
        internal double dev33410Meas(ushort deviceNum, ushort gpibNum, MvaInstrumentController.DmmMeasType measType, uint nplc)
        {
            double dmmRead = 0;
            string strWrite;
            int devDMM = RegisterDevice("33410", deviceNum, gpibNum, false);

            strWrite = string.Format("CONF:{0}", measType.ToString(), nplc);
            gpibWrite(devDMM, strWrite);
            
            strWrite = string.Format("SENSE:{0}:NPLC {1}", measType.ToString(), nplc.ToString());
            gpibWrite(devDMM, strWrite);

            strWrite = string.Format("READ?");
            gpibWrite(devDMM, strWrite);

            //int delay = 0;
            //if(measType == MVAInstrumentController.DmmMeasType.FRES)
            //{
            //    delay = 50;
            //}
            //else
            //{
            //    delay = 50;
            //}

            //Thread.Sleep((int)nplc * delay);

            string readMessage = gpibRead(devDMM);
            dmmRead = double.Parse(readMessage.ToString().Trim().Substring(0, readMessage.ToString().IndexOf("\n")));
            
            return dmmRead;
        }

        internal void dev34410Load_Auto(ushort deviceNum, ushort gpibNum, bool LoadImped)
        {
            string strWrite = "";
            int dev34410 = RegisterDevice("34410", deviceNum, gpibNum, false);
            if (LoadImped == true)
            {
                strWrite = "VOLT:IMP:AUTO ON";
            }
            else
            {
                strWrite = "VOLT:IMP:AUTO OFF";
            }
            gpibWrite(dev34410, strWrite);

        }

        internal void dev34410ClsReset(ushort deviceNum, ushort gpibNum)
        {
            int dev34410 = RegisterDevice("34410", deviceNum, gpibNum, false);
            GPIB.SendIFC(0);
            gpibWrite(dev34410, "SYST:PRES");
            gpibWrite(dev34410, "*CLS");
            gpibWrite(dev34410, "*RST");
        }

        internal void dev34410TriggerConfig(ushort deviceNum, ushort gpibNum, ushort triggerCnt)
        {
            int dev34410 = RegisterDevice("34410", deviceNum, gpibNum, false);
            gpibWrite(dev34410, "CONF:VOLT:DC 10, 0.00004");
            gpibWrite(dev34410, "TRIG:COUN " + triggerCnt.ToString());
            gpibWrite(dev34410, "TRIG:SOUR EXT");
            gpibWrite(dev34410, "TRIG:SLOP POS");
            gpibWrite(dev34410, "TRIG:DEL MIN");
            gpibWrite(dev34410, "SAMP:COUN 1");
            gpibWrite(dev34410, "INIT");
        }

        internal List<double> dev34410DMMTransferData(ushort deviceNum, ushort gpibNum, ushort dataCnt)
        {
            int dev34410 = RegisterDevice("34410", deviceNum, gpibNum, false);
            int s = 0;
            StringBuilder DMM_Data = new StringBuilder(600000);
            string strWrite = "";
            strWrite = "FETC?";
            GPIB.ibwrt(dev34410, strWrite, strWrite.Length);
            GPIB.ibrd(dev34410, DMM_Data, DMM_Data.Capacity);

            int indexOfEnd = DMM_Data.ToString().IndexOf('\n');
            if (indexOfEnd < 0)
            {
                throw new Exception("DMM reading count not match");
            }

            List<string> sData = DMM_Data.ToString().Substring(0, indexOfEnd).Split(',').ToList<string>();
            List<double> data = sData.Select(x => double.Parse(x.Replace("\n","").Replace("\\", ""))).ToList();
            if (data.Count != dataCnt)
            {
                throw new Exception("DMM reading count not match");
            }
            return data;

        }




        #endregion

        #region Agilent E3649A
        internal void devE3649AOutputDC(ushort deviceNum, ushort gpibNum, double outputVoltage1, double outputVoltage2)
        {
            int devE3649A = RegisterDevice("E3649A", deviceNum, gpibNum, false);
            string strWrite = string.Empty;
            strWrite = "INST:SEL OUT1;";
            gpibWrite(devE3649A, strWrite);

            strWrite = "VOLTage "+outputVoltage1.ToString()+";";
            gpibWrite(devE3649A, strWrite);

            strWrite = "INST:SEL OUT2;";
            gpibWrite(devE3649A, strWrite);

            strWrite = "VOLTage " + outputVoltage2.ToString() + ";";
            gpibWrite(devE3649A, strWrite);

            strWrite = "OUTPut ON;";

            gpibWrite(devE3649A, strWrite);

            Thread.Sleep(500);
        }
        #endregion

        #region Agilent 33250

        private int ini33250(ushort deviceNum, ushort gpibNum)
        {
            string strWrite;

            int dev33250 = gpibIni(deviceNum, gpibNum);

            gpibClear(dev33250);

            strWrite = "*RST";
            gpibWrite(dev33250, strWrite);

            strWrite = "OUTP:LOAD Inf";    //HZ mode
            gpibWrite(dev33250, strWrite);

            Thread.Sleep(100);

            return dev33250;
        }

        internal short dev33250OutputDC(ushort deviceNum, ushort gpibNum, double voltage)
        {
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            string strWrite = "APPL:DC 1HZ, 1VPP, " + voltage + " V";
            gpibWrite(dev33250, strWrite);

            Thread.Sleep(100);

            return 0;
        }

        internal void dev33250Load_50ohm(ushort deviceNum, ushort gpibNum, bool LoadImped)
        {
            string strWrite = "";
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            if (LoadImped == true)
            {
                strWrite = "OUTP:LOAD 50";
            }
            else
            {
                strWrite = "OUTP:LOAD INF";
            }
            gpibWrite(dev33250, strWrite);

        }

        internal void dev33250Trigger(ushort deviceNum, ushort gpibNum)
        {
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            gpibWrite(dev33250, "TRIG");
        }

        internal void dev33250BurstCycle(ushort deviceNum, ushort gpibNum, int cycle, int triggerSource, double periodSec, bool triggerSlopPositive, bool enable)
        {
            string strWrite = "";
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);

            if (!enable)
            {
                strWrite = "BURS:STAT OFF";
                gpibWrite(dev33250, strWrite);
                return;
            }

            strWrite = "BURS:INT:PER " + periodSec.ToString();
            gpibWrite(dev33250, strWrite);
            Thread.Sleep(100);

            strWrite = "BURS:NCYC " + cycle.ToString();
            gpibWrite(dev33250, strWrite);
            Thread.Sleep(100);

            if (triggerSource == 0)
                strWrite = "TRIG:SOUR IMM";
            else if (triggerSource == 1)
                strWrite = "TRIG:SOUR BUS";
            else
                strWrite = "TRIG:SOUR EXT";
            gpibWrite(dev33250, strWrite);
            Thread.Sleep(100);

            strWrite = triggerSlopPositive ? "TRIG:SLOP POS" : "TRIG:SLOP NEG";
            gpibWrite(dev33250, strWrite);
            Thread.Sleep(100);

            strWrite = enable ? "BURS:STAT ON" : "BURS:STAT OFF";
            gpibWrite(dev33250, strWrite);
            Thread.Sleep(100);

        }

        internal void dev33250BurstOff(ushort deviceNum, ushort gpibNum, bool IsBurstOff)
        {
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            gpibWrite(dev33250, IsBurstOff ? "BURS:STAT ON" : "BURS:STAT OFF");
            Thread.Sleep(100);
        }


        internal void dev33250SyncOff(ushort deviceNum, ushort gpibNum, bool IsSyncOff)
        {
            string strWrite = "";
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            if (IsSyncOff == true)
            {
                strWrite = "OUTP:SYNC OFF";
            }
            else
            {
                strWrite = "OUTP:SYNC ON";
            }
            gpibWrite(dev33250, strWrite);
        }

        internal void dev33250OutputFrequency(ushort deviceNum, ushort gpibNum, double frequency, double vpp, double offsetV)
        {
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            string strWrite = "APPL:SIN " + frequency.ToString() + " HZ, " + vpp + " VPP, " + offsetV.ToString() + " V";
            gpibWrite(dev33250, strWrite);

            Thread.Sleep(500);
        }

        internal void dev33250OutputSqureWave(ushort deviceNum, ushort gpibNum, double frequency, double vpp, double offsetV)
        {
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            string strWrite = "APPL:SQU " + frequency.ToString() + " HZ, " + vpp + " VPP, " + offsetV.ToString() + " V";
            gpibWrite(dev33250, strWrite);

            Thread.Sleep(500);
        }

        internal void dev33250OutputRampWave(ushort deviceNum, ushort gpibNum, double frequency, double vpp, double offsetV)
        {
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            string strWrite = "APPL:RAMP " + frequency.ToString() + " HZ, " + vpp + " VPP, " + offsetV.ToString() + " V";
            gpibWrite(dev33250, strWrite);

            Thread.Sleep(500);
        }

        internal void dev33250SetPulseWave(ushort deviceNum, ushort gpibNum, double pulseperiod, double vpp, double offsetV, double pulsewidth, bool burstmode, uint numofcycles, bool trig, double burstperiod)
        {
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            //string strWrite = "APPL:PULS " + frequency.ToString() + " HZ, " + vpp + " VPP, " + offsetV.ToString() + " V";
            string wrtbuf;

            if (burstmode)
            {
                wrtbuf = "BURS:STAT ON";
                gpibWrite(dev33250, wrtbuf);
                Thread.Sleep(150);
                wrtbuf = "BURS:NCYC " + numofcycles.ToString();
                gpibWrite(dev33250, wrtbuf);
                Thread.Sleep(150);

                if(trig)
                {
                    wrtbuf = "BURSt:MODE TRIG";
                    gpibWrite(dev33250, wrtbuf);
                    Thread.Sleep(150);
                    wrtbuf = "TRIG:SOUR BUS";
                    gpibWrite(dev33250, wrtbuf);
                    Thread.Sleep(150);
                }
                else
                {
                    wrtbuf = "BURSt:INT:PER " + burstperiod.ToString();
                    gpibWrite(dev33250, wrtbuf);
                    Thread.Sleep(150);
                }
            }
            else
            {
                wrtbuf = "BURS:STAT OFF";
                gpibWrite(dev33250, wrtbuf);
                Thread.Sleep(200);
            }

            wrtbuf = "OUTP ON";
            gpibWrite(dev33250, wrtbuf);
            Thread.Sleep(200);

            wrtbuf = "FUNC PULS";
            gpibWrite(dev33250, wrtbuf);
            Thread.Sleep(200);

            wrtbuf = "PULS:PER " + pulseperiod.ToString();
            gpibWrite(dev33250, wrtbuf);
            Thread.Sleep(200);

            wrtbuf = "VOLT " + vpp.ToString(); 
            gpibWrite(dev33250, wrtbuf);
            Thread.Sleep(200);

            wrtbuf = "VOLT:OFFS " + offsetV.ToString();
            gpibWrite(dev33250, wrtbuf);
            Thread.Sleep(200);

            wrtbuf = "PULS:WIDT " + pulsewidth.ToString();
            gpibWrite(dev33250, wrtbuf);
            Thread.Sleep(200);
 
        }

        internal void SendBusTrig(ushort deviceNum, ushort gpibNum)
        {
            int dev33250 = RegisterDevice("33250", deviceNum, gpibNum, false);
            string wrtbuf = "TRIG";
            gpibWrite(dev33250, wrtbuf);
        }

        #endregion

        #region DS360

        internal short devDS360OutputFrequency(ushort deviceNum, ushort gpibNum, double frequency, double vpp, double offsetV, bool forceRegister)
        {
            int devDS360 = RegisterDevice("DS360", deviceNum, gpibNum, forceRegister);
            string strWrite = string.Format("FREQ{0};FUNC0;AMPL{1}VP;OFFS{2}", frequency.ToString(), vpp.ToString(), offsetV.ToString());
            gpibWrite(devDS360, strWrite);

            Thread.Sleep(500);

            return 0;
        }

        #endregion

        #region 5720A
        internal short dev5720AOutputDC(ushort deviceNum, ushort gpibNum, double outputValue, bool current, bool forceRegister)
        {
            int dev5720A = RegisterDevice("5720A", deviceNum, gpibNum, forceRegister);

            string strWrite = string.Format("OUT {0}{1}; OPER", outputValue.ToString(), current == true ? "mA" : "V");
            gpibWrite(dev5720A, strWrite);

            Thread.Sleep(2000);

            return 0;
        }
        #endregion

        #region Agilent 33600A
        internal void dev33600OutputWaveform(ushort deviceNum, ushort gpibNum, ushort sourcefrom, double frequency, double vpp, double offsetV, uint nWavetype)
        {            
            int dev33600 = RegisterDevice("33622", deviceNum, gpibNum, false);
            string sTYpe = getWaveformType(nWavetype);
            string strWrite = "SOURCe" + sourcefrom.ToString();
            strWrite = strWrite + ":APPL:" + sTYpe + " " + frequency.ToString() + "HZ, " + vpp + "VPP, " + offsetV.ToString() + " V";
            gpibWrite(dev33600, strWrite);

            Thread.Sleep(500);
        }

        /*internal void dev33600OutputDC(ushort deviceNum, ushort gpibNum, ushort sourcefrom, double offsetV)
        {
            int dev33600 = RegisterDevice("33600", deviceNum, gpibNum, false);
            string strWrite = "SOURCe" + sourcefrom.ToString();
            strWrite = strWrite + ":APPL:DC 1HZ, 1VPP, " + offsetV.ToString() + " V";
            gpibWrite(dev33600, strWrite);

            Thread.Sleep(500);
        }
         */
        internal void dev33600OutputWaveParameterSet(ushort deviceNum, ushort gpibNum, ushort sourcefrom, uint nWavetype, string sPara, double dPara)
        {
            int dev33600 = RegisterDevice("33600", deviceNum, gpibNum, false);
            string sTYpe = this.getWaveformType(nWavetype);
            string strWrite = "SOURCe" + sourcefrom.ToString();           
            strWrite = strWrite + ":FUNC:" + sTYpe + ":" + sPara + " " + dPara.ToString();
            gpibWrite(dev33600, strWrite);

            Thread.Sleep(500);
        }

        internal void dev33600Load_50ohm(ushort deviceNum, ushort gpibNum, ushort sourcefrom, bool LoadImped)
        {
            int dev33600 = RegisterDevice("33600", deviceNum, gpibNum, false);
            string strWrite = null;
            strWrite = strWrite + "OUTP" + sourcefrom.ToString();

            if (LoadImped == true)
            {
                strWrite = strWrite + ":LOAD 50";
            }
            else
            {
                strWrite = "OUTP:LOAD INF";
            }
            gpibWrite(dev33600, strWrite);

            Thread.Sleep(500);
        }
        internal void dev33600Sync_Off(ushort deviceNum, ushort gpibNum, ushort sourcefrom, bool IsSync_Off)
        {
            int dev33600 = RegisterDevice("33600", deviceNum, gpibNum, false);
            string strWrite = null;
            strWrite = strWrite + "OUTPut" + sourcefrom.ToString();

            if (IsSync_Off == true)
            {
                strWrite = strWrite + ":SYNC OFF";
            }
            else
            {
                strWrite = strWrite + ":SYNC ON";
            }
            gpibWrite(dev33600, strWrite);

            Thread.Sleep(500);
        }
        internal void dev33600BurstCycle(ushort deviceNum, ushort gpibNum, int cycle, int triggerSource, double periodSec, bool triggerSlopPositive, bool enable)
        {
            string strWrite = "";
            int dev33600 = RegisterDevice("33600", deviceNum, gpibNum, false);

            if (!enable)
            {
                strWrite = "BURS:STAT OFF";
                gpibWrite(dev33600, strWrite);
                return;
            }

            strWrite = "BURS:INT:PER " + periodSec.ToString();
            gpibWrite(dev33600, strWrite);
            Thread.Sleep(100);

            strWrite = "BURS:NCYC " + cycle.ToString();
            gpibWrite(dev33600, strWrite);
            Thread.Sleep(100);

            if (triggerSource == 0)
                strWrite = "TRIG:SOUR IMM";
            else if (triggerSource == 1)
                strWrite = "TRIG:SOUR BUS";
            else
                strWrite = "TRIG:SOUR EXT";
            gpibWrite(dev33600, strWrite);
            Thread.Sleep(100);

            strWrite = triggerSlopPositive ? "TRIG:SLOP POS" : "TRIG:SLOP NEG";
            gpibWrite(dev33600, strWrite);
            Thread.Sleep(100);

            strWrite = enable ? "BURS:STAT ON" : "BURS:STAT OFF";
            gpibWrite(dev33600, strWrite);
            Thread.Sleep(100);

        }
        internal void dev33600OutputFrequency(ushort deviceNum, ushort gpibNum, double frequency, double vpp, double offsetV)
        {
            int dev33600 = RegisterDevice("33600", deviceNum, gpibNum, false);
            string strWrite = "APPL:SIN " + frequency.ToString() + " HZ, " + vpp + " VPP, " + offsetV.ToString() + " V";
            gpibWrite(dev33600, strWrite);

            Thread.Sleep(500);
        }
        internal void dev33600BurstOff(ushort deviceNum, ushort gpibNum, bool IsBurstOff)
        {
            int dev33600 = RegisterDevice("33600", deviceNum, gpibNum, false);
            gpibWrite(dev33600, IsBurstOff ? "BURS:STAT ON" : "BURS:STAT OFF");
            Thread.Sleep(100);
        }

        #endregion

        #region Agilent 81150A

        private int ini81150(ushort deviceNum, ushort gpibNum)
        {
            string strWrite;

            int dev81150 = gpibIni(deviceNum, gpibNum);

            gpibClear(dev81150);

            strWrite = "*RST";
            gpibWrite(dev81150, strWrite); //*RST

            strWrite = ":OUTP1:LOAD 1000000";   //1M Load impedance1 
            gpibWrite(dev81150, strWrite);

            strWrite = ":OUTP2:LOAD 1000000";   //1M Load impedance2 
            gpibWrite(dev81150, strWrite);

            strWrite = ":TRACk:FREQuency  OFF";   //disable Coupling between CH1 & CH2
            gpibWrite(dev81150, strWrite);

            Thread.Sleep(100);

            return dev81150;
        }

        internal void dev81150Load_50ohm(ushort deviceNum, ushort gpibNum, int ch, bool LoadImped)
        {
            string strWrite = "";
            int dev81150 = RegisterDevice("81150", deviceNum, gpibNum, false);
            if (LoadImped == true)
            {
                strWrite = ":OUTP" + ch.ToString() + ":IMP:EXT 50";
            }
            else
            {
                strWrite = ":OUTP" + ch.ToString() + ":IMP:EXT MAX";
            }
            gpibWrite(dev81150, strWrite);
        }

        internal short dev81150OutputDC(ushort deviceNum,  ushort gpibNum, int ch, double voltage)
        {
            int dev81150 = RegisterDevice("81150", deviceNum, gpibNum, false);
            string strWrite = ":FUNCtion" + ch.ToString() + " DC";
            gpibWrite(dev81150, strWrite);
            strWrite = ":APPL" + ch.ToString() + ":DC 1HZ, 1VPP, " + voltage + " V";
            gpibWrite(dev81150, strWrite);
            strWrite = ":OUTput" + ch.ToString() + " ON";
            gpibWrite(dev81150, strWrite);

            Thread.Sleep(100);

            return 0;
        }

        internal short dev81150OutputSine(ushort deviceNum, ushort gpibNum, int ch, double frequency, double voltageHigh, double voltageLow, double dutyCycle, bool forceRegister)
        {
            short dev81150 = (short)RegisterDevice("81150", deviceNum, gpibNum, forceRegister);
            string strWrite = "";

            strWrite = ":FUNCtion" + ch.ToString() + " SIN";
            gpibWrite(dev81150, strWrite);

            strWrite = "FREQuency" + ch.ToString() + " " + frequency.ToString() + " HZ";
            gpibWrite(dev81150, strWrite);

            strWrite = ":FUNCtion" + ch.ToString() + ":SQUare:DCYCle " + dutyCycle.ToString();
            gpibWrite(dev81150, strWrite);


            strWrite = ":VOLTage" + ch.ToString() + ":HIGH " + voltageHigh.ToString() + " V";
            gpibWrite(dev81150, strWrite);

            strWrite = ":VOLTage" + ch.ToString() + ":LOW " + voltageLow.ToString() + " V";
            gpibWrite(dev81150, strWrite);

            strWrite = ":OUTput" + ch.ToString() + " ON";
            gpibWrite(dev81150, strWrite);

            strWrite = ":OUTP" + ch.ToString() + ":COMPlement ON";
            gpibWrite(dev81150, strWrite);

            Thread.Sleep(200);

            return dev81150;
        }

        internal short dev81150OutputSquare(ushort deviceNum, ushort gpibNum, int ch, double frequency, double voltageHigh, double voltageLow, double dutyCycle, bool forceRegister)
        {

            int dev81150 = RegisterDevice("81150", deviceNum, gpibNum, forceRegister);
            string strWrite = "";

            strWrite = ":FUNCtion" + ch.ToString() + " SQUare";
            gpibWrite(dev81150, strWrite);
            
            strWrite = "FREQuency" + ch.ToString() + " " + frequency.ToString() + " HZ";
            gpibWrite(dev81150, strWrite);

            strWrite = ":FUNCtion" + ch.ToString() + ":SQUare:DCYCle " + dutyCycle.ToString();
            gpibWrite(dev81150, strWrite);


            strWrite = ":VOLTage" + ch.ToString() + ":HIGH " + voltageHigh.ToString() + " V";
            gpibWrite(dev81150, strWrite);

            strWrite = ":VOLTage" + ch.ToString() + ":LOW " + voltageLow.ToString() + " V";
            gpibWrite(dev81150, strWrite);

            strWrite = ":OUTput" + ch.ToString() + " ON";
            gpibWrite(dev81150, strWrite);

            strWrite = ":OUTP" + ch.ToString() + ":COMPlement ON";
            gpibWrite(dev81150, strWrite);

            Thread.Sleep(200);

            
            return 0;
        }

        internal short dev81150TriggerSource(ushort deviceNum, ushort gpibNum, int ch, string source, bool forceRegister)
        {

            int dev81150 = RegisterDevice("81150", deviceNum, gpibNum, forceRegister);
            string strWrite = "";

            strWrite = ":ARM:SOURce" + ch.ToString() + " " + source;
        
            gpibWrite(dev81150, strWrite);
            
            Thread.Sleep(200);
            
            return 0;
        }

        internal short dev81150BurstCycle(ushort deviceNum, ushort gpibNum, int ch, ushort cycle, bool forceRegister)
        {

            int dev81150 = RegisterDevice("81150", deviceNum, gpibNum, forceRegister);
            string strWrite = ":TRIGger" + ch.ToString() + ":COUNt " + cycle.ToString();

            gpibWrite(dev81150, strWrite);

           Thread.Sleep(200);

            return 0;
        }

        internal short dev81150TrackFrequency(ushort deviceNum, ushort gpibNum, ushort multiplier, ushort devider, bool forceRegister)
        {

            int dev81150 = RegisterDevice("81150", deviceNum, gpibNum, forceRegister);
            string strWrite = "";

            strWrite = ":TRACk:FREQuency  ON";   //Enable Coupling between CH1 & CH2
            gpibWrite(dev81150, strWrite);

            strWrite = ":TRACk:FREQuency:DIVider " + devider.ToString();   
            gpibWrite(dev81150, strWrite);

            strWrite = ":TRACk:FREQuency:MULTiplier " + multiplier.ToString();     
            gpibWrite(dev81150, strWrite);


           
            Thread.Sleep(200);


            return 0;
        }

        internal short dev81150ManTrigger(ushort deviceNum, ushort gpibNum, bool forceRegister)
        {

            int dev81150 = RegisterDevice("81150", deviceNum, gpibNum, forceRegister);
            string strWrite = "";

            strWrite = "*TRG";  //Man trigger
            gpibWrite(dev81150, strWrite);

            Thread.Sleep(100);
            return 0;
        }

        internal short dev81150CHDelay(ushort deviceNum, ushort gpibNum, int ch, double delay, bool forceRegister)
        {
          
            int dev81150 = RegisterDevice("81150", deviceNum, gpibNum, forceRegister);
            string strWrite = "";

            strWrite = ":PULSe:DELay" + ch.ToString() + " " + delay.ToString() + "MS";
            gpibWrite(dev81150, strWrite);
            
            Thread.Sleep(200);
            
            return 0;
        }

        #endregion

        #region Agilent 53220A

        private int ini53220(ushort deviceNum, ushort gpibNum)
        {
        

            return 0;
        }


        internal double dev53220ReadFrequency(ushort deviceNum, ushort gpibNum)
        {
            double dmmRead = 0;
            string strWrite;
            int dev53220 = RegisterDevice("53220", deviceNum, gpibNum, false);

            strWrite = "CONF:FREQ";
            gpibWrite(dev53220, strWrite);

            strWrite = string.Format("READ?");
            gpibWrite(dev53220, strWrite);
            
            string readMessage = gpibRead(dev53220);
            dmmRead = double.Parse(readMessage.ToString().Trim().Substring(0, readMessage.ToString().IndexOf("\n")));

            return dmmRead;
        }
        #endregion

        #region Agilent 34980A
        internal double dev34980ChannelSet(ushort deviceNum, ushort gpibNum, ushort nslot, short nChannel)
        {
            string strWrite;
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);
            string sCh = nslot.ToString() + nChannel.ToString();
            strWrite = "ROUTe:CLOSe (@" + sCh + ")";
            gpibWrite(devDMM, strWrite);

            return 0;
        }

        internal string dev34980Custom(ushort deviceNum, ushort gpibNum, string command, bool ReadEnable)
        {
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);
            gpibWrite(devDMM, command);

            if (ReadEnable)
                return gpibRead(devDMM);

            return "0";
        }

        internal double dev34980TriggerSet(ushort deviceNum, ushort gpibNum, ushort nslot)
        {
            string strWrite;
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);
            strWrite = "SOURce:MODule:TRIGger:OUTPut ON, " + nslot.ToString();
            gpibWrite(devDMM, strWrite);

            return 0;
        }

        internal double dev34980TriggerSent(ushort deviceNum, ushort gpibNum, ushort nslot)
        {
            string strWrite;
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);
            strWrite = "SOURce:MODule:TRIGger:EXTernal:IMMediate " + nslot.ToString();
            gpibWrite(devDMM, strWrite);

            return 0;
        }

        internal double dev34980ClockSent(ushort deviceNum, ushort gpibNum, ushort nslot, double Frequence)
        {
            string strWrite;
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);
            strWrite = "SOURce:MODule:CLOCk:FREQ " + Frequence.ToString() + ", " + nslot.ToString();
            gpibWrite(devDMM, strWrite);

            Thread.Sleep(300);

            strWrite = "SOURce:MODule:CLOCk ON ," + nslot.ToString();
            gpibWrite(devDMM, strWrite);

            return 0;
        }

        internal double dev34980ClockOff(ushort deviceNum, ushort gpibNum, ushort nslot)
        {
            string strWrite;
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);
           
            strWrite = "SOURce:MODule:CLOCk OFF ," + nslot.ToString();
            gpibWrite(devDMM, strWrite);

            return 0;
        }


        internal double dev34980Meas(ushort deviceNum, ushort gpibNum, MvaInstrumentController.DmmMeasType measType, ushort nslot, short nChannel, double maxMeasVoltage)
        {
            double dmmRead = 0;
            string strWrite;
            string chan=null;

            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);

            if(nChannel.ToString().Length < 3)
            {
                chan = chan + "00" + nChannel.ToString();
            }
            else
                chan = chan + nChannel.ToString();
            chan = nslot.ToString() + chan;

            strWrite = string.Format("MEASure:" + measType + ":DC? " + maxMeasVoltage.ToString() + ",0.001, (@" + chan + ")");
            gpibWrite(devDMM, strWrite);

            Thread.Sleep(500);

            string readMessage = gpibRead(devDMM);
            dmmRead = double.Parse(readMessage.ToString().Trim().Substring(0, readMessage.ToString().IndexOf("\n")));

            return dmmRead;
        }

        internal double dev34980_34934_allOpen(ushort deviceNum, ushort gpibNum)
        {
            string strWrite;
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);

            strWrite = "ROUT:OPEN:ALL";
            gpibWrite(devDMM, strWrite);

            return 0;
        }

        internal double dev34980_34934_ClosePairs(ushort deviceNum, ushort gpibNum, bool open, string pairs)
        {
            string strWrite;
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);
            string[] temp = pairs.Split(',');
           

            if (temp.Length > 0)
            {
                strWrite = open ? "ROUT:OPEN (@" : "ROUT:CLOS (@";
                foreach (string Item in temp)
                    strWrite += Item + ",";

                int lastIndexOfComma = strWrite.LastIndexOf(",");
                strWrite = strWrite.Substring(0, lastIndexOfComma) + ")";
                gpibWrite(devDMM, strWrite);
            }
            return 0;
        }

        internal double dev34980_34934_CloseProtection100R(ushort deviceNum, ushort gpibNum, bool open, int slotOf34934)
        {
            string strWrite;
            int devDMM = RegisterDevice("34980", deviceNum, gpibNum, false);
            if(open)
                strWrite = "SYST:MOD:ROW:PROT " + slotOf34934.ToString() + ", AUTO0";
            else
                strWrite = "SYST:MOD:ROW:PROT " + slotOf34934.ToString() + ", AUTO100";
            gpibWrite(devDMM, strWrite);
            return 0;
        }
        #endregion

        #region Fluke7526A
        internal double devFUK7526Output(ushort deviceNum, ushort gpibNum, double OutputValue, string sUnits)
        {
            //double CaliRead = 0.0;
            string strWrite;
           
            int devDMM = RegisterDevice("7526", deviceNum, gpibNum, false);

            strWrite = string.Format("OUT " + OutputValue.ToString() + " " + sUnits); //Standby
            gpibWrite(devDMM, strWrite);

            Thread.Sleep(200);

            strWrite = string.Format("OPER"); //Operator Mode to output
            gpibWrite(devDMM, strWrite);

            return 0;
        }
        internal double devFUK7526Measure(ushort deviceNum, ushort gpibNum, string sSensorType, string sType)
        {
            //double CaliRead = 0.0;
            string strWrite;

            int devDMM = RegisterDevice("7526", deviceNum, gpibNum, false);

            strWrite = string.Format("TSENS_TYPE " + sSensorType);
            gpibWrite(devDMM, strWrite);

            Thread.Sleep(200);

            if (sSensorType.ToString() == "RTD")
            {
                strWrite = string.Format("RTD_TYPE " + sType);
                gpibWrite(devDMM, strWrite);
                Thread.Sleep(200);
                //strWrite = string.Format("RTD_MEAS CEL");
                //gpibWrite(devDMM, strWrite);
            }
            else
            {
                strWrite = string.Format("TC_TYPE " + sType);
                gpibWrite(devDMM, strWrite);
                Thread.Sleep(200);
                //strWrite = string.Format("TC_MEAS CEL");
                //gpibWrite(devDMM, strWrite);
                //strWrite = string.Format("TC_REF EXT");
                //gpibWrite(devDMM, strWrite);
            }

            strWrite = string.Format("OPER"); //Operator Mode to output
            gpibWrite(devDMM, strWrite);
            Thread.Sleep(500);

            //string readMessage = gpibRead(devDMM);
            //CaliRead = double.Parse(readMessage.ToString().Trim().Substring(0, readMessage.ToString().IndexOf("\n")));

            return 0;
        }
        #endregion


        #region GPIB function
        private void gpibClear(int dev)
        {
            int ibsta, iberr, ibcnt, ibcntl;
            GPIB.ibclr(dev);
            GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
            if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
            {
                throw new Exception("Error in clearing the 33250 device.");
            }
        }

        private int gpibIni(ushort deviceNum, ushort gpibNum)
        {
            int ibsta, iberr, ibcnt, ibcntl;
            int dev = GPIB.ibdev(deviceNum, gpibNum, 0, (int)GPIB.gpib_timeout.T10s, 1, 0);
            GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
            if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
            {
                throw new Exception("Error in initializing the GPIB instrument.");
            }

            return dev;
        }

        private string gpibRead(int dev)
        {
            int ibsta, iberr, ibcnt, ibcntl;
            StringBuilder strRead = new StringBuilder(100);
            GPIB.ibrd(dev, strRead, 100);
            GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
            if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
            {
                throw new Exception("Error in reading the response string from the GPIB instrument");
            }

            return strRead.ToString();
        }

        private void gpibWrite(int dev, string strWrite)
        {
            int ibsta, iberr, ibcnt, ibcntl;
            int e = GPIB.ibwrt(dev, strWrite, strWrite.Length);
            GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
            if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
            {
                throw new Exception("Error in writing the string command to the GPIB instrument (" + strWrite + ")");
            }
        }

        #endregion

       

        internal short getAddress(ushort deviceNum, string deviceName)
        {
            int ibsta, iberr, ibcnt, ibcntl;
            ushort[] result = new ushort[30];
            ushort[] instruments = new ushort[31];
            int num_listeners;
            ushort i, k;

            //Reset the GPIB interface card by sending an interface clear command (SendIFC)
            GPIB.SendIFC(deviceNum);
            GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
            if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
            {
                throw new Exception("SendIFC Fail");
            }

            for (k = 0; k < 30; k++)
            {
                instruments[k] = (ushort)(k + 1);
            }
            instruments[30] = GPIB.NOADDR;

            //Find the listeners (instruments) on the GPIB bus using the FindLstn() command
            GPIB.FindLstn(0, instruments, result, 31);
            GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
            if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
            {
                throw new Exception("FindLstn fail");
            }

            num_listeners = ibcnt;

            for (i = 0; i < num_listeners; i++)
            {
                int dev = gpibIni(deviceNum, result[i]);
                gpibWrite(dev, "*IDN?");

                string read = gpibRead(dev);

                if (read.Contains(deviceName) == true)
                {
                    return (short)result[i];
                }
            }
            return -1;
        }

        //public string[,] ModuleLookup()
        //{
        //    int rm = 0;
        //    int vi;
        //    int viStatus = 0;
        //    int theChassis = -1;
        //    ResourceManager aceResManager = new ResourceManager();
        //    string[] pxiResLists = aceResManager.FindRsrc("?*");
        //    string[,] pxiModule = new string[25, 2]; //pxiModule[0] = Chassis;//23,24 = pximodule, need to add another 6 slots as backup!!
        //    string Modules = string.Empty;
        //    AgVisa32.viOpenDefaultRM(out rm);
        //    AgVisa32.viSetAttribute(rm, 0x0FFF004C, 1);
        //    Report RP = new Report();
        //    string _Directory = @"C:\RUNDMA\";
        //    if (Directory.Exists(_Directory))
        //    {
        //        Directory.Delete(_Directory, true);
        //        Thread.Sleep(1000);
        //    }
        //    RP.FolderCreator(_Directory);
        //    string[] Settings = File.ReadAllLines(@"C:\Utilities\M9019ASetting.txt");
        //    string threshold = "1000";
        //    foreach (string _setting in Settings)
        //    {
        //        if (_setting.Contains("DMA"))
        //            threshold = _setting.Split('|')[1].Trim();
        //    }
        //    //   string[] _pxi_modules = new string[2]; 
        //    int i = 23;
        //    foreach (string Address in pxiResLists)
        //    {
        //    RETRY:
        //        if (Address.Contains("PXI") && Address.Contains("INSTR"))
        //        {
        //            viStatus = AgVisa32.viOpen(rm, Address, AgVisa32.VI_NO_LOCK, 500, out vi);
        //            int vid = 0;
        //            int pid = 0;
        //            short slot = -1;
        //            if (viStatus == AgVisa32.VI_SUCCESS)
        //            {
        //                viStatus = AgVisa32.viGetAttribute(vi, AgVisa32.VI_ATTR_PXI_CHASSIS, out theChassis);
        //            }


        //            if (viStatus == AgVisa32.VI_SUCCESS)
        //            {
        //                viStatus = AgVisa32.viGetAttribute(vi, AgVisa32.VI_ATTR_SLOT, out slot);

        //            }

        //            // Get the Manfufacturing ID

        //            if (viStatus == AgVisa32.VI_SUCCESS)
        //            {
        //                viStatus = AgVisa32.viGetAttribute(vi, AgVisa32.VI_ATTR_MANF_ID, out vid);
        //            }

        //            if (viStatus == AgVisa32.VI_SUCCESS)
        //            {
        //                viStatus = AgVisa32.viGetAttribute(vi, AgVisa32.VI_ATTR_MODEL_CODE, out pid);
        //            }

        //            if (viStatus == AgVisa32.VI_SUCCESS)
        //            {
        //                string vidpid = String.Format("{0:X4}:{1:X4}", vid, pid);

        //                switch (vidpid)
        //                {
        //                    case ("15BC:12A3"):

        //                        pxiModule[slot, 1] = Address;
        //                        pxiModule[slot, 0] = "Module";

        //                        break;
        //                    case ("15BC:1238"):

        //                        pxiModule[slot, 1] = Address;

        //                        break;
        //                    case ("15BC:1237"):
        //                        if (slot == -1)
        //                        {
        //                            MessageBox.Show("Keystone slot number =" + slot.ToString());
        //                            goto ENDLOOP;
        //                        }
        //                        string filename = "DMA" + slot.ToString() + ".bat";
        //                        filename = _Directory + filename;
        //                        RP.ResultFileCreator(filename);
        //                        string _pxiadd = string.Empty;
        //                        pxiModule[slot, 1] = Address;
        //                        pxiModule[slot, 0] = "Keystone";
        //                        _pxiadd = Address.Split(':')[0].Trim('P', 'X', 'I');
        //                        //    _pxiadd = "C:\\Utilities\\DMA2.exe " + _pxiadd +  " 1024 "+threshold; 
        //                        _pxiadd = "C:\\Utilities\\visasimpleWriteRead.exe -a" + _pxiadd + " -k" + threshold; //threshold is buffer size
        //                        RP.WritetoDMAfile(filename, _pxiadd);
        //                        break;
        //                    case ("15BC:123C"):
        //                        pxiModule[0, 1] = Address;
        //                        pxiModule[0, 0] = "M9018B";
        //                        break;
        //                    case ("15BC:123D"):
        //                        pxiModule[0, 1] = Address;
        //                        pxiModule[0, 0] = "M9010A";
        //                        break;
        //                    case ("15BC:123E"):
        //                        pxiModule[0, 1] = Address;
        //                        pxiModule[0, 0] = "M9019A";
        //                        break;
        //                    case ("15BC:1251"):

        //                        if (pxiModule[23, 1] == null)
        //                        {
        //                            pxiModule[23, 1] = Address;
        //                            pxiModule[23, 0] = "M9130A";
        //                        }
        //                        else
        //                        {
        //                            pxiModule[24, 1] = Address;
        //                            pxiModule[24, 0] = "M9130A";
        //                        }
        //                        break;
        //                    case ("15BC:1255"):

        //                        if (pxiModule[23, 1] == null)
        //                        {
        //                            pxiModule[23, 1] = Address;
        //                            pxiModule[23, 0] = "M9187A";
        //                        }
        //                        else
        //                        {
        //                            pxiModule[24, 1] = Address;
        //                            pxiModule[24, 0] = "M9187A";
        //                        }
        //                        break;


        //                    default:
        //                        break;
        //                }


        //            }
        //        ENDLOOP:
        //            AgVisa32.viClose(vi);
        //        }
        //        //if (Address.Contains("GPIB") && Address.Contains("INSTR"))
        //        //{
        //        //    DirectIO myDev = null;
        //        //    try
        //        //    {
        //        //        myDev = new DirectIO(Address);
        //        //        myDev.Timeout = 10000;
        //        //        myDev.Clear();
        //        //        myDev.Write("*cls;*idn?");
        //        //        string Devices = myDev.Read();
        //        //        if (Devices.Contains("53132A") || Devices.Contains("53230A"))
        //        //            pxiModule[19, 1] = Address;
        //        //        if (Devices.Contains("33220A") || Devices.Contains("33250A"))
        //        //            pxiModule[20, 1] = Address;
        //        //        if (Devices.Contains("34980A"))
        //        //            pxiModule[21, 1] = Address;
        //        //    }
        //        //    catch (VisaException ex)
        //        //    {

        //        //        //  MessageBox.Show("Visa Exception:" + ex.ToString());
        //        //        myDev.Clear();
        //        //        Thread.Sleep(500);
        //        //        goto RETRY;
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //    }
        //        //}
        //        if (Address.Contains("USB") && Address.Contains("INSTR"))
        //            pxiModule[22, 1] = Address;
        //    }
        //    AgVisa32.viClose(rm);
        //    return pxiModule;
        //}

        private string getWaveformType (uint waveformType)
        {
            string sWaveformType;
            switch(waveformType)
            {
                case 1:
                    sWaveformType ="DC";
                    break;
                case 2:
                    sWaveformType = "SIN";
                    break;
                case 3:
                    sWaveformType = "SQU";
                    break;
                case 4:
                    sWaveformType = "PULS";
                    break;
                case 5:
                    sWaveformType = "RAMP";
                    break;
                default:
                    sWaveformType = null;
                    break;
            }
            return sWaveformType;
        }
        //#region need to be refactoring

        //public void dev5720AOutput(double value, bool current)
        //{
        //    if (current == false)
        //    {
        //        strWrite = "OUT " + value.ToString() + "V; OPER";
        //    }
        //    else if(current ==true)
        //    {
        //        strWrite = "OUT " + value.ToString() + "mA; OPER";
        //    }
        //    GPIB.ibwrt(dev5720A, strWrite, strWrite.Length);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in writing the string command to the 5720A GPIB instrument.");
        //    }

        //    Thread.Sleep(100);
        //}

        //public void dev33250Output(ushort gpibNum, string frequency, string voltage)
        //{
        //    int dev33250 = RegisterDevice("33250", gpibNum, false);
        //    strWrite = "APPL:SIN " + frequency + " HZ, " + voltage + " VPP, 0 V";
        //    GPIB.ibwrt(dev33250, strWrite, strWrite.Length);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in writing the string command to the 33250 GPIB instrument.");
        //    }

        //    Thread.Sleep(100);
        //}

        //public void dev33250Pattern(ushort gpibNum, string pattern, int sleep)
        //{
        //    int dev33250 = RegisterDevice("33250", gpibNum, false);
        //    strWrite = pattern;
        //    GPIB.ibwrt(dev33250, strWrite, strWrite.Length);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in writing the string command to the 33250 GPIB instrument.");
        //    }

        //    Thread.Sleep(sleep);
        //}

        //public bool iniDMMGPIB(int GPIB_num)
        //{
        //    //DMM init
        //    devDMM = GPIB.ibdev(GPIB_num, 22, 0, (int)GPIB.gpib_timeout.T1s, 1, 0);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in initializing the DMM GPIB instrument.");
        //    }

        //    GPIB.ibclr(devDMM);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in clearing the DMM GPIB device.");
        //    }

        //    strWrite = "*RST";
        //    //Write a string command to a GPIB instrument using the ibwrt() command
        //    GPIB.ibwrt(devDMM, strWrite, strWrite.Length);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in writing the string command to the DMM GPIB instrument.");
        //    }         

        //    return true;
        //}

        //public bool iniDS360GPIB(int GPIB_num)
        //{
        //    //DS360 init
        //    devDS360 = GPIB.ibdev(GPIB_num, 8, 0, (int)GPIB.gpib_timeout.T3s, 1, 0);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in initializing the DS360 GPIB instrument.");
        //    }

        //    GPIB.ibclr(devDS360);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in clearing the DS360 GPIB device.");
        //    }

        //    strWrite = "*RST";
        //    //Write a string command to a GPIB instrument using the ibwrt() command
        //    GPIB.ibwrt(devDS360, strWrite, strWrite.Length);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in writing the string command to the DS360 GPIB instrument.");
        //    }

        //    return true;
        //}

        //public bool ini5720A(int GPIB_num)
        //{
        //    dev5720A = GPIB.ibdev(GPIB_num, 13, 0, (int)GPIB.gpib_timeout.T3s, 1, 0);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in initializing the DS360 GPIB instrument.");
        //    }

        //    GPIB.ibclr(dev5720A);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in clearing the DS360 GPIB device.");
        //    }

        //    strWrite = "*RST";
        //    //Write a string command to a GPIB instrument using the ibwrt() command
        //    GPIB.ibwrt(dev5720A, strWrite, strWrite.Length);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in writing the string command to the DS360 GPIB instrument.");
        //    }

        //    strWrite = "OUT 10V ; RANGELCK ON";
        //    GPIB.ibwrt(dev5720A, strWrite, strWrite.Length);
        //    GPIB.gpib_get_globals(out ibsta, out iberr, out ibcnt, out ibcntl);
        //    if ((ibsta & (int)GPIB.ibsta_bits.ERR) != 0)
        //    {
        //        throw new Exception("Error in writing the string command to the 5720A GPIB instrument.");
        //    }

        //    return true;
        //}        

        //public void dev5720Aclose()
        //{
        //    GPIB.ibonl(0,0);
        //}

        //#endregion
    }
}
