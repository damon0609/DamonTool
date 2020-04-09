using System;
using System.Collections.Generic;
using UnityEngine;
namespace HT {
  public sealed class ReferencePool {
    private int mLimited = 100;

    private Queue<IReference> mPool = new Queue<IReference> ();

    public ReferencePool (int count = 100) {
      if (count > mLimited) {
        Debug.Log ("超出引用数量的限制");
      } else {
        this.mLimited = count;
      }
    }
    //回收池中存在的对象
    public int count { get { return mPool.Count; } }
    public IReference OnSpawn (Type t) {
      IReference r = null;
      if (mPool.Count > 0 && mPool != null)
        r = mPool.Dequeue ();
      else
        r = Activator.CreateInstance (t) as IReference;
      return r;
    }

    public void OnDeSpawn (IReference r) {
      if (r == null) return;
      if (mPool.Count > mLimited)
        r = null;
      else {
        r.Reset ();
        mPool.Enqueue (r);
      }
    }
    public void Clear () {
      mPool.Clear ();
    }
  }
}