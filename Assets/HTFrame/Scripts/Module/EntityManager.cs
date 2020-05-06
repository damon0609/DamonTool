using System;
using System.Collections.Generic;
using System.Reflection;
using Damon;
using Damon.Tool;
using UnityEngine;
namespace HT {
  [InternalModule (HTFrameworkModuleType.Entity)]
  public class EntityManager : InternalBaseModule {

    [SerializeField]
    private bool isInit = false;

    //实体对象的名称
    [SerializeField]
    private List<string> defineEntityNames = new List<string> ();

    //每个实体对象对应的游戏对象
    [SerializeField]
    private List<GameObject> defineEntityGos = new List<GameObject> ();

    //根据类型名称存储对应的游戏对象
    private Dictionary<string, GameObject> mDefineEntity = new Dictionary<string, GameObject> ();
    private Transform mEntityRoot;

    //存放各个实体类的组
    private Dictionary<Type, GameObject> mEntityGroup = new Dictionary<Type, GameObject> ();

    //存放实体对象
    private Dictionary<Type, List<Entity>> mEntities = new Dictionary<Type, List<Entity>> ();

    //管理实体类的游戏对象
    private Dictionary<Type, Queue<GameObject>> mObjectPool = new Dictionary<Type, Queue<GameObject>> ();

    #region  程序中动态创建实体
    public void CreateEntity (Type type, string entityName, DAction<float> loadingAction = null, DAction<Entity> loadDoneAction = null) {
      Entity temp = null;
      if (mEntities.ContainsKey (type)) {//字典中包含该类
        List<Entity> list = mEntities[type];
        if (list.Count > 0) { 
          temp = list[list.Count - 1];
          temp.Reset ();
        } else {
          Entity entity = Activator.CreateInstance<Entity> ();
          mEntities[type].Add (entity);
        }
      } else {//字典中不包含此类
        List<Entity> list = new List<Entity> ();
        Entity entity = Activator.CreateInstance<Entity> ();
        list.Add (entity);
        mEntities[type] = list;
        temp = entity;
      }
      if (loadDoneAction != null) {
        loadDoneAction (temp);
      }
    }
    private void ExecuteCreateEntity (Type type, string entityName = "", DAction<float> loadingAction = null, DAction<Entity> loadDoneAction = null) {
      EntityInfoAttribute att = (EntityInfoAttribute) type.GetCustomAttributes (false) [0];
      if (att == null) {
        this.e ("damon", "资源信息未添加");
        return;
      }

      //取回一个实体对象并且进行初始化
      Entity entity = (Entity) Main.referencePoolManager.OnSpawn (type);
      GameObject go = null;
      entity.Reset ();
      string typeName = type.GetType().FullName;
      if (mEntities.ContainsKey (type))
      {
        if (att.isUseObject) {
          if (mObjectPool.Count > 0) {
            go = mObjectPool[type].Dequeue ();
          } else {

          }
        } 
        else 
        {
          if(mDefineEntity.ContainsKey(typeName)&&(mDefineEntity.ContainsKey(typeName)!=null))
          {
            GameObject prefab = mDefineEntity[typeName];
            go = Instantiate(prefab);
            go.SetParent(mEntityGroup[type]);
            entity.gameObject = go;
          }
          else
          {
              //需要将游戏对象加载到容器中
          }

        }
        go.SetParent (mEntityGroup[type]);
        entity.gameObject = go;
        mEntities[type].Add(entity);
        entity.name = (entityName == "" ? type.Name : entityName);
        entity.OnAwake ();
        entity.active = true;
        entity.OnStart ();
      } else { //这里字典中包含该类型
        this.e ("damon", "不是指定的类型");
      }
    }

    #endregion

    #region  生命周期
    public override void OnInitialization () {
      base.OnInitialization ();
      mEntityRoot = GameObject.Find ("EntityRoot").transform;

      for (int i = 0; i < defineEntityNames.Count; i++) {
        if (!mDefineEntity.ContainsKey (defineEntityNames[i])) {
          mDefineEntity.Add (defineEntityNames[i], defineEntityGos[i]);
        }
      }

      //初始化相关的数据 并且将指定的类型均已添加到实体字典中
      List<Type> types = GlobalTool.GetRuntimeTypes ();
      foreach (Type t in types) {
        if (!t.IsSubclassOf (typeof (Entity)))
          continue;
        System.Object[] objs = t.GetCustomAttributes (true);
        EntityInfoAttribute att = objs[0] as EntityInfoAttribute;
        if (att != null) {
          mEntities.Add (t, new List<Entity> ());

          //对放实体对象的容器
          GameObject group = GameObjectTool.NewGameObject (t.Name + "_[Group]", mEntityRoot.gameObject);
          mEntityGroup[t] = group;

          //按照实体的类型将队列初始化
          mObjectPool[t] = new Queue<GameObject> ();
          this.d ("+++++", "添加实体类型" + t.GetType (), false);
        } else {
          this.e ("damon", "未定义实体资源属性");
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
      mEntities.Clear ();

      mObjectPool.Clear ();
      foreach (GameObject go in mEntityGroup.Values) {
        go.DestroySelf ();
      }
      mEntityGroup.Clear ();
    }
    #endregion
  }
}