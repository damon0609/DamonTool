using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

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

    //public List<T> GetBaseDateSets<T>() where T : BaseDateSet
    //{
    //    return GetBaseDateSets(typeof(T)).ConvertAllAS<T, BaseDateSet>();
    //}

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

    #region 生命周期
    public override void OnInitialization () {
        base.OnInitialization ();
    }

    public override void OnPause () {
        base.OnPause ();
    }

    public override void OnPreparatory () {
        base.OnPreparatory ();
        Debug.Log ("DateSetManager preparatory");
        JsonData data = new JsonData ();
        data["name"] = "npc1";
        data["id"] = 1;
        CreateDateSet (typeof (NPCDate), data);

        List<BaseDateSet> list = GetBaseDateSets (typeof (NPCDate));
        foreach (NPCDate d in list) {
            Debug.Log (d.Pack ().ToJson ());
        }
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