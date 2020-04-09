using System;
using System.Collections.Generic;
using System.Reflection;
using Damon;
using Damon.Tool;
using UnityEngine;
namespace HT {
  [InternalModule (HTFrameworkModuleType.Entity)]
  public class EntityManager : InternalBaseModule {

    private Transform mEntityRoot;

    //存放各个实体类的组
    private Dictionary<Type, GameObject> mEntityGroup = new Dictionary<Type, GameObject> ();

    //存放实体对象
    private Dictionary<Type, List<Entity>> mEntities = new Dictionary<Type, List<Entity>> ();

    //管理实体类的游戏对象
    private Dictionary<Type, Queue<GameObject>> mObjectPool = new Dictionary<Type, Queue<GameObject>> ();

    #region  创建实体

    public void CreateEntity (Type type, string entityName, DAction<float> loadingAction = null, DAction<Entity> loadDoneAction = null) {
      if (mEntities.ContainsKey (type)) {
        List<Entity> list = mEntities[type];

      } else {
        List<Entity> list = new List<Entity> ();
        Entity entity = Activator.CreateInstance<Entity> ();
        list.Add (entity);
        mEntities[type] = list;
      }
    }

    private void ExecuteCreateEntity (Type type, string entityName = "", DAction<float> loadingAction = null, DAction<Entity> loadDoneAction = null) {
      ReferencePool pool = Main.referencePoolManager.GetReference (type);
      Entity entity = pool.OnSpawn (typeof (Entity)) as Entity;
      if (mEntities.ContainsKey (type)) {
        mEntities[type].Add (entity);
        entity.gameObject = mObjectPool[type].Dequeue ();
        entity.name = (entityName == "" ? type.Name : entityName);
        entity.OnAwake ();
        entity.active = true;
        entity.OnStart ();
        

      } else {

      }
    }

    #endregion

    #region  生命周期
    public override void OnInitialization () {
      base.OnInitialization ();
      mEntityRoot = GameObject.Find ("EntityRoot").transform;
      List<Type> types = GlobalTool.GetRuntimeTypes ();
      foreach (Type t in types) {
        if (!t.IsSubclassOf (typeof (Entity)))
          continue;
        System.Object[] objs = t.GetCustomAttributes (true);
        if (objs != null && objs.Length > 0) {
          mEntities.Add (t, new List<Entity> ());
          GameObject group = GameObjectTool.NewGameObject (t.Name + "_[Group]", mEntityRoot.gameObject);
          mEntityGroup[t] = group;

          //按照实体的类型将队列初始化
          mObjectPool[t] = new Queue<GameObject> ();
          this.d ("+++++", "添加实体类型" + t.GetType (), false);
        }
      }

      //实体的生命周期
      foreach (List<Entity> list in mEntities.Values) {
        foreach (Entity e in list) {
          e.OnAwake ();
        }
      }
    }
    public override void OnPause () {
      base.OnPause ();
    }
    public override void OnPreparatory () {
      base.OnPreparatory ();
    }
    public override void OnRefresh () {
      base.OnRefresh ();
      foreach (List<Entity> list in mEntities.Values) {
        foreach (Entity e in list) {
          e.OnUpdate ();
        }
      }
    }

    public override void OnResume () {
      base.OnResume ();
    }
    public override void OnTermination () {
      base.OnTermination ();
      foreach (List<Entity> list in mEntities.Values) {
        foreach (Entity e in list) {
          e.OnDestroy ();
        }
      }
      mEntities.Clear();

      mObjectPool.Clear();
      foreach(GameObject go in mEntityGroup.Values)
      {
          go.DestroySelf();
      }
      mEntityGroup.Clear();
    }
    #endregion
  }
}