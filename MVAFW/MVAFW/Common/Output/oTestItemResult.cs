using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

using MVAFW.DB;
using MVAFW.Config;

namespace MVAFW.Common.Output
{
    public class oTestItemResult
    {
        public static Dictionary<int, TestResult> GetTestItemResult(uint cardNumber)
        {
            long resultIndex = oTestItemManage.GetResultIndex(cardNumber);
            Dictionary<int, TestResult> dictResult = new Dictionary<int,TestResult>();
            DataTable dt = MvaDSManager.MainDB.getTestItemResult(resultIndex);

            foreach (DataRow dr in dt.Rows)
            {
                dictResult.Add(int.Parse(dr["testItemIndex"].ToString()), (TestResult)Enum.Parse(typeof(TestResult), dr["result"].ToString()));
            }

            return dictResult;
        }

        public static List<string> GetEffectedValues(string column)
        {
            List<string> listValues = new List<string>();
            DataTable dt = MvaDSManager.MainDB.getEffectedValueOfColumn(column);

            foreach (DataRow dr in dt.Rows)
            {
                listValues.Add(dr[column].ToString());
            }

            return listValues;
        }

        public static DataTable GetTestResult(params string[] values)
        {
            return MvaDSManager.MainDB.getTestResult(values);
        }       

        public static int GetDatas(int[] resultDetailIndex, int[] channels, out double[][] datas, out double[][] scaledDatas)
        {
            DataTable dt = MvaDSManager.MainDB.getDatas(resultDetailIndex);
            datas = new double[resultDetailIndex.Length][];
            scaledDatas = new double[resultDetailIndex.Length][];

            int dataCount = dt.Rows.Count / resultDetailIndex.Length;

            for (int index = 0; index < datas.Length; index++)
            {
                datas[index] = new double[dataCount];
                scaledDatas[index] = new double[dataCount];

                DataRow[] drs = dt.Select("channel=" + channels[index].ToString());
                for (int dpIndex = 0; dpIndex < drs.Length; dpIndex++)
                {
                    double data = double.Parse(drs[dpIndex]["data"].ToString());
                    double scaledData = double.Parse(drs[dpIndex]["scaledData"].ToString());

                    datas[index][dpIndex] = data;
                    scaledDatas[index][dpIndex] = scaledData;
                }
            }

            return dataCount;
        }

        public static void DeleteResult(int[] resultIndex)
        {
            Error err;
            for (int i = 0; i < resultIndex.Length; i++)
            {
                err = MvaDSManager.MainDB.delResult(resultIndex[i]);
                if (err != Error.NoError)
                {
                    MessageBox.Show("oTestItemResult DeleteResult delResult db error!");
                    return;
                }
            }            
        }

        public static void ReleaseDiskSpace()
        {
            Error err = MvaDSManager.MainDB.releaseDiskSpace();
            if (err != Error.NoError)
            {
                MessageBox.Show("oTestItemResult ReleasediskSpace error");
            }
        }

        public static DataTable GetResultDetail(int resultIndex, int testItemIndex)
        {
            return MvaDSManager.MainDB.getResultDetailByTestItem(resultIndex, testItemIndex);
        }

        public static DataTable GetGroupValuesOfCycle(int resultIndex, int testItemIndex, string column)
        {
            return MvaDSManager.MainDB.getGroupValuesOfCycle(resultIndex, testItemIndex, column);
        }

        public static DataTable GetValuesOfCycle(int resultIndex, int testItemIndex)
        {
            return MvaDSManager.MainDB.getValuesOfCycle(resultIndex, testItemIndex);
        }

        public static List<string> GetTestProperty(int testItemIndex)
        {
            List<string> propertys = new List<string>();
            DataTable dt = MvaDSManager.MainDB.getTestProperty(testItemIndex);
            foreach (DataRow dr in dt.Rows)
            {
                string property = dr["name"].ToString() + " : " + dr["value"].ToString();
                propertys.Add(property);
            }

            return propertys;
        }
    }
}
