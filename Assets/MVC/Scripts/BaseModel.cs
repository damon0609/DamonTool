using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public enum ModelKind {
    CSV,
}

public class CSVModel : BaseModel {

    public class CSVInfo {
        public string name;
        public string path;
    }
    private List<CSVInfo> csvList = new List<CSVInfo> ();
    public CSVModel () : base () {

    }
    protected override void Init () {
        string path = Path.GetFullPath (Application.dataPath).Replace ("/", "\\");
        GetCSVFile (path);
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
                CSVInfo csv = new CSVInfo {
                name = Path.GetFileName (file.FullName),
                path = file.FullName,
                };
                csvList.Add(csv);
            }
        }
    }
}
public class BaseModel : IModel {
    public ModelKind mModelType;
    public ModelKind modelType {
        get {
            return modelType;
        }

        set {
            mModelType = value;
        }
    }

    public BaseModel () {
        Init ();
    }
    protected virtual void Init () {

    }
}