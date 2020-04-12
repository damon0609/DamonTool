using System;
using System.Collections.Generic;
using Damon.Tool;
using UnityEngine;
namespace HT {
  [InternalModule (HTFrameworkModuleType.ReferencePool)]
  public class ReferencePoolManager : InternalBaseModule {

    private Dictionary<Type, ReferencePool> mReferencePools = new Dictionary<Type, ReferencePool> ();

    #region 回收引用
    public void OnDeSpawn (IReference re) {
      Type t = re.GetType ();
      ReferencePool referencePool = mReferencePools[t];
      referencePool.OnDeSpawn (re);

    }
    public void OnDeSpawn<T> (List<T> refes) where T : class, IReference, new () {
      Type type = typeof (T);
      if (!mReferencePools.ContainsKey (type)) {
        mReferencePools.Add (type, new ReferencePool ());
      }
      foreach (T item in refes) {
        mReferencePools[type].OnDeSpawn (item);
      }
      refes.Clear ();
    }

    public void OnDeSpawn<T> (T[] refes) where T : class, IReference, new () {
      Type type = typeof (T);
      if (!mReferencePools.ContainsKey (type)) {
        mReferencePools.Add (type, new ReferencePool ());
      }
      foreach (T item in refes) {
        mReferencePools[type].OnDeSpawn (item);
      }
    }

    #endregion

    #region  生成对象
    private ReferencePool GetReference (Type t) {
      ReferencePool referencePool = null;
      if (mReferencePools.ContainsKey (t)) {
        referencePool = mReferencePools[t];
      } else {
        referencePool = new ReferencePool ();
        mReferencePools[t] = referencePool;
      }
      return referencePool;
    }
    public IReference OnSpawn (Type type) {
      ReferencePool referencePool = GetReference (type);
      return referencePool.OnSpawn (type);
    }

    public T OnSpwn<T> (T type) where T : class, IReference, new () {
      T t = default (T);
      Type temp = type.GetType ();
      if (mReferencePools.ContainsKey (temp)) {
        t = (mReferencePools[temp].OnSpawn (temp)) as T;
      } else {
        ReferencePool pool = new ReferencePool ();
        t = pool.OnSpawn (temp) as T;
        mReferencePools[temp] = pool;
      }
      return t;
    }
    #endregion

    public void Clear (Type type) {
      if (mReferencePools.ContainsKey (type)) {
        mReferencePools[type] = null;
        mReferencePools.Remove (type);
      }
    }
    public void Clear () {
      foreach (Type item in mReferencePools.Keys) {
        Clear (item);
      }
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