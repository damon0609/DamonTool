using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;
public sealed partial class Main : MonoBehaviour
{
    #region 静态方法

    public static void OnDestroy(UnityObject obj)
    {
        Destroy(obj);
    }
    public static void OnDestroyImmediate(UnityObject obj)
    {
        DestroyImmediate(obj);
    }

    public static void OnDestroy<T>(List<T> list) where T : UnityObject
    {
        foreach (T t in list)
        {
            Destroy(t);
        }
        list.Clear();
    }

    public static GameObject CloneGameObject(GameObject originGo, bool isUI = false)
    {
        GameObject obj = Instantiate(originGo);
        obj.transform.SetParent(originGo.transform.parent);
        if (isUI)
        {
            RectTransform objRectTransform = obj.GetComponent<RectTransform>();
            RectTransform orignRectTransform = originGo.GetComponent<RectTransform>();

            objRectTransform.anchoredPosition3D = orignRectTransform.anchoredPosition3D;
            objRectTransform.sizeDelta = orignRectTransform.sizeDelta;
            objRectTransform.anchorMin = orignRectTransform.anchorMin;
            objRectTransform.anchorMax = orignRectTransform.anchorMax;
        }
        else
        {
            obj.transform.localPosition = originGo.transform.localPosition;
        }
        obj.transform.localRotation = originGo.transform.localRotation;
        obj.transform.localScale = originGo.transform.localScale;
        obj.SetActive(true);
        return obj;
    }

    public static T Clone<T>(T obj) where T : UnityObject
    {
        return Instantiate<T>(obj);
    }

    public static T Clone<T>(T obj, Vector3 pos, Quaternion rotation) where T : UnityObject
    {
        return Instantiate<T>(obj, pos, rotation);
    }

    public static T Clone<T>(T obj, Vector3 pos, Quaternion ratation, Transform parent) where T : UnityObject
    {
        return Instantiate<T>(obj, pos, ratation, parent);
    }

    public static T Clone<T>(T obj, Transform parent) where T : UnityObject
    {
        return Instantiate<T>(obj, parent);
    }

    public static T Clone<T>(T obj, Transform parent, bool worldPositionStays) where T : UnityObject
    {
        return Instantiate<T>(obj, parent, worldPositionStays);
    }

    #endregion


    private Dictionary<HTFrameworkModule, InternalBaseModule> mModules = new Dictionary<HTFrameworkModule, InternalBaseModule>();

    private void InitModule()
    {
        InternalBaseModule[]  modules =  transform.GetComponentsInChildren<InternalBaseModule>(true);
        for (int i = 0; i < modules.Length; i++)
        {
            Type t = modules[i].GetType();
             t.GetCustomAttributes(true);
        }
    }


    public enum HTFrameworkModule
    {
        AspectTrack,
        Audio,
        Controller,
        Coroutiner,
        CustomModule,
        DataSet,
        Debug,
        Entity,
        Event,
        ExceptionHandler,
        FSM,
        HotFix,
        Input,
        Main,
        NetWork,
        ObjectPool,
        Procedure,
        ReferencePool,
        Resource,
        StepEditor,
        UI,
        Utility,
        WebRequest,

    }
}
