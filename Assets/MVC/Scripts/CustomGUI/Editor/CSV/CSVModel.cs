using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Damon.MVC;
public enum ModelKind {
    CSV,
}
public class CSVModel : BaseModel {

    public class CSVInfo {
        public string name;
        public string path;
    }
    private List<CSVInfo> m_csvList = new List<CSVInfo> ();
    public List<CSVInfo> csvList {
        get { return m_csvList; }
    }
    public CSVModel () : base () {
    }
    protected override void Init () {
        GetCSVFile (Application.dataPath);
    }

    private void GetCSVFile (string path) {
        DirectoryInfo root = new DirectoryInfo (path);
        DirectoryInfo[] childs = root.GetDirectories ();
        foreach (DirectoryInfo child in childs) {
            GetCSVFile (child.FullName);
        }
        FileInfo[] files = root.GetFiles ();
        foreach (FileInfo file in files) {
            if (Path.GetExtension (file.FullName) == ".csv") {
                CSVInfo csv = new CSVInfo
                {
                    name = Path.GetFileName (file.FullName),
                    path = file.FullName,
                };
                m_csvList.Add (csv);
            }
        }
    }
}