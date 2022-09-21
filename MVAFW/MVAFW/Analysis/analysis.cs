using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MVAFW.TestItemColls;

namespace MVAFW.Analysis
{
    public class analysis
    {
        public static void getDCAnalysis(double[] data, double[] scaledData, out double average, out double min, out double max, out double rms, out double std, out double p2p, out int nofc)
        {
            double sum = 0;
            double stdSum = 0;
            double rmsSum = 0;
            max = scaledData[0];
            min = scaledData[0];
            Dictionary<string, uint> numberDict = new Dictionary<string, uint>();

            for (int i = 0; i < scaledData.Length; i++)
            {
                sum += scaledData[i];
                rmsSum += Math.Pow(scaledData[i], 2);

                if (max < scaledData[i])
                {
                    max = scaledData[i];
                }

                if (min > scaledData[i])
                {
                    min = scaledData[i];
                }

                string key = data[i].ToString();
                if (!numberDict.ContainsKey(key))
                {
                    numberDict.Add(key, 1);
                }
                else
                {
                    numberDict[key] += 1;
                }
            }

            average = sum / scaledData.Length;
            rms = Math.Sqrt(rmsSum / scaledData.Length);
            
            foreach (double value in scaledData)
            {
                stdSum += Math.Pow((value - average), 2);
            }

            std = Math.Sqrt(stdSum / (scaledData.Length));
            nofc = numberDict.Count;
            p2p = Math.Abs(max - min);
        }

        public static double getSTD(double[] data)
        {
            double sum = 0;
            double average = getAverage(data);

            for (int i = 0; i < data.Length; i++)
            {
                sum += Math.Pow((data[i] - average), 2);
            }

            return Math.Sqrt(sum / (data.Length));
        }

        public static double Connectivity(double[] scaledData)
        {
            double[] delta = new double[scaledData.Length - 1];
            double deltaMax = 0;

            for (var i = 0; i < scaledData.Length - 1; i++)
            {
                delta[i] = Math.Abs(scaledData[i + 1] - scaledData[i]);

                if (deltaMax < delta[i])
                    deltaMax = delta[i];
            }

            return deltaMax;
        }


        public static double getAverage(double[] data)
        {
            double sum = 0;
         
            foreach (double value in data)
            {
                sum += value;
            }

            return sum / data.Length;
        }

        public static double getMax(double[] data)
        {
            double max = data[0];
            foreach (double value in data)
            {
                if (max < value)
                {
                    max = value;
                }
            }

            return max;
        }

        public static double getMin(double[] data)
        {
            double min = data[0];
            foreach (double value in data)
            {
                if (min > value)
                {
                    min = value;
                }
            }

            return min;
        }

        public static double getRms(double[] data)
        {
            double rms = 0;
            double sum = 0;
            foreach (double value in data)
            {
                sum += Math.Pow(value, 2);
            }

            rms = Math.Sqrt(sum / data.Length);

            return rms;
        }

        public static int getNumberOfCode(double[] data)
        {

            Dictionary<string, uint> numberDict = new Dictionary<string, uint>();
            for (int i = 0; i < data.Length; i++)
            {
                string key = data[i].ToString();
                if (!numberDict.ContainsKey(key))
                {
                    numberDict.Add(key, 1);
                }
                else
                {
                    numberDict[key] += 1;
                }
            }

            return numberDict.Count;
        }

        public static bool IsPrime(uint candidate)
        {
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }
            return (candidate != 1) ? true : false;
        }

        public static double getPeakToPeak(double[] data)
        {
            return Math.Abs(getMax(data) - getMin(data));
        }

        public static void GetDynamicPerformance(AITestItem testItem, double[] data, bool isDump, int channel)
        {
            Dynamic.TEST_dynamic_performance(testItem, data, isDump, channel);
        }

        public static int GetDelayCalculate(AITestItem testItem, double[] samples, double[] data, int channel)
        {
            uint nTemp = (uint)data.Length;
            double n = Math.Log(nTemp, 2);
            int temp = (int)(n + 0.5);
            uint count = (uint)(Math.Pow(2, temp))*2;
            
            double[] datasReal = new double[count];
            double[] datasImg = new double[count];
            double[] sampleReal = new double[count];
            double[] sampleImg = new double[count];

            double [] data1=new double[count];
            double [] data2=new double[count];
            double [] z_Real = new double[count];
            double[] z_img = new double[count];

            data.CopyTo(data1, 0);
            samples.CopyTo(data2, 0);

            FFT.fft_double(0, count, data1, null, datasReal, datasImg);
            FFT.fft_double(0, count, data2, null, sampleReal, sampleImg);

            double [] ZR = new double[count];
            double[] ZI = new double[count];

            for(int i=0 ; i< count ; i++)
            {
                sampleImg[i] = (-1) * sampleImg[i];
                ZR[i] = sampleReal[i] * datasReal[i] - sampleImg[i] * datasImg[i];
                ZI[i] = sampleImg[i] * datasReal[i] + sampleReal[i] * datasImg[i];
            }

            FFT.fft_double(1, count, ZR, ZI, z_Real, z_img);

           
            double dmax=0;
            int nMaxIdx = 0;
            for (int nct = 0; nct < count; nct++)
            {
                //if (Math.Abs(z_Real[nct]) > dmax)
                //{
                //    dmax = Math.Abs(z_Real[nct]);
                //    nMaxIdx = nct;
                //}

                if (z_Real[nct] > dmax)
                {
                    dmax = z_Real[nct];
                    nMaxIdx = nct;
                }
            }

            if (nMaxIdx > count / 2)
                nMaxIdx = nMaxIdx - (int)count;

            //string s1="";
            //for (int nct = 0; nct < count; nct++)
            //{
            //    s1 = s1 + z_Real[nct] + "\r\n";
            //}
            //System.IO.File.WriteAllText(@"C:\result.txt", s1);
            return nMaxIdx;
        }

        public static void GetDynamicPerformance2(AITestItem testItem, double[] data, bool isDump, int channel)
        {
            Dynamic2.TEST_dynamic_performance(testItem, data, isDump, channel);
        }

        public static double GetCoherenceFreqency(double Freq, double SampleRate, uint datacount)
        {
            uint ratio = (uint)(datacount * Freq / SampleRate);
            bool bisprime = false;
            while (ratio > 0)
            {
                bisprime=IsPrime(ratio);
                if (bisprime)
                {
                    break;
                }
                else
                {
                    ratio--;
                }
            }
            return   SampleRate * ratio / datacount;
        }

        public static void GetCrossTalkAndSkew(AITestItem testItem, int baseChannel, bool isRawdata, out double[] crosstalks, out  double[] skews)
        {
            Dynamic.TestCrossTalkAndSkew(testItem, baseChannel, isRawdata, out crosstalks, out skews);
        }

        public static void GetCrossTalkAndSkew(AITestItem testItem, int numberofcards, string baseCardandCH, double[] AllSampleRates, double[][] databuf, int baseChannel, bool isRawdata, out double[] mainSignalsFreqs, out double[] crosstalks, out  double[] skews)
        {
            Dynamic.TestCrossTalkAndSkew(testItem, numberofcards, baseCardandCH, AllSampleRates, databuf, baseChannel, isRawdata, out mainSignalsFreqs, out crosstalks, out skews);
        }

        public static double GetCMRR(AITestItem testItem, double[] dataWithSignal, double[] dataWithShort, int channel)
        {
            return Dynamic.CMRR(testItem, dataWithSignal, dataWithShort, channel);
        }
    }
}
