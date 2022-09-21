using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVAFW.TestItemColls;
using System.Windows.Forms;


namespace System.Collections.Generic
{
   
}

namespace MVAFW.Common.Entity
{
    

    public static class eMVACollection
    {
        private static Object thisLock = new Object();

        private static Dictionary<string, double> multiCardsSampleRate; //Record multi cards' sampling rate for further multicards' test and analysis
        public static Dictionary<string, double> MultiCardsSampleRate
        {
            get
            {
                if (multiCardsSampleRate == null)
                {
                    multiCardsSampleRate = new Dictionary<string, double>();
                }
                return multiCardsSampleRate;
            }
            set
            {
                multiCardsSampleRate = value;
            }
        }

        private static Dictionary<string, double[]> multiCardsDataBuff; //Restore acquired data for mutiple cards' test
        public static Dictionary<string, double[]> MultiCardsDataBuff
        {
            get
            {
                if (multiCardsDataBuff == null)
                {
                    multiCardsDataBuff = new Dictionary<string, double[]>();
                }
                return multiCardsDataBuff;
            }
            set
            {
                multiCardsDataBuff = value;
            }
        }

        private static Dictionary<string, double> mvaCollection;
        public static Dictionary<string, double> MVACollection
        {
            get
            {
                if (mvaCollection == null)
                {
                    mvaCollection = new Dictionary<string, double>();
                }
                return mvaCollection;
            }
            set
            {
                mvaCollection = value;
            }
        }

        private static Dictionary<string, string> mvaStringCollection;
        public static Dictionary<string, string> MVAStringCollection
        {
            get
            {
                if(mvaStringCollection==null)
                {
                    mvaStringCollection = new Dictionary<string, string>();
                }
                return mvaStringCollection;
            }
            set
            {
                mvaStringCollection = value;
            }
        }

        #region CardID
        public static Dictionary<string, short> DictCard = new Dictionary<string, short>();

        #endregion

        #region Test Data
        private static Queue<TestData> testDataQueue;
        public static Queue<TestData> TestDataQueue
        {
            get
            {
                if (testDataQueue == null)
                {
                    testDataQueue = new Queue<TestData>();
                }
                return testDataQueue;
            }
        }

        private static TestData aiTestData = new TestData();
        public static TestData AiTestData
        {
            get
            {
                return aiTestData;
            }
            set
            {
                aiTestData = value;
            }
        }

        public static void AssignTestData(double[][] datas, double[][] scaledDatas, AITestItem aiTestItem)
        {
            lock (AiTestData)
            {
                AiTestData = new TestData(GenericCopier<double[][]>.DeepCopy(datas), GenericCopier<double[][]>.DeepCopy(scaledDatas), aiTestItem);
            }
        }

        public static void AddQueue(TestData testData)
        {
            //TestDataQueue.Enqueue(testData);
            AssignTestData(testData.Datas, testData.ScaledDatas, testData.testItem); //for backward compatibility
        }

        public static TestData DeQueue()
        {
           // lock (thisLock)
            //{
                return TestDataQueue.Dequeue();
            //}
        }
        #endregion


        #region Product Type Map
        private static Dictionary<string, string> ProductTypeMap = new Dictionary<string, string>();

        public static void InsertProductTypeMapping(string spaceName, string productType)
        {
            ProductTypeMap[spaceName] = productType;
        }
        #endregion


        #region Test Item Map
        public class DualKeyDictionary<TKey1, TKey2, TValue> : Dictionary<Tuple<TKey1, TKey2>, TValue>, IDictionary<Tuple<TKey1, TKey2>, TValue>
        {
            public TValue this[TKey1 key1, TKey2 key2]
            {
                get { return base[Tuple.Create(key1, key2)]; }
                set { base[Tuple.Create(key1, key2)] = value; }
            }

            public void Add(TKey1 key1, TKey2 key2, TValue value)
            {
                base.Add(Tuple.Create(key1, key2), value);
            }

            public bool ContainsKey(TKey1 key1, TKey2 key2)
            {
                return base.ContainsKey(Tuple.Create(key1, key2));
            }
        }
        private static DualKeyDictionary<string, ushort, string> DualKeyMap = new DualKeyDictionary<string, ushort, string>();

        public static void InsertApiMapping(string productType, ushort productId, string fullApiName)
        {
            DualKeyMap[productType, productId] = fullApiName;
        }

        public static Type GetApiType(TestItem testItem, ushort productId)
        {
            Type type = null;
            string space = Type.GetType(testItem.ToString()).Namespace;
            foreach (KeyValuePair<string, string> item in ProductTypeMap)
            {
                if (space.Contains(item.Key) == true)
                {
                    type = Type.GetType(eMVACollection.DualKeyMap[item.Value, productId], true);
                    break;
                }
            }

            if (type == null)
            {
                try {
                    type = Type.GetType(eMVACollection.DualKeyMap["MISC", productId], true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Cannot find product type mapped to " + space + "!");
                }
            }
            return type;
        }

        #endregion




        public static Dictionary<string, TestItemCardType> TestItemMap = new Dictionary<string, TestItemCardType>();

        public static void Initial()
        {

        }

        public class TestItemCardType
        {
            public string TestItem;
            public ushort CardType;

            public TestItemCardType(string testItem, ushort cardType)
            {
                this.TestItem = testItem;
                this.CardType = cardType;
            }
        }
    }
}
