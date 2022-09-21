using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MVAFW.DB
{
    public class MvaDBDataFetcher
    {
        private static string sqliteLocation = System.Windows.Forms.Application.StartupPath + "\\mvaDB.sqlite";
        private static string sqliteBaseLocation = System.Windows.Forms.Application.StartupPath + "\\mvaDB_base.sqlite";

        private static string sqliteDataLocation = System.Windows.Forms.Application.StartupPath + "\\mvaDataDB.sqlite";
        private static string sqliteBaseDataLocation = System.Windows.Forms.Application.StartupPath + "\\mvaDataDB_base.sqlite";

        private static string sqliteResultLocation = System.Windows.Forms.Application.StartupPath + "\\mvaResultDB.sqlite";
        private static string sqliteBaseResultLocation = System.Windows.Forms.Application.StartupPath + "\\mvaResultDB_base.sqlite";

        private static MvaSQLDataFetcher mvaDB;
        public static MvaSQLDataFetcher MvaDB
        {
            get
            {
                if (mvaDB == null)
                {
                    if (File.Exists(sqliteLocation) == false)
                    {
                        File.Copy(sqliteBaseLocation, sqliteLocation);
                    }
                    mvaDB = new MvaSQLDataFetcher(System.Windows.Forms.Application.StartupPath + "\\mvaDB.sqlite");
                }
                return mvaDB;
            }
        }

        private static MvaSQLDataFetcher mvaDataDB;
        public static MvaSQLDataFetcher MvaDataDB
        {
            get
            {
                if (mvaDataDB == null)
                {
                    if (File.Exists(sqliteDataLocation) == false)
                    {
                        File.Copy(sqliteBaseDataLocation, sqliteDataLocation);
                    }
                    mvaDataDB = new MvaSQLDataFetcher(System.Windows.Forms.Application.StartupPath + "\\mvaDataDB.sqlite");
                }
                return mvaDataDB;
            }
        }

        private static MvaSQLDataFetcher mvaResultDB;
        public static MvaSQLDataFetcher MvaResultDB
        {
            get
            {
                if (mvaResultDB == null)
                {
                    if (File.Exists(sqliteResultLocation) == false)
                    {
                        File.Copy(sqliteBaseResultLocation, sqliteResultLocation);
                    }
                    mvaResultDB = new MvaSQLDataFetcher(System.Windows.Forms.Application.StartupPath + "\\mvaResultDB.sqlite");
                }
                return mvaResultDB;
            }
        }
    }
}
