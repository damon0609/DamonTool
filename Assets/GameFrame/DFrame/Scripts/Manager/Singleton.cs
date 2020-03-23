using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Reflection;
using System;
public interface ISingleton
{
    void OnSingletonInit();
}

public class SingletonCreater<T> where T : class, ISingleton
{
    protected static T mIntance;
    public static T GetInstance()
    {
        var constructors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
        ConstructorInfo info = Array.Find(constructors, c =>
        {
            return c.GetParameters().Length == 0;
        });
        if (info == null)
        {
            throw new NullReferenceException();
        }
        mIntance = info.Invoke(null) as T;
        mIntance.OnSingletonInit();
        return mIntance;
    }
}

public abstract class Singleton<T> : ISingleton where T : Singleton<T>
{
    protected static T mInstance;
    private static object objlock = new object();
    protected Singleton()
    {
    }
    public static T Instance
    {
        get
        {
            lock (objlock)
            {
                if (mInstance == null)
                {
                    mInstance = SingletonCreater<T>.GetInstance();
                }
            }
            return mInstance;
        }
    }

    public virtual void OnSingletonInit()
    {

    }

    public virtual void Dispose()
    {
        mInstance = null;
    }
}
