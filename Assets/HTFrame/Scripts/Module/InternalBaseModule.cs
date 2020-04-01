using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;

public class InternalBaseModule : MonoBehaviour, IModule
{
    protected InternalModuleAttribute attribute;

    private bool mIsPause = false;
    public bool isPause
    {
        get { return mIsPause; }
        set
        {
            if (mIsPause == value) return;
            mIsPause = value;
        }
    }

    [SerializeField]
    protected HTFrameworkModuleType mModuleType;
    public HTFrameworkModuleType moduleType
    {
        get { return mModuleType; }
    }
    public virtual void OnInitialization()
    {
        System.Object[] objs = GetType().GetCustomAttributes(typeof(InternalModuleAttribute), true);
        for (int i = 0; i < objs.Length; i++)
        {
            if (typeof(InternalModuleAttribute) == objs[i].GetType())
            {
                attribute = (InternalModuleAttribute)(objs[i]);
                mModuleType = attribute.moduleType;
            }
        }
    }

    public virtual void OnPause()
    {

    }

    public virtual void OnPreparatory()
    {

    }

    public virtual void OnRefresh()
    {

    }

    public virtual void OnResume()
    {

    }

    public virtual void OnTermination()
    {

    }
}
