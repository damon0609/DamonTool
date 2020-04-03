using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damon.Tool;
namespace Damon.Table {
    public class BaseTable:ILog {
        protected string mName;
        public string name { get { return mName; } }

        protected List<TableItem> items = new List<TableItem> ();

        public int count{get{return items==null?0:items.Count;}}

        public TableItem this[int index] {get{return items[index];}}

        public int index;
        private string mPath;
        public BaseTable (string name, string path) {
            this.mName = name;
            this.mPath = path;
        }
        public virtual void InitData () {
            if (items.Count == 0 || items == null) {
                TextAsset textAsset = ResourceManager.Instance.LoadRes<TextAsset> (ResourceManager.ResourceType.Data, mPath);
                if (textAsset != null) {
                    string dataStr = textAsset.text;
                    Dictionary<string, Dictionary<string, string>> datas = CSVTool.Parse (dataStr);
                    foreach (string id in datas.Keys) {
                        TableItem item = new TableItem();
                        int idValue = int.Parse(id);
                        item.id = idValue;
                        item.name = datas[id]["name"];
                        item.account =int.Parse(datas[id]["account"]);
                        item.price = float.Parse(datas[id]["price"]);
                        item.data = datas[id]["data"];
                        items.Add(item);
                        this.d("damon",item.ToString());
                    }
                } else {
                    Debug.Log (mPath);
                }
            }
        }
        public virtual void Close () {

        }

    }
}