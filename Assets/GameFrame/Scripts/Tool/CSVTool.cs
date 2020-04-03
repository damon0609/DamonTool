using System.Collections.Generic;
namespace Damon.Tool {
    public class CSVTool {
        private static Dictionary<string, Dictionary<string, string>> mDic;
        private static List<string> mLabels;
        public static Dictionary<string, Dictionary<string, string>> Parse (string content) {
            mDic = new Dictionary<string, Dictionary<string, string>> ();
            string[] rows = content.TrimEnd ('\n').Split ('\n');
            for (int i = 0; i < rows.Length; i++) {
                string row = rows[i].Trim ();
                if (i == 0)
                    ParseLabel (row);
                else
                    ParseID (row);
            }
            return mDic;
        }

        private static void ParseID (string str) {
            string[] strs = str.Split (',');
            Dictionary<string, string> dic = new Dictionary<string, string> ();
            for (int i = 0; i < strs.Length; i++) {
                if (i == 0) {
                    mDic[strs[i]] = dic;
                    //Debug.Log(strs[i]);
                } else {
                    string label = mLabels[i];
                    dic.Add (label, strs[i].TrimEnd ());
                    //Debug.Log(label + "--" + strs[i]);
                }
            }
        }

        private static void ParseLabel (string label) {
            mLabels = new List<string> ();
            string[] temp = label.Split (',');
            foreach (string s in temp) {
                mLabels.Add (s.TrimEnd ());
            }
        }
    }
}