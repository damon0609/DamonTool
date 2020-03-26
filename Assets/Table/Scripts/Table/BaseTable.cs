using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.Table
{
    public abstract class BaseTable
    {
        protected string mName;
        public string name { get { return mName; } }

        protected List<BaseObj> items = new List<BaseObj>();

        public BaseTable(string name)
        {
            this.mName = name;
            InitData();
            InitView();
        }

        protected virtual void InitData()
        {

        }

        public virtual void Close()
        {
            Debug.Log("close " + mName);
        }

        protected abstract void InitView();

        protected abstract void Clear();

        public abstract void ItemChange();

        public virtual void UpdateView()
        {

        }

    }
}

