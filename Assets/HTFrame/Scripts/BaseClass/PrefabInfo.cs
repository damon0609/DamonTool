using System;

public sealed class PrefabInfo:BaseResourcesInfo
{
    public PrefabInfo(string assetBundleName,string assetPath,string resPath):base(assetBundleName,assetPath,resPath)
    {

    }
    public PrefabInfo(UIResourcesInfoAttribute att):base(att.assetBundleName,att.assetPath,att.resPath)
    {

    }
    public PrefabInfo(EntityInfoAttribute att):base(att.assetBundleName,att.assetPath,att.resourcePath)
    {

    }
}