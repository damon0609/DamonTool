using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime.DynamicDispatching;
using Damon.Tool.Events;
using UnityEngine;
using Damon.Tool;
namespace Damon.Table
{
    public class TableFactory : Singleton<TableFactory>, IDataSet
    {
        public static EventDispatcher dispatcher = new EventDispatcher();
        private Dictionary<int, BaseTable> mDic = new Dictionary<int, BaseTable>();
        private BaseTable mCurTable;
        public BaseTable curTable
        {
            get { return mCurTable; }
        }
        private int mIndex = 0;
        protected TableFactory()
        {
        }
        public int count
        {
            get
            {
                return mDic == null ? 0 : mDic.Count;
            }
        }
        public BaseTable this[int index]{get{return mDic[index];}}        
        private void InitData()
        {
            TextAsset textAsset = ResourceManager.Instance.LoadRes<TextAsset>(ResourceManager.ResourceType.Data, "table");
            Dictionary<string, Dictionary<string, string>> dic = CSVTool.Parse(textAsset.text);
            foreach (string id in dic.Keys)
            {
                string name = dic[id]["name"];
                string path = dic[id]["resPath"];
                BaseTable baseTable = new BaseTable(name, path);
                mDic[mIndex++] = baseTable;
            }
        }
        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            InitData();
        }
    }
}
