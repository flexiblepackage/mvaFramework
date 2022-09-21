using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVAFW.TestItemColls
{
    public class TestData
    {
        public double[][] Datas;
        public double[][] ScaledDatas;
        public AITestItem testItem;

        public TestData(double[][] datas, double[][] scaledDatas, AITestItem testItem)
        {
            this.Datas = datas;
            this.ScaledDatas = scaledDatas;
            this.testItem = testItem;
        }

        public TestData() { }
    }
}
