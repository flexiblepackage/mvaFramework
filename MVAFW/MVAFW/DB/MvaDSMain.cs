using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;


using MVAFW.Config;
using MVAFW.Common.Entity;
using MVAFW.TestItemColls;

namespace MVAFW.DB
{
    internal class MvaDSMain
    {
        internal DataTable getProductSetting()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM productSetting INNER JOIN testProfile ON productSetting.profileIndex=testProfile.profileIndex";
            //return MvaDBDataFetcher.MvaDB.GetDataTable("SELECT * FROM productSetting");
            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }

        internal Error updateProductSetting(string modelName, string fpgaVersion, string mcuVersion, string firmwareVersion,
                                                        string qcProgramVersion, string macId, string productDescription, string driverVersion, string engineer,
                                                        string testInstrument)
        {
            int count;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET modelName=@modelName,fpgaVersion=@fpgaVersion," +
                                         "mcuVersion=@mcuVersion,firmwareVersion=@firmwareVersion,qcProgramVersion=@qcProgramVersion," +
                                         "macId=@macId,productDescription=@productDescription,driverVersion=@driverVersion,engineer=@engineer," +
                                         "testInstrument=@testInstrument";
            cmd.Parameters.AddWithValue("@modelName", modelName);
            cmd.Parameters.AddWithValue("@fpgaVersion", fpgaVersion);
            cmd.Parameters.AddWithValue("@mcuVersion", mcuVersion);
            cmd.Parameters.AddWithValue("@firmwareVersion", firmwareVersion);
            cmd.Parameters.AddWithValue("@qcProgramVersion", qcProgramVersion);
            cmd.Parameters.AddWithValue("@macId", macId);
            cmd.Parameters.AddWithValue("@productDescription", productDescription);
            cmd.Parameters.AddWithValue("@driverVersion", driverVersion);
            cmd.Parameters.AddWithValue("@engineer", engineer);
            cmd.Parameters.AddWithValue("@testInstrument", testInstrument);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal DataTable getTestItem(uint cardNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM  testItem WHERE cardNumber=@cardNumber and profileIndex=@profileIndex order by seqIndex";
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }

        internal DataTable getSingleTestItem(uint cardNumber, int testItemIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM  testItem t WHERE cardNumber=@cardNumber and t.'index'=@testItemIndex";
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);

            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }

        internal DataTable getLastInsertTestItem()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT last_insert_rowid() FROM testItem";

            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }

        internal DataTable getTestItemProperty(int testItemIndex, uint cardNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM testProperty WHERE testItemIndex=@testItemIndex and modelName=@modelName order by name DESC";
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);
            cmd.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);

            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }

        internal Error addTestItem(uint seqIndex,string name, uint cardNumber)
        {
            int count;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "INSERT INTO testItem (seqIndex,name,modelName,cardNumber,profileIndex) VALUES(@seqIndex,@name,@modelName,@cardNumber,@profileIndex)";
            cmd.Parameters.AddWithValue("@seqIndex", seqIndex);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error addTestItem(uint seqIndex, int OriProfileIdx, int OriTestitemIdx, uint cardNumber)
        {
            int count;
            SQLiteCommand cmd = new SQLiteCommand();
            string sComd = " INSERT INTO testItem (seqIndex,name,reTest,cycle,modelName,enable,stopWhenFail,reTestNUpperLimit,until,cardNumber,profileIndex,cycleDelay,aliasName)";
            sComd = sComd + " Select @seqIndex,name,reTest,cycle,modelName,enable,stopWhenFail,reTestNUpperLimit,until,cardNumber,@profileIndex,cycleDelay,aliasName";
            sComd = sComd + " from testItem where \"index\"=@OriTestitemIdx and cardNumber=@cardNumber and profileIndex=@OriProfileIdx";
            cmd.CommandText = sComd;
            cmd.Parameters.AddWithValue("@seqIndex", seqIndex);   
            cmd.Parameters.AddWithValue("@OriTestitemIdx", OriTestitemIdx);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@OriProfileIdx", OriProfileIdx);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error updateAliasName(int testItemIndex, string aliasName)
        {
            int count;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE testItem SET aliasName=@aliasName WHERE \"index\"=@testItemIndex";
            cmd.Parameters.AddWithValue("@aliasName", aliasName);
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);
            
            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error addTestItemProperty(int testItemIndex, string name, string value, uint cardNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "INSERT INTO testProperty (testItemIndex, name, value, modelName, cardNumber, profileIndex) VALUES(@testItemIndex, @name, @value, @modelName, @cardNumber, @profileIndex)";
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error addTestItemProperty(int testItemIndex, int OriTestitemIdx, int OriProfileIdx, uint cardNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            string sCmd = "INSERT INTO testProperty (testItemIndex, name, value, modelName, cardNumber, profileIndex) ";
            sCmd = sCmd + "select @testItemIndex, name, value, modelName, cardNumber, @profileIndex ";
            sCmd = sCmd + "from testproperty where testItemIndex=@OriTestitemIdx and cardNumber=@cardNumber and profileIndex=@OriProfileIdx";
            cmd.CommandText = sCmd;
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);
            cmd.Parameters.AddWithValue("@OriTestitemIdx", OriTestitemIdx);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@OriProfileIdx", OriProfileIdx);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error updateTestItemSetting(string columnName, string value, int index, uint cardNumber)
        {
            int count;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE testItem SET \"" + columnName + "\"=@value WHERE \"index\"=@index and \"cardNumber\"=@cardNumber and profileIndex=@profileIndex";
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@index", index);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error updateCardNumber(uint cardNumber)
        {
            int count;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET cardNumber=@cardNumber";
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error updateTestItemProperty(int testItemIndex, string pName, string pValue, string modelName, out int count, uint cardNumber)
        {
            count=0;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE testProperty SET \"value\"=@value WHERE testItemIndex=@testItemIndex and name=@name and modelName=@modelName and profileIndex=@profileIndex";
            cmd.Parameters.AddWithValue("@value", pValue);
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);
            cmd.Parameters.AddWithValue("@name", pName);
            cmd.Parameters.AddWithValue("@modelName", modelName);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error updateTestItemModelName(string pName, string pValue)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE testProperty SET modelName=@value; UPDATE testItem SET modelName=@value ";
            cmd.Parameters.AddWithValue("@value", pValue);
            cmd.Parameters.AddWithValue("@name", pName);
            //cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

       internal Error ExecToUpdateMultiTI(string columnName, string value, string[] testItemIndex, uint cardNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            string sCmd = string.Empty;
            int i =0;
            foreach(string str in testItemIndex)
            {
                if (sCmd.Length > 0)
                    sCmd = sCmd + "," + str;
                else
                    sCmd = sCmd + str;
                i++;
            }

            cmd.CommandText = "UPDATE testItem SET @columnName=@value where \"index\" in (" + sCmd + ")";
            cmd.Parameters.AddWithValue("@columnName", columnName);
            cmd.Parameters.AddWithValue("@value", value);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error addTestItemClass(string name, string classFullName, out int count)
        {
            count=0;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "INSERT INTO testItemClass (name, classFullName, modelName) VALUES(@name, @classFullName, @modelName)";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@classFullName",classFullName);
            cmd.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error delTestItemClass(string name, out int count)
        {
            count = 0;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "DELETE FROM testItemClass where name=@name";
            cmd.Parameters.AddWithValue("@name", name);
            
            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal DataTable getAllTestItemClass()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM testItemClass WHERE modelName=@modelName ORDER BY classFullName";
            cmd.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);

            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }

        internal Error delTestItem(int index, out int count, uint cardNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "DELETE FROM testItem WHERE \"index\"=@index and modelName=@modelName and cardNumber=@cardNumber and profileIndex=@profileIndex;" +
                                         "DELETE FROM testProperty WHERE testItemIndex=@index and modelName=@modelName and cardNumber=@cardNumber and profileIndex=@profileIndex";
            cmd.Parameters.AddWithValue("@index", index);
            cmd.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error delProfileTestItem(int profileIndex, uint cardNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "DELETE FROM testItem WHERE profileIndex=@profileIndex;" +
                                         "DELETE FROM testProperty WHERE profileIndex=@profileIndex";
            cmd.Parameters.AddWithValue("@profileIndex", profileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error addProfile(string profileName)
        {
            int count;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "INSERT INTO testProfile (profileName) VALUES(@profileName)";
            cmd.Parameters.AddWithValue("@profileName", profileName);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error delProfile(int profileIndex, out int count)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "DELETE from testProfile where profileIndex=@profileIndex;"+
                                         "DELETE FROM testItem WHERE profileIndex=@profileIndex;"+
                                         "DELETE FROM testProperty WHERE profileIndex=@profileIndex";
            cmd.Parameters.AddWithValue("@profileIndex", profileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal DataTable getAllProfile()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM testProfile order by profileName";
            
            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }

        internal Error updateCardNumber(uint cardNumber, out int count)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET cardNumber=@cardNumber";
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error updateProfile(int profileIndex, out int count)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET profileIndex=@profileIndex";
            cmd.Parameters.AddWithValue("@profileIndex", profileIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error updateLock(int Lock)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET lock=@lock";
            cmd.Parameters.AddWithValue("@lock", Lock);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error updateAutoClose(int autoClose)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET autoClose=@autoClose";
            cmd.Parameters.AddWithValue("@autoClose", autoClose);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error updateGlobalCycle(int globalCycle, out int count)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET globalCycle=@globalCycle";
            cmd.Parameters.AddWithValue("@globalCycle", globalCycle);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd, out count);
        }

        internal Error updateGlobalCycleDelay(double globalCycleDelay)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET globalCycleDelay=@globalCycleDelay";
            cmd.Parameters.AddWithValue("@globalCycleDelay", globalCycleDelay);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error startTestLog(string sn, out long count,uint cardNumber,int profileIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText="INSERT INTO result (sn,result,startTime,cardNumber,profileIndex) VALUES(@sn,'UNKNOWN',@startTime,@cardNumber,@profileindex)";
            cmd.Parameters.AddWithValue("@sn",sn);
            cmd.Parameters.AddWithValue("@startTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@cardNumber",cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", profileIndex);
            

            return MvaDBDataFetcher.MvaResultDB.ExecSQL(cmd, out count);            
        }

        internal Error updateTestResult(long resultIndex, string result)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE result SET result=@result,endTime=@endTime WHERE resultIndex=@resultIndex";
            cmd.Parameters.AddWithValue("@result", result);
            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);
            cmd.Parameters.AddWithValue("@endTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return MvaDBDataFetcher.MvaResultDB.ExecSQL(cmd);
        }

        internal DataTable getLastInsertResult()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT last_insert_rowid() FROM result";

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal DataTable getLastInsertResultDetail()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT last_insert_rowid() FROM resultDetail";

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal Error addResultDetail(int index, string spec, string value,TestResult result,int cycle,uint cardNumber,int profileIndex,
                                               long resultIndex, short channel, string errorCode, string description, string compareType, out long resultDetailIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "INSERT INTO resultDetail (modelName,testItemIndex,spec,value,result,cycle,time,cardNumber," +
                                                                            "profileIndex,resultIndex,channel,errorCode, description, compareType) VALUES(@modelName,@testItemIndex," +
                                                                            "@spec,@value,@result,@cycle,@time,@cardNumber,@profileIndex,@resultIndex,@channel,@errorCode,@description,@compareType)";

            cmd.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);
            cmd.Parameters.AddWithValue("@testItemIndex", index);
            cmd.Parameters.AddWithValue("@spec", spec);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@result", result.ToString());
            cmd.Parameters.AddWithValue("@cycle", cycle);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@profileIndex", profileIndex);
            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);
            cmd.Parameters.AddWithValue("@channel", channel);
            cmd.Parameters.AddWithValue("@errorCode", errorCode);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@compareType", compareType);

            return MvaDBDataFetcher.MvaResultDB.ExecSQL(cmd, out resultDetailIndex);
        }

        internal DataTable getTestResultWithDetail(long resultIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText =
                                         "SELECT r.resultIndex,r.sn,r.result,rd.result as resultDetail,t.'index' as testItemIndex, t.name,rd.resultDetailIndex,rd.spec" +
                                         ",rd.value,rd.result as resultDetail,rd.cycle,rd.channel,rd.time,r.startTime,r.endTime,rd.errorCode,rd.description as Description,rd.compareType, r.cardNumber " +
                                         "FROM resultDetail rd INNER JOIN result r ON r.resultIndex=rd.resultIndex " +
                                         "INNER JOIN mvaDB.testItem t ON rd.testItemIndex=t.'index' " +
                                         "WHERE rd.resultIndex=@resultIndex ORDER BY rd.time;";
            //cmd.CommandText = "SELECT * FROM resultDetail rd INNER JOIN testItem t ON rd.testItemIndex=t.'index' "+
            //                             "INNER JOIN result r ON rd.resultIndex=r.resultIndex WHERE rd.resultIndex=@resultIndex ORDER BY time";

            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal Error updateSchedulerTime(int miniutes, TurnOffType turnOffType)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET schedulerMinutes=@miniutes, turnOffType=@turnOffType";
            cmd.Parameters.AddWithValue("@miniutes", miniutes);
            cmd.Parameters.AddWithValue("@turnOffType", turnOffType.ToString());

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error updatePartNumber(string partNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET partNumber=@partNumber";
            cmd.Parameters.AddWithValue("@partNumber", partNumber);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error updateDataDisplayType(displayDataType dataTypeDisplay)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET dataTypeDisplay=@dataTypeDisplay";
            cmd.Parameters.AddWithValue("@dataTypeDisplay", dataTypeDisplay.ToString());

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal Error updateRestartIndex(int restartIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "UPDATE productSetting SET restartIndex=@restartIndex";
            cmd.Parameters.AddWithValue("@restartIndex", restartIndex);

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal DataTable getTestItemResult(long resultIndex) 
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM resultDetail where resultIndex=@resultIndex group by testItemIndex";
            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal DataTable getLastResultIndex(uint cardNumber)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM result where cardNumber=@cardNumber order by resultIndex DESC";
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal DataTable getEffectedValueOfColumn(string column)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText =
                                         "SELECT * FROM result r INNER JOIN mvaDB.testProfile tp on r.profileIndex=tp.profileIndex " +
                                         "WHERE startTime>=@startTime and (endTime<=@endTime or endTime is null) GROUP BY \"" + column + "\" ORDER BY \"" + column + "\";";                                         
            cmd.Parameters.AddWithValue("@startTime", eTestItemResult.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@endTime", eTestItemResult.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal DataTable getTestResult(params string[] values)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText ="SELECT * FROM result r INNER JOIN mvaDB.testProfile tp on r.profileIndex=tp.profileIndex " +
                             "WHERE startTime>=@startTime and (endTime<=@endTime or endTime is null)";                                                     
            foreach (string value in values)
            {
                cmd.CommandText += value;
            }
            cmd.CommandText += " ORDER BY startTime;";

            cmd.Parameters.AddWithValue("@startTime", eTestItemResult.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@endTime", eTestItemResult.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal DataTable getDatas(int[] resultDetailIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM testData WHERE resultDetailIndex=" + resultDetailIndex[0];

            for (int index = 1; index < resultDetailIndex.Length; index++)
            {
                cmd.CommandText += " OR resultDetailIndex=" + resultDetailIndex[index];
            }

            return MvaDBDataFetcher.MvaDataDB.GetDataTable(cmd);
        }

        internal Error delTestData(int resultIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
          
            cmd.CommandText = "DELETE FROM testData WHERE resultIndex=@resultIndex";
            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);

            return MvaDBDataFetcher.MvaDataDB.ExecSQL(cmd);
        }

        internal Error delResult(int resultIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "DELETE FROM result where resultIndex=@resultIndex;" +
                                        "DELETE FROM resultDetail where resultIndex=@resultIndex;";                                        

            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);

            delTestData(resultIndex);

            return MvaDBDataFetcher.MvaResultDB.ExecSQL(cmd);
        }

        internal Error releaseDiskSpace()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "VACUUM";

            return MvaDBDataFetcher.MvaDB.ExecSQL(cmd);
        }

        internal DataTable getResultDetailByTestItem(int resultIndex, int testItemIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM resultDetail WHERE resultIndex=@resultIndex and testItemIndex=@testItemIndex";
            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal DataTable getGroupValuesOfCycle(int resultIndex, int testItemIndex, string column)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM resultDetail WHERE resultIndex=@resultIndex and testItemIndex=@testItemIndex GROUP BY "+column;
            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal DataTable getValuesOfCycle(int resultIndex, int testItemIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM resultDetail WHERE resultIndex=@resultIndex and testItemIndex=@testItemIndex GROUP BY cycle";
            cmd.Parameters.AddWithValue("@resultIndex", resultIndex);
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);

            return MvaDBDataFetcher.MvaResultDB.GetDataTable(cmd);
        }

        internal DataTable getTestProperty(int testItemIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT * FROM testProperty WHERE testItemIndex=@testItemIndex";
            cmd.Parameters.AddWithValue("@testItemIndex", testItemIndex);

            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }

        internal DataTable getTestItemCount(int profileIndex)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "SELECT COUNT(*) AS count FROM testItem WHERE profileIndex=@profileIndex";
            cmd.Parameters.AddWithValue("@profileIndex", profileIndex);

            return MvaDBDataFetcher.MvaDB.GetDataTable(cmd);
        }       
    }
}
