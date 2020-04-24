using System;
using System.Collections;
using System.Collections.Generic;
using Damon.Tool;
using LitJson;
using UnityEngine;
using Damon;

[InternalModule (HTFrameworkModuleType.DateSet)]
public class DateSetManager : InternalBaseModule {
    private Dictionary<Type, List<BaseDateSet>> mDateSet = new Dictionary<Type, List<BaseDateSet>> ();

    #region 功能函数
    public T CreateDateSet<T> (JsonData data) where T : BaseDateSet {
        return CreateDateSet (typeof (T), data) as T;
    }
    public BaseDateSet CreateDateSet (Type type, JsonData data) {
        if (type.IsSubclassOf (typeof (BaseDateSet))) {
            if (!mDateSet.ContainsKey (type))
                mDateSet.Add (type, new List<BaseDateSet> ());
            if (data != null) {
                List<BaseDateSet> list = mDateSet[type];
                BaseDateSet set = ScriptableObject.CreateInstance (type) as BaseDateSet;
                set.Fill (data);
                list.Add (set);
                return set;
            }
            return null;
        } else {
            Debug.Log ("不存在的数据类型");
            return null;
        }
    }

    #region  获得数据集合
    public List<T> GetBaseDateSets<T> () where T : BaseDateSet {
        return GetBaseDateSets (typeof (T)).ConvertAS<T, BaseDateSet> ();
    }
    public List<BaseDateSet> GetBaseDateSets (Type type) {
        if (type.IsSubclassOf (typeof (BaseDateSet))) {
            List<BaseDateSet> list;
            if (!mDateSet.TryGetValue (type, out list)) {
                return null;
            } else {
                list = mDateSet[type];
                return list;
            }
        }
        return null;
    }
    #endregion

    #region  根据条件匹配数据
    public T GetBaseDateSet<T> (Predicate<T> match) where T : BaseDateSet {
        return GetBaseDateSet (typeof (T), match as Predicate<BaseDateSet>) as T;
    }
    public BaseDateSet GetBaseDateSet (Type type, Predicate<BaseDateSet> match) {
        if (mDateSet.ContainsKey (type)) {
            return mDateSet[type].Find (match);
        }
        return null;
    }
    #endregion

    #region 匹配多个数据集合
    public List<T> GetBaseDateSets<T> (Predicate<T> match) where T : BaseDateSet {
        return GetBaseDateSets (typeof (T), match as Predicate<BaseDateSet>).ConvertAS<T, BaseDateSet> ();
    }

    public List<BaseDateSet> GetBaseDateSets (Type type, Predicate<BaseDateSet> match) {
        if (mDateSet.ContainsKey (type)) {
            return mDateSet[type].FindAll (match);
        }
        return null;
    }
    #endregion


    #region  移除数据
    public void ClearBaseDateSetItem (Type type, BaseDateSet set) {
        List<BaseDateSet> list = mDateSet[type];
        for (int i = list.Count; i >= 0; i--) {
            if (list[i] == set)
                list.Remove (set);
        }
    }
    public void ClearBaseDateSet (Type type) {
        if (mDateSet.ContainsKey (type)) {
            List<BaseDateSet> list = mDateSet[type];
            list.Clear ();
            list = null;
            mDateSet.Remove (type);
        }
    }
    public void ClearAll () {
        foreach (Type t in mDateSet.Keys) {
            ClearBaseDateSet (t);
        }
    }
    #endregion

    public void RemoveDateSet (BaseDateSet date) {
        Type t = date.GetType ();
        if (mDateSet.ContainsKey (t)) {
            List<BaseDateSet> list = mDateSet[t];
            list.Remove (date);
        } else {
            Debug.Log ("不包含的类");
        }
    }

    public void AddDateSet (BaseDateSet data) {
        Type t = data.GetType ();
        if (mDateSet.ContainsKey (t)) {
            List<BaseDateSet> list = mDateSet[t];
            list.Add (data);
        } else {
            List<BaseDateSet> list = new List<BaseDateSet> () { data };
            mDateSet[t] = list;
        }
    }

    #endregion

    #region 生命周期
    public override void OnInitialization () {
        base.OnInitialization ();

        List<Type> list = GlobalTool.GetRuntimeTypes ();
        foreach (Type t in list) {
            if (t.IsSubclassOf (typeof (BaseDateSet))) {
                mDateSet.Add (t, new List<BaseDateSet> ());
                this.d ("damon", t.FullName,false);
            }
        }
    }

    public override void OnPause () {
        base.OnPause ();
    }

    public override void OnPreparatory () {
        base.OnPreparatory ();
        //Debug.Log ("DateSetManager preparatory");
        // JsonData data = new JsonData ();
        // data["name"] = "npc1";
        // data["id"] = 1;
        // CreateDateSet (typeof (NPCDate), data);

        // List<BaseDateSet> list = GetBaseDateSets (typeof (NPCDate));
        // foreach (NPCDate d in list) {
        //     Debug.Log (d.Pack ().ToJson ());
        // }

    }

    public override void OnRefresh () {
        base.OnRefresh ();
    }

    public override void OnResume () {
        base.OnResume ();
    }

    public override void OnTermination () {
        base.OnTermination ();
    }
    #endregion
}