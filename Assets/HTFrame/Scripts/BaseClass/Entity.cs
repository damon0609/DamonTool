using Damon.Tool;
using UnityEngine;
using HT;
[EntityInfo ("1", "2", "3",true)]
public class EntityNPC : Entity {
    public EntityNPC () : base () { }
    public override void OnAwake () {
        this.d ("OnAwake", assetPath + "--" + assetBundleName + "--" + resPath);
        this.d ("OnAwake", "------EntityNPC");
    }
    public override void OnStart () {

    }
    public override void OnUpdate () {

    }
    public override void Reset () { }
    public override void OnDestroy () {

    }
}

[EntityInfo ("4", "5", "6",true)]
public class EntityMonster : Entity {
    public EntityMonster () : base () { }
    public override void OnAwake () {
        this.d ("OnAwake", assetPath + "--" + assetBundleName + "--" + resPath);
        this.d ("OnAwake", "------EntityMonster");
    }
    public override void OnStart () {

    }
    public override void OnUpdate () {

    }
    public override void Reset () { }

    public override void OnDestroy () {

    }
}
public class Entity : IReference {

    protected string assetBundleName;
    protected string assetPath;
    protected string resPath;

    public override string ToString () {
        return string.Format ("a=={0},b=={1},c=={2}", assetBundleName, assetPath, resPath);
    }
    public Entity () {
        System.Object[] attributes = this.GetType ().GetCustomAttributes (true);
        for (int i = 0; i < attributes.Length; i++) {
            EntityInfoAttribute a = attributes[i] as EntityInfoAttribute;
            assetBundleName = a.assetBundleName;
            assetPath = a.assetPath;
            resPath = a.resourcePath;
        }
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

    public virtual void OnDestroy () {
        gameObject.DestroySelf ();
    }
    public virtual void OnAwake () {

    }

    public virtual void OnStart () {

    }

    public virtual void OnUpdate () {

    }
    #endregion

    public virtual void Reset () { 
        
    }
}