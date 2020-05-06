using System;

[AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class EntityInfoAttribute : Attribute {

    public string assetBundleName;
    public string assetPath;
    public string resourcePath;
    public bool isUseObject;
     
    public EntityInfoAttribute (string assetBundleName,string assetPath,string resourcePath,bool isUseObject) {
        this.assetBundleName = assetBundleName;
        this.assetPath = assetPath;
        this.resourcePath = resourcePath;
        this.isUseObject = isUseObject;
    }
}