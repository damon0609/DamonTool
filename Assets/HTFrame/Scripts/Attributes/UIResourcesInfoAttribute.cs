using System;


[AttributeUsage(AttributeTargets.Class,AllowMultiple=false,Inherited = false)]
public class UIResourcesInfoAttribute:Attribute
{
    public string assetBundleName;

    public string assetPath;

    public string resPath;

    public RenderUIType renderMode ;

    public UIResourcesInfoAttribute(string assetBundleName,string assetPath,string resPath,RenderUIType renderMode)
    {
        this.assetBundleName = assetBundleName;
        this.assetPath = assetPath;
        this.resPath = resPath;
        this.renderMode = renderMode;
    }
}

public enum RenderUIType
{
    Overlay,

    Camera,
    World
} 