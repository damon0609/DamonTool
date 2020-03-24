using System;
using UnityEngine;
public class ResourceManager : Singleton<ResourceManager>
{
    public enum ResourceType
    {
        Undifine,
        Data,
        Monster,
        NPC,
        Scene,
        UI,
    }

    public const string DATA = "Data/";
    public const string MONSTER = "Monster/";
    public const string NPC = "Npc/";
    public const string SCENE = "Scene/";
    public const string UI = "UI/";


    #region 加载Resources中资源
    public T LoadRes<T>(ResourceType type, string name) where T : UnityEngine.Object
    {
        T t = default(T);
        string path = string.Empty;
        switch (type)
        {
            case ResourceType.Data:
                path += DATA + name;
                break;
            case ResourceType.Monster:
                path += MONSTER + name;
                break;
            case ResourceType.NPC:
                path += NPC + name;
                break;
            case ResourceType.Scene:
                path += SCENE + name;
                break;
            case ResourceType.UI:
                path += UI + name;
                break;
        }
        t = Resources.Load<T>(path);
        return t;
    }

    #endregion

    #region 加载AssetBundle
    public T LoadAssetBundle<T>(ResourceType resType, string name) where T : UnityEngine.Object
    {
        T t = default(T);
        string path = Application.streamingAssetsPath + "/";
        switch (resType)
        {
            case ResourceType.Data:
                path += DATA + name;
                break;
            case ResourceType.Monster:
                path += MONSTER + name;
                break;
            case ResourceType.NPC:
                path += NPC + name;
                break;
            case ResourceType.Scene:
                path += SCENE + name;
                break;
            case ResourceType.UI:
                path += UI + name;
                break;
        }

        AssetBundleManager.Instance.Load<T>(path);

        return t;
    }


    public void AsyncLoadAssetBundle<T>(ResourceType resType, string name, Action action) where T : UnityEngine.Object
    {

    }

    #endregion
    protected ResourceManager() { }

    public override void Dispose()
    {
        base.Dispose();
    }

    public override void OnSingletonInit()
    {
        base.OnSingletonInit();
    }
}
