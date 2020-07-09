using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HT;
using System.Diagnostics;
public sealed class CoroutineItem : IEnumerator, IReference
{
    public Delegate targetDelegate;
    public object targetObject;

    private object[] args;

    private IEnumerator mIEnumber;
    public Coroutine mCoroutiner;

    public string id;

    public int resumeCount = 0;

#if UNITY_EIDOTR
    private DateTime mStartTime;
    private DateTime mStopTime;
    
#endif
    private StackTrace tracer;
    private CoroutineItemState mState;
    public CoroutineItemState state
    {
        get { return mState; }
        private set
        {
            mState = value;

#if UNITY_EIDOTR
            switch (mState)
            {
                case CoroutineItemState.Running:
                    mStartTime = DateTime.Now;
                    break;
                case CoroutineItemState.Stop:
                    mStopTime = DateTime.Now;
                    break;
                case CoroutineItemState.Finish:
                    elapsedTime = (DateTime.Now - mStartTime).TotalMilliseconds;
                    break;
            }
#endif
        }
    }


    private double elapsedTime;
    public void Run()
    {
        mIEnumber = targetDelegate.Method.Invoke(targetObject, args) as IEnumerator; //将委托绑定的方法转换程IEnumberator因为其返回类型为IEnumerator,被调用之后才可以返回IEnumerator类型
        if (mIEnumber != null)
        {
            mCoroutiner = Main.instance.StartCoroutine(this);
            state = CoroutineItemState.Running;
            resumeCount = 1;
        }


#if UNITY_EIDOTR
        tracer = new StackTrace(true);
#endif
    }

    //停止运行
    public void Stop()
    {
        if (state == CoroutineItemState.Running)
        {
            if (mCoroutiner != null)
                Main.instance.StopCoroutine(mCoroutiner);
            state = CoroutineItemState.Stop;
        }
    }

    //重新唤起执行
    public void Resume()
    {
        if (state == CoroutineItemState.Running)
        {
            if (mCoroutiner != null)
                Main.instance.StopCoroutine(mCoroutiner);
            state = CoroutineItemState.Stop;
        }
        mIEnumber = targetDelegate.Method.Invoke(targetObject, args) as IEnumerator; //将委托绑定的方法转换程IEnumberator因为其返回类型为IEnumerator
        if (mIEnumber != null)
        {
            mCoroutiner = Main.instance.StartCoroutine(this);
            state = CoroutineItemState.Running;
        }
#if UNITY_EIDOTR
        tracer = new StackTrace(true);
#endif
        resumeCount++;
    }

    public CoroutineItem Fill(Delegate dele, object[] args)
    {
        id = Guid.NewGuid().ToString();
        this.targetDelegate = dele;
        this.args = args;
        this.targetObject = targetDelegate.Target;
        return this;
    }
    public object Current
    {
        get { return mIEnumber.Current; }
    }

    public bool MoveNext()
    {
        bool isMoveNext = mIEnumber.MoveNext();
        if (!isMoveNext)
        {
            state = CoroutineItemState.Finish;
        }
        return isMoveNext;
    }

    public void Reset()
    {
        targetObject = null;
        targetDelegate = null;
        args = null;
        mIEnumber = null;
        mCoroutiner = null;
    }


    //标识枚举运行的状态
    public enum CoroutineItemState
    {
        Stop,
        Running,
        Finish,
    }
}


