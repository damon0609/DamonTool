using System;

[AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class EntityInfoAttribute : Attribute {

    public string assetBundleName;
    public string assetPath;

    public string resourcePath;
     
    public EntityInfoAttribute (string assetBundleName,string assetPath,string resourcePath) {
        this.assetBundleName = assetBundleName;
        this.assetPath = assetPath;
        this.resourcePath = resourcePath;
    }
}