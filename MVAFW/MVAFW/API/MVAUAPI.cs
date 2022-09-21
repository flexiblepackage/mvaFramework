using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVAFW.TestItemColls;
using MVAFW.Common.Entity;


namespace MVAFW.API
{
    public class MVAUAPI
    {
        private static readonly object balanceLock = new object();
        private static MVAUAPI _instance;
        private MVADASKAPI api;
        private MVAUAPI() { }

        public static MVADASKAPI LookupApi(TestItem testItem)
        {
            lock (balanceLock)
            {
                return _instance == null ? null : _instance.FindOrCreateApi(testItem);
            }
        }

        //internal static object LookupApi(object o)
        //{
        //    throw new NotImplementedException();
        //}

        public static void InitialApiMapping()
        {
            if (_instance == null)
                _instance = new MVAUAPI();

            #region Create test item map
            eMVACollection.InsertApiMapping("Uart", 0, "MVAFW.API.MVAUartAPI");
            eMVACollection.InsertApiMapping("Web", 0, "MVAFW.API.MVAWebAPI.WebAPI");
            eMVACollection.InsertApiMapping("Tend", 0, "MVAFW.API.MVAAndroidAPI.AndroidAPITend");
            eMVACollection.InsertApiMapping("Falcon", 0, "MVAFW.API.MVAAndroidAPI.MVAMyQAPI.MyQAPIFalcon");
            eMVACollection.InsertApiMapping("WinterSoldier", 0, "MVAFW.API.MVAAndroidAPI.MVAMyQAPI.MyQAPIWS");
            #endregion

            #region Create product type map
            eMVACollection.InsertProductTypeMapping("MVAFW.TestItemColls.Uart", "Uart");
            eMVACollection.InsertProductTypeMapping("MVAFW.TestItemColls.Camera.Web", "Web");
            eMVACollection.InsertProductTypeMapping("MVAFW.TestItemColls.Android.MyQ.Tend", "Tend");
            eMVACollection.InsertProductTypeMapping("MVAFW.TestItemColls.Android.MyQ.Falcon", "Falcon");
            eMVACollection.InsertProductTypeMapping("MVAFW.TestItemColls.Android.MyQ.WinterSoldier", "WinterSoldier");
            #endregion
        }

        private MVADASKAPI FindOrCreateApi(TestItem testItem)
        {
            if (api != null && api.TestItem != null && api.TestItem == testItem)
            {
                return api;
            }
            else
            {
                Type type = eMVACollection.GetApiType(testItem, testItem.CardType);
                api = (MVADASKAPI)Activator.CreateInstance(type);
                api.TestItem = testItem;
                return api;
            }
        }
    }
}
