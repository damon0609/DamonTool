using System.Collections.Generic;
using Damon;
using UnityEngine;
namespace HT {

  [InternalModule (HTFrameworkModuleType.ObjectPool)]
  public class ObjectPoolManager : InternalBaseModule {

    Dictionary<string, ObjectPool> mObjectPools = new Dictionary<string, ObjectPool> ();

    public void RegisterSpawnPool (string name, GameObject prefab, DAction<GameObject> onSpawn,
      DAction<GameObject> onDeSpawn) {
      if (!mObjectPools.ContainsKey (name)) {
        mObjectPools.Add (name, new ObjectPool (prefab, onSpawn, onDeSpawn));
      }
    }
    public void UnRegisterSpawnPool (string name) {
      if (mObjectPools.ContainsKey (name)) {
        mObjectPools[name].Clear ();
        mObjectPools.Remove (name);
      }
    }

    public int getCount (string name) {
      if (mObjectPools.ContainsKey (name)) {
        return mObjectPools[name].count;
      }
      return 0;
    }

    public override void OnInitialization () {
      base.OnInitialization ();
    }
    public override void OnPause () {

    }

    public override void OnPreparatory () {

    }

    public override void OnRefresh () {

    }

    public override void OnResume () {

    }

    public override void OnTermination () {

    }
  }
}