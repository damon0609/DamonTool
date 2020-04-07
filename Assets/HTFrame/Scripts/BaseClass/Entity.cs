using UnityEngine;
using Damon.Tool;
[EntityInfo ("assetBundle", "assetPath", "resPath")]
public class Entity : IReference,ILog {

    private string assetBundleName;
    private string assetPath;
    private string resPath;
    public Entity () {

        System.Object[] attributes = this.GetType().GetCustomAttributes(true);
        for(int i=0;i<attributes.Length;i++)
        {
            EntityInfoAttribute a = attributes[i] as EntityInfoAttribute;
            assetBundleName = a.assetBundleName;
            assetPath = a.assetPath;
            resPath = a.resourcePath;
        }
        this.d("damon",assetBundleName);
        this.d("damon",assetPath);
        this.d("damon",resPath);
    }

    public string name;
    public GameObject gameObject;

    public bool active {
        get {
            return gameObject.activeSelf;
        }
        set { gameObject.SetActive (value); }
    }
    #region  生命周期

    public void OnDestroy () {

    }
    public void OnAwake () {

    }

    public void OnStart () {

    }

    public void OnUpdate () {

    }

    #endregion

    public void Reset () { }
}