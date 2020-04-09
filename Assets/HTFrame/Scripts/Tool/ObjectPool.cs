using System;
using System.Collections.Generic;
using Damon;
using Damon.Tool;
using UnityEngine;
namespace HT {
  public class ObjectPool : ILog {
    private GameObject mPrefab;
    private int mLimited = 1;
    public int limited {
      get {
        return mLimited;
      }
      set {
        if (value == 0 && value > 100) {
          this.w ("limited", "对象池数量必须在1-100之间");
          return;
        }
        mLimited = value;
      }
    }
    private DAction<GameObject> mOnSpawn;
    private DAction<GameObject> mDeSpawn;
    private Queue<GameObject> mPool = new Queue<GameObject> ();

    private int mCount = 0;
    public int count {
      get { return mCount; }
    }
    public ObjectPool (GameObject prefab, DAction<GameObject> onSpawn = null, DAction<GameObject> deSpawn = null) {
      this.mPrefab = prefab;
      this.mOnSpawn = onSpawn;
      this.mDeSpawn = deSpawn;
    }
    public GameObject OnSpawn (string name) {
      GameObject obj = null;
      if (mPool.Count > 0) {
        obj = mPool.Dequeue ();
      } else {
        obj = GameObjectTool.Instantiated(mPrefab,name);
        obj.SetActive(true);
        mCount++;
      }
      if (mOnSpawn != null&&obj!=null)
        mOnSpawn (obj);
      return obj;
    }

    public void OnDeSpawn (GameObject go) {
      if (mPool != null) {
        mPool.Enqueue (go);
        go.SetActive (false);
        if (mDeSpawn != null)
          mDeSpawn (go);
      }
    }

    public void Clear () {
      while (mPool.Count > 0) {
        GameObject go = mPool.Dequeue ();
        if (go != null)
          go.DestroySelf ();
      }
    }
  }
}