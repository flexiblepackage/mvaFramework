using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

using MVAFW.Config;
using MVAFW.Common.Entity;

namespace MVAFW.DB
{
    public class MvaSQLDataFetcher
    {
        private string connectString;
        private SQLiteConnection sqliteConnection;
        private SQLiteCommand sqliteCommand;

        internal MvaSQLDataFetcher(string db)
        {
            this.connectString = "Data source=" + db;
            acquireConnection();
            initialAttach();
        }

        private void initialAttach()
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.CommandText = "attach database '"+System.Windows.Forms.Application.StartupPath+"\\mvaDB.sqlite' as mvaDB";
            ExecSQL(cmd);
        }

        private void detach()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "detach mvaDB";
            ExecSQL(cmd);
        }

        private void acquireConnection()
        {
            if (sqliteConnection == null)
            {
                sqliteConnection = new SQLiteConnection(this.connectString);
                sqliteConnection.Open();
                sqliteCommand = sqliteConnection.CreateCommand();
            }
        }

        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            sqliteCommand.CommandText = sql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(sqliteCommand.CommandText, sqliteConnection);
            da.Fill(dt);

            return dt;
        }

        public DataTable GetDataTable(SQLiteCommand command)
        {
            DataTable dt = new DataTable();
            command.Connection = sqliteConnection;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            da.Fill(dt);

            return dt;
        }

        public Error ExecSQL(SQLiteCommand command, out int count)
        {
            count=0;
            command.Connection = sqliteConnection;
            try
            {
                count = command.ExecuteNonQuery();
                if(count==0)
                {
                    return Error.NoData;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());             
                return Error.DBError;
            }

            return Error.NoError;
        }

        public Error ExecSQL(SQLiteCommand command, out long lastInsertRowID)
        {
            lastInsertRowID = 0;
            command.Connection = sqliteConnection;
            try
            {
                command.ExecuteNonQuery();
                lastInsertRowID = command.Connection.LastInsertRowId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Error.DBError;
            }

            return Error.NoError;
        }

        public Error ExecSQL(SQLiteCommand command)
        {
            command.Connection = sqliteConnection;        
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());             
                return Error.DBError;
            }

            return Error.NoError;
        }

        public Error ExecTransToDelData(int[] resultIndex)
        {
            Error err;
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = sqliteConnection;

            for (int i = 0; i < resultIndex.Length; i++)
            {
                DataTable dt = MvaDSManager.MainDB.getTestResultWithDetail(resultIndex[i]);
                SQLiteTransaction trans = command.Connection.BeginTransaction();
                try
                {
                    for (int dtIndex = 0; dtIndex < dt.Rows.Count; dtIndex++)
                    {
                        command.CommandText = "DELETE FROM testData WHERE resultDetailIndex=@resultDetailIndex";
                        command.Parameters.AddWithValue("@resultDetailIndex", int.Parse(dt.Rows[dtIndex]["resultDetailIndex"].ToString()));

                        command.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    return Error.DBError;
                }                

                err = MvaDSManager.MainDB.delResult(resultIndex[i]);
                if (err != Error.NoError)
                {
                    return Error.DBError;
                }
            }
            return Error.NoError;
        }

        public Error ExecTransToUpdateSeq(string columnName, uint[] value, uint[] testItemIndex, uint cardNumber)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = sqliteConnection;

            detach(); //to prevent transaction lock, might be improved in the future.

            SQLiteTransaction trans = command.Connection.BeginTransaction();

            try
            {
                for(int i=0;i<value.Length;i++)
                {
                    string sCmd = "UPDATE testItem SET " + columnName + "=@value where \"index\"=@testItemIndex";
                    command.CommandText = sCmd;
                    //command.CommandText = "UPDATE testItem SET seqIndex=@value where \"index\"=@testItemIndex";                    
                    //command.Parameters.AddWithValue("@columnName", columnName);
                    command.Parameters.AddWithValue("@value",value[i]);
                    command.Parameters.AddWithValue("@testItemIndex",testItemIndex[i]);
                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                initialAttach();
                trans.Rollback();
                return Error.DBError;
            }

            initialAttach();

            return Error.NoError;
        }

        public Error ExecTransToAddProperty(int testItemIndex, string[] names, string[] values, uint cardNumber)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = sqliteConnection;

            detach(); //to prevent transaction lock, might be improved in the future.

            SQLiteTransaction trans = command.Connection.BeginTransaction();

            try
            {
                for (int i = 0; i < names.Length; i++)
                {
                    command.CommandText = "INSERT INTO testProperty (testItemIndex, name, value, modelName, cardNumber, profileIndex)"+ 
                                                        "VALUES(@testItemIndex, @name, @value, @modelName, @cardNumber, @profileIndex)";
                    command.Parameters.AddWithValue("@testItemIndex", testItemIndex);
                    command.Parameters.AddWithValue("@name", names[i]);
                    command.Parameters.AddWithValue("@value", values[i]);
                    command.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);
                    command.Parameters.AddWithValue("@cardNumber", cardNumber);
                    command.Parameters.AddWithValue("@profileIndex", eProductSetting.ProfileIndex);

                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                initialAttach();
                trans.Rollback();
                return Error.DBError;
            }

            initialAttach();
            return Error.NoError;
        }

        public Error ExecTransToAddData(long resultDetailIndex, long resultIndex, double[] datas, double[] scaledDatas, short channel)
        {
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = sqliteConnection;

            SQLiteTransaction trans = command.Connection.BeginTransaction();

            try
            {
                for (int i = 0; i < datas.Length; i++)
                {
                    command.CommandText = "INSERT INTO testData (resultDetailIndex,modelName,data,scaledData,channel,resultIndex) " +
                                                      " VALUES(@resultDetailIndex,@modelName,@data,@scaledData,@channel,@resultIndex)";
                    command.Parameters.AddWithValue("@resultDetailIndex", resultDetailIndex);
                    command.Parameters.AddWithValue("@modelName", eProductSetting.ModelName);
                    command.Parameters.AddWithValue("@data", datas[i]);
                    command.Parameters.AddWithValue("@scaledData", scaledDatas[i]);
                    command.Parameters.AddWithValue("@channel", channel);
                    command.Parameters.AddWithValue("@resultIndex", resultIndex);

                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                return Error.DBError;
            }

            return Error.NoError;
        }

       
    }
}
