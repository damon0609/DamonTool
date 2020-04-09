using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Damon {
    #region 无返回值委托
    public delegate void DAction ();
    public delegate void DAction<in T> (T t1);
    public delegate void DAction<in T1, in T2> (T1 t1, T2 t2);
    public delegate void DAction<in T1, in T2, in T3> (T1 t1, T2 t2, T3 t3);
    #endregion

    #region 有返回值委托
    public delegate TResult DFunc<out TResult> ();
    public delegate TResult DFunc<in T1, out TResult> (T1 t1);
    public delegate TResult DFunc<in T1, in T2, out TResult> (T1 t1, T2 t2);
    public delegate TResult DFunc<in T1, in T2, in T3, out TResult> (T1 t1, T2 t2, T3 t3);
    #endregion

    public class ReflectionTool {
        public static List<Type> GetAssembleType () {
            Assembly[] assemblies = GetAllAssembles ();
            foreach (Assembly e in assemblies) {
                Debug.Log (e.FullName + "--");
                // Type[] teyps = e.GetTypes ();
            }
            return null;
        }

        public static Assembly[] GetAllAssembles () {
            return AppDomain.CurrentDomain.GetAssemblies ();
        }
    }
}

public static class GlobalTool {
    private static readonly HashSet<string> RunTimeAssemblies = new HashSet<string> () {
        "Assembly-CSharp",
        "HTFramework.RunTime",
        "HTFramework.AI.RunTime",
        "HTFramework.Auxiliary.RunTime",
        "UnityEngine",
        "UnityEngine.CoreModule",
        "UnityEngine.UI",
        "UnityEngine.PhysicsModule"
    };
    #region 静态方法

    public static void OnDestroy (UnityObject obj) {
        GameObject.Destroy (obj);
    }
    public static void OnDestroyImmediate (UnityObject obj) {
        GameObject.DestroyImmediate (obj);
    }

    public static void OnDestroy<T> (List<T> list) where T : UnityObject {
        foreach (T t in list) {
            GameObject.Destroy (t);
        }
        list.Clear ();
    }

    public static GameObject CloneGameObject (GameObject originGo, bool isUI = false) {
        GameObject obj = GameObject.Instantiate (originGo);
        obj.transform.SetParent (originGo.transform.parent);
        if (isUI) {
            RectTransform objRectTransform = obj.GetComponent<RectTransform> ();
            RectTransform orignRectTransform = originGo.GetComponent<RectTransform> ();

            objRectTransform.anchoredPosition3D = orignRectTransform.anchoredPosition3D;
            objRectTransform.sizeDelta = orignRectTransform.sizeDelta;
            objRectTransform.anchorMin = orignRectTransform.anchorMin;
            objRectTransform.anchorMax = orignRectTransform.anchorMax;
        } else {
            obj.transform.localPosition = originGo.transform.localPosition;
        }
        obj.transform.localRotation = originGo.transform.localRotation;
        obj.transform.localScale = originGo.transform.localScale;
        obj.SetActive (true);
        return obj;
    }

    public static T Clone<T> (T obj) where T : UnityObject {
        return GameObject.Instantiate<T> (obj);
    }

    public static T Clone<T> (T obj, Vector3 pos, Quaternion rotation) where T : UnityObject {
        return GameObject.Instantiate<T> (obj, pos, rotation);
    }

    public static T Clone<T> (T obj, Vector3 pos, Quaternion ratation, Transform parent) where T : UnityObject {
        return GameObject.Instantiate<T> (obj, pos, ratation, parent);
    }

    public static T Clone<T> (T obj, Transform parent) where T : UnityObject {
        return GameObject.Instantiate<T> (obj, parent);
    }

    public static T Clone<T> (T obj, Transform parent, bool worldPositionStays) where T : UnityObject {
        return GameObject.Instantiate<T> (obj, parent, worldPositionStays);
    }

    #endregion
    public static Dictionary<string, Type[]> types = new Dictionary<string, Type[]> (); //根据程序集的来存储类
    public static void GetRuntimeType () {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies (); //获取程序域中的所有程序集
        foreach (Assembly assembly in assemblies) {
            string name = assembly.GetName ().Name; //获取程序集的名称
            if (RunTimeAssemblies.Contains (name)) {
                Type[] ts = assembly.GetTypes (); //获取程序集中的所有类
                if (!types.ContainsKey (name))
                    types[name] = ts;
            }
        }
    }
    //获取指定程序集中的所有类
    public static List<Type> GetRuntimeTypes () {
        GetRuntimeType ();
        List<Type> list = new List<Type> ();
        foreach (Type[] type in types.Values) {
            list.AddRange (type);
        }
        return list;
    }

    public static List<TOutput> ConvertAS<TOutput, TInput> (this List<TInput> array) where TOutput : class where TInput : class {
        if (array == null && array.Count == 0) return null;

        List<TOutput> temp = new List<TOutput> ();
        for (int i = 0; i < array.Count; i++) {
            temp.Add (array[i] as TOutput);
        }
        return temp;
    }
}