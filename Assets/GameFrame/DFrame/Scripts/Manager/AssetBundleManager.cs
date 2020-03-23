using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AssetBundleManager : Singleton<AssetBundleManager>
{
    public class MonoClass : MonoBehaviour
    {

    }

    private MonoClass monoClass;
    private Dictionary<string, AssetBundle> mCacheAssetBundles;
    protected AssetBundleManager() { }


    public void Load<T>(string path)
    {
        string name = path.Substring(path.LastIndexOf('/') + 1);
        name = name.Remove(name.IndexOf('.'));
        Debug.Log(name);

    }

    public void Load(string path)
    {
        string name = path.Substring(path.LastIndexOf('/') + 1);
        name = name.Remove(name.IndexOf('.'));
        Debug.Log(name);
    }

    private void LoadAssetBundle<T>(string path, Action<T> action) where T : UnityEngine.Object
    {
        if (null == monoClass)
            Debug.LogError("MonoClass instance failed");




        monoClass.StartCoroutine(AsyncLoadAssetBundle(path, assetbundle =>
        {
            T t = assetbundle.LoadAsset<T>(path);
            string name = path.Substring(path.LastIndexOf('/') + 1);
            name = name.Remove(name.IndexOf('.'));

            Debug.Log(" assetbundle name " + name);

            if (action != null)
                action(t);
        }));
    }

    public override void OnSingletonInit()
    {
        monoClass = new MonoClass();
        mCacheAssetBundles = new Dictionary<string, AssetBundle>();
        base.OnSingletonInit();
    }

    IEnumerator AsyncLoadAssetBundle(string path, Action<AssetBundle> action)
    {
        using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(path))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isHttpError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(webRequest);
                if (action != null)
                    action(assetBundle);
            }
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        mCacheAssetBundles.Clear();
    }
}
