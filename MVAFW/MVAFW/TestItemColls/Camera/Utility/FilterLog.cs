using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using MVAFW.Common.Entity;

namespace MVAFW.TestItemColls.Camera.Utility
{
    public class FilterLog : BasicTestItem
    {

        public FilterLog()
        {
            searchPath = "search.log";
            filePath = "file.log";
            regexPattern = "pattern.log";
            dictionaryEnable = false;
            dictionaryKey = "filterkey1";
            filterConfig = regexConfig.countMatch;
        }

        public string filePath { get; set; }
        public string searchPath { get; set; }
        public string regexPattern { get; set; }
        public bool dictionaryEnable { get; set; }
        public string dictionaryKey { get; set; }
        public regexConfig filterConfig { get; set; }
        public enum regexConfig
        {
            countMatch = '0',
            valueMatch = '1'
        }

        readonly string defaultPath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "QC LOG FILE";

        private string fileInput()
        {
            string temp = "";

            if (dictionaryEnable)
                temp = File.ReadAllText(eMVACollection.MVAStringCollection[dictionaryKey] + ".log");
            else
                temp = File.ReadAllText(defaultPath + "\\" + filePath);

            return temp;
        }

        private string patternInput(string file)
        {
            var patternTable = File.ReadAllText(defaultPath + "\\" + regexPattern);
            var pattern = patternTable.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();

            var temp = Regex.Replace(Regex.Replace(file, string.Join("|", pattern), string.Empty), string.Join("|", pattern), string.Empty);

            var sw = new StreamWriter(defaultPath + "\\patternTemp.log");
            sw.Write(temp);
            sw.Close();

            return temp;
        }

        private string[] filterList()
        {
            var filterTable = File.ReadAllText(defaultPath + "\\" + searchPath);
            return filterTable.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        }


        public override void doTest()
        {
            base.doTest();

            var file = fileInput();

            var temp = patternInput(file);

            var filter = filterList();

            for (var i = 0; i < ChannelNumbers; i++)
            {
                switch (filterConfig.ToString())
                {
                    case "countMatch":
                        Values[i] = Regex.Matches(temp, filter[i]).Count.ToString();
                        break;
                    case "valueMatch":
                        Values[i] = Regex.Match(temp, filter[i], RegexOptions.RightToLeft).ToString();
                        break;
                }
            }
        }
    }
}
