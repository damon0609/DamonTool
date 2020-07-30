using System.Collections;
using System.Collections.Generic;
using Damon.Tool;
using UnityEngine;
using System;
using UnityEngine.Networking;

[InternalModule(HTFrameworkModuleType.Resource)]
public class ResourcesManager : InternalBaseModule
{
    private WaitUntil waitUnitl;
    private bool isLoading = false;
    private string assetBundleRootPath = "file:///C:/Users/damon.cheng/Desktop/AssetBundles/";
    public ResourcesMode resourcesMode = ResourcesMode.AssetBundle;
    private AssetBundleManifest assetBundleManifest;
    private Dictionary<string, AssetBundle> assetBundles = new Dictionary<string, AssetBundle>();
    private Dictionary<string, Hash128> hashDic = new Dictionary<string, Hash128>();
    public bool isCahce = true;
    public bool isEditorMode;
    public string assetBundleManifestName = "models";

    public IEnumerator LoadAssetAsync(BaseResourcesInfo resInfo, Action<float> loadingProcess, Action<UnityEngine.Object> loadedSuccess)
    {
        DateTime beginTime = DateTime.Now;
        // if (isLoading)
        //     yield return waitUnitl;
        // isLoading = true;

        //先加载依赖项
        yield return Main.instance.StartCoroutine(LoadDependenciesAssetBundleAsync(resInfo.assetBundleName));

        DateTime loadedTime = DateTime.Now;
        UnityEngine.Object asset = null;
        if (resourcesMode == ResourcesMode.Resources)
        {
            ResourceRequest resourceRequest = Resources.LoadAsync(resInfo.resPath);
            while (!resourceRequest.isDone)
            {
                if (loadingProcess != null)
                {
                    loadingProcess.Invoke(resourceRequest.progress);
                    yield return null;
                }
            }
            asset = resourceRequest.asset;
        }
        else
        {
            if (assetBundles.ContainsKey(resInfo.assetBundleName))
            {
                if (loadingProcess != null)
                    loadingProcess.Invoke(1);
                yield return null;

                this.d("LoadAssetAsync", assetBundles[resInfo.assetBundleName] == null);
                // asset = assetBundles[resInfo.assetBundleName].LoadAsset(resInfo.assetPath);
            }
            else
            {
                using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleRootPath + resInfo.assetBundleName))
                {
                    request.SendWebRequest();
                    while (!request.isDone)
                    {
                        if (loadingProcess != null)
                        {
                            loadingProcess(request.downloadProgress);
                        }
                        yield return null;
                    }

                    if (!request.isHttpError && !request.isNetworkError)
                    {
                        AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                        if (assetBundle != null)
                        {
                            asset = assetBundle.LoadAsset<UnityEngine.Object>(resInfo.assetPath);
                            this.d("damon", "----");
                        }
                        if (isCahce)
                        {
                            if (!assetBundles.ContainsKey(resInfo.assetBundleName))
                            {
                                assetBundles[resInfo.assetBundleName] = assetBundle;
                            }
                        }
                        else
                        {
                            assetBundle.Unload(false);
                        }
                    }
                }
            }
        }
        if (loadedSuccess != null)
        {
            loadedSuccess(asset);
        }
        yield return null;
    }

    //加载依赖项先加载AssetBunldeManifest 
    public IEnumerator LoadDependenciesAssetBundleAsync(string assetBundleName)
    {
        if (resourcesMode == ResourcesMode.AssetBundle)
        {
            //通过Manifest获取加载的依赖项
            yield return Main.instance.StartCoroutine(LoadAssetBundleManifestAsync());//加载配置文件
            if (assetBundleManifest)
            {
                string[] dependencies = assetBundleManifest.GetAllDependencies(assetBundleName);
                foreach (string name in dependencies)
                {
                    if (assetBundles.ContainsKey(name))
                        continue;
                    yield return Main.instance.StartCoroutine(LoadAssetBundleAsync(name));
                }
            }
            else if (resourcesMode == ResourcesMode.Resources)
            {
            }
            yield return null;
        }
    }

    //加载manifset
    public IEnumerator LoadAssetBundleManifestAsync()
    {
        if (string.IsNullOrEmpty(assetBundleManifestName) || assetBundleManifestName == " ")
        {
            this.d("damon", "请提供ab配置表的清单");
            yield return null;
        }
        else
        {
            if (assetBundleManifest == null)
            {
                yield return Main.instance.StartCoroutine(LoadAssetBundleAsync(assetBundleManifestName, true));
                if (assetBundles.ContainsKey(assetBundleManifestName))
                {
                    assetBundleManifest = assetBundles[assetBundleManifestName].LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                    UnLoadAsset(assetBundleManifestName);
                }
            }
        }
    }

    //加载资源
    private IEnumerator LoadAssetBundleAsync(string name, bool isMainifest = false)
    {
        if (!assetBundles.ContainsKey(name))
        {
            using (UnityWebRequest webRequest = isMainifest ?
                UnityWebRequestAssetBundle.GetAssetBundle(assetBundleRootPath + name) :
                UnityWebRequestAssetBundle.GetAssetBundle(assetBundleRootPath + name, GetAssetBundleHash(name)))
            {
                yield return webRequest.SendWebRequest();
                if (!webRequest.isNetworkError && !webRequest.isHttpError)
                {
                    AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(webRequest);
                    if (assetBundle)
                    {
                        assetBundles[name] = assetBundle;
                        this.d("LoadAssetBundleAsync", name + "--" + assetBundle.GetAllAssetNames().Length);
                    }
                    else
                    {
                        this.d("damon", "加载的assetbunlde is null");
                    }
                }
                else
                {
                    this.d("LoadAssetBundleAsync", "加载时网络出错");
                }
                // this.d("LoadAssetBundleAsync", assetBundleRootPath + name);
            }
        }

        yield return null;
    }

    private Hash128 GetAssetBundleHash(string name)
    {
        if (hashDic.ContainsKey(name))
        {
            return hashDic[name];
        }
        else
        {
            Hash128 hash = assetBundleManifest.GetAssetBundleHash(name);
            hashDic[name] = hash;
            return hash;
        }
    }

    private void UnLoadAsset(string name, bool unLoadAllObject = false)
    {
        if (resourcesMode == ResourcesMode.Resources)
        {
            Resources.UnloadUnusedAssets();
        }
        else if (resourcesMode == ResourcesMode.AssetBundle)
        {
            if (assetBundles.ContainsKey(name))
            {
                assetBundles[name].Unload(unLoadAllObject);
                assetBundles[name] = null;
            }
            if (hashDic.ContainsKey(name))
            {
                assetBundles.Remove(name);
            }
        }

    }

    #region  Unity Events
    public override void OnInitialization()
    {
        base.OnInitialization();
        waitUnitl = new WaitUntil(() => { return !isLoading; });
    }
    public override void OnPause()
    {

    }
    public override void OnPreparatory()
    {

    }
    public override void OnRefresh()
    {

    }
    public override void OnResume()
    {

    }

    public override void OnTermination()
    {

    }
    #endregion
}

public enum ResourcesMode
{
    Resources,
    AssetBundle,
}