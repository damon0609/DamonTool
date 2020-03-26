using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime.DynamicDispatching;
using Damon.Tool.Events;
using UnityEngine;

namespace Damon.Table
{
    public class TableFactory : Singleton<TableFactory>
    {
        public enum TableType : int
        {
            NPC = 0,
            Monster,
            ItemObj,
        }
        public static EventDispatcher dispatcher = new EventDispatcher();
        private Dictionary<TableType, BaseTable> dic = new Dictionary<TableType, BaseTable>();

        private BaseTable mCurTable;
        public BaseTable curTable
        {
            get { return mCurTable; }
        }

        protected TableFactory() { }

        public int count
        {
            get
            {
                return dic == null ? 0 : dic.Count;
            }
        }
        private void InitData()
        {
            if (mCurTable == null)
            {
                NPC npc = new NPC("NPC");
                dic[TableType.NPC] = npc;
                mCurTable = npc;
            }

            Monster monster = new Monster("monster");
            dic[TableType.Monster] = monster;

            ItemObj itemObj = new ItemObj("itemObj");
            dic[TableType.ItemObj] = itemObj;

            dispatcher.Trigger("InitTableView", dic);
        }

        public void ChangeTable(BaseTable t)
        {
            foreach (BaseTable table in dic.Values)
            {
                if (t == table)
                {
                    mCurTable.Close();
                    mCurTable = t;
                    break;
                }
            }

            if (!dic.ContainsValue(t))
            {

            }

        }

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            InitData();
            dispatcher.Register("Create", (TableType type) =>
            {
                switch (type)
                {
                    case TableType.NPC:
                        NPC npc = null;
                        if (dic.ContainsKey(type))
                        {
                            npc = (NPC)dic[type];
                        }
                        else
                        {
                            npc = new NPC("npc");
                            dic[type] = npc;
                        }
                        break;
                    case TableType.Monster:
                        Monster monster = null;
                        if (dic.ContainsKey(type))
                        {
                            monster = (Monster)dic[type];
                        }
                        else
                        {
                            monster = new Monster("monster");
                            dic[type] = monster;
                        }
                        break;
                    case TableType.ItemObj:
                        ItemObj itemObj = null;
                        if (dic.ContainsKey(type))
                        {
                            itemObj = (ItemObj)dic[type];
                        }
                        else
                        {
                            itemObj = new ItemObj("itemObj");
                            dic[type] = itemObj;
                        }
                        break;
                }
            });
        }
    }
}
