using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.Table
{
    public class BaseTable
    {
        protected string mName;
        public string name { get { return mName; } }

        protected List<TableItem> items = new List<TableItem>();
        public bool isActive = false;
        public int index;

        private string mPath;
        public BaseTable(string name, string path)
        {
            this.mName = name;
            this.mPath = path;
        }

        public virtual void InitData()
        {
            if (!isActive) return;
            Debug.Log(mName + "----" + mPath);
        }

        public virtual void Close()
        {

        }

    }
}

