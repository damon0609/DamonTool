using System;


public abstract class BaseResourcesInfo
{
    public string assetBundleName;
    public string assetPath;
    public string resPath;
    public BaseResourcesInfo (string assetBundleName,string assetPath,string resPath)
    {
        this.assetBundleName = assetBundleName;
        this.assetPath = assetPath;
        this.resPath = resPath;
    }

    internal string GetResourcesPath()
    {
        return "Resources/"+resPath;
    }

    internal string GetAssetBundlePath(string assetBundleRootPath)
    {
        return "AssetBundlePath:"+ assetBundleRootPath + assetBundleName +"AssetPath:" + assetPath;
    }
}