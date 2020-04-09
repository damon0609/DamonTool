using System;
using System.Collections.Generic;
using Damon.Tool;
using UnityEngine;
namespace HT {
  [InternalModule (HTFrameworkModuleType.ReferencePool)]
  public class ReferencePoolManager : InternalBaseModule {

    private Dictionary<Type, ReferencePool> mReferencePools = new Dictionary<Type, ReferencePool> ();

    public ReferencePool GetReference (Type t) {
      ReferencePool referencePool = null;
      if (mReferencePools.ContainsKey (t)) {
        referencePool = mReferencePools[t];
      } else {
        referencePool = new ReferencePool ();
        mReferencePools[t] = referencePool;
      }
      return referencePool;
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