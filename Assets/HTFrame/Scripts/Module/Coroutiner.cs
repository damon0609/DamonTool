using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
namespace HT
{
    //协程管理器
    [InternalModule(HTFrameworkModuleType.Coroutiner)]
    public class Coroutiner : InternalBaseModule
    {
        internal Dictionary<string, CoroutineEnumerator> coroutineEnumerators = new Dictionary<string, CoroutineEnumerator>();//根据id对迭代器进行分类

        internal Dictionary<Delegate, List<CoroutineEnumerator>> warehouse = new Dictionary<Delegate, List<CoroutineEnumerator>>(); //根据委托类型对迭代器进行分类
        public override void OnTermination()
        {
            base.OnTermination();
        }

        //这里的CoroutineAction只是委托,只需要将方法名填入形参即可，而不是协程方法
        public string Run(CoroutineAction action)
        {
            CoroutineEnumerator c = (CoroutineEnumerator)Main.referencePoolManager.OnSpwn<CoroutineEnumerator>();
            c.Fill(action, null);
            c.Run();

            if (!coroutineEnumerators.ContainsKey(c.id))
                coroutineEnumerators.Add(c.id, c);

            Addwarehouse(c);
            return c.id;
        }
        //这里的第二个参数是委托调用中的需要使用的参数
        public string Run<T>(CoroutineAction<T> action, T t1)
        {
            CoroutineEnumerator c = (CoroutineEnumerator)Main.referencePoolManager.OnSpwn<CoroutineEnumerator>();
            c.Fill(action, new object[] { t1 });
            c.Run();

            if (!coroutineEnumerators.ContainsKey(c.id))
                coroutineEnumerators.Add(c.id, c);
            Addwarehouse(c);

            return c.id;
        }

        public string Run<T1, T2>(CoroutineAction<T1, T2> action, T1 t1, T2 t2)
        {
            CoroutineEnumerator c = (CoroutineEnumerator)Main.referencePoolManager.OnSpwn<CoroutineEnumerator>();
            c.Fill(action, new object[] { t1, t2 });
            c.Run();

            if (!coroutineEnumerators.ContainsKey(c.id))
                coroutineEnumerators.Add(c.id, c);
            Addwarehouse(c);

            return c.id;
        }

        public void Return(string id)
        {
            if (!coroutineEnumerators.ContainsKey(id))
            {
                UnityEngine.Debug.Log("不包含指定id");
            }
            else
            {
                coroutineEnumerators[id].Return();
            }
        }
        //停止指定id的协程
        public void Stop(string id)
        {
            if (!coroutineEnumerators.ContainsKey(id))
            {
                UnityEngine.Debug.Log("不包含指定id");
            }
            else
            {
                coroutineEnumerators[id].Stop();
            }
        }
        //停止指定委托的协程，这里为什么会使用委托类来停止协程，主要是因为委托中可以在通过一个协程中使用不同的参数
        public void Stop(Delegate del)
        {
            if (!warehouse.ContainsKey(del))
            {
                UnityEngine.Debug.Log("不包含指定对象");
            }
            else
            {
                List<CoroutineEnumerator> list = warehouse[del];
                foreach (CoroutineEnumerator c in list)
                {
                    c.Stop();
                }
            }
        }

        public void ClearNotRunning()
        {
            Dictionary<string, CoroutineEnumerator> notRunning = new Dictionary<string, CoroutineEnumerator>();
            foreach (CoroutineEnumerator ci in coroutineEnumerators.Values)
            {
                if (ci.state != CoroutineState.Running)
                {
                    if (!notRunning.ContainsKey(ci.id))
                    {
                        notRunning.Add(ci.id, ci);
                    }
                }
            }

            foreach (CoroutineEnumerator c in notRunning.Values)
            {
                coroutineEnumerators.Remove(c.id);
                Removewarehouse(c);
                Main.referencePoolManager.OnDeSpawn(c);
            }
            notRunning = null;
        }
        //判断指定id的运行状态
        public bool IsRuning(string id)
        {
            if (!coroutineEnumerators.ContainsKey(id)) return false;
            else
                return coroutineEnumerators[id].state == CoroutineState.Running ? true : false;
        }
        private void Addwarehouse(CoroutineEnumerator c)
        {
            if (!warehouse.ContainsKey(c.action))
                warehouse.Add(c.action, new List<CoroutineEnumerator>() { c });
            else
                warehouse[c.action].Add(c);
        }
        private void Removewarehouse(CoroutineEnumerator c)
        {
            if (warehouse.ContainsKey(c.action))
            {
                warehouse[c.action].Remove(c);
                if (warehouse[c.action].Count <= 0)
                    warehouse.Remove(c.action);
            }
        }
    }
    ///缺少状态结束时的回调
    public class CoroutineEnumerator : IEnumerator, IReference
    {
        private string mID;
        public string id
        {
            get { return mID; }
        }
        private Delegate mAction;
        public Delegate action
        {
            get { return mAction; }
        }
        private object[] args;
        private object targetObj;
        private CoroutineState mState;
        private IEnumerator mEnumerator;
        private Coroutine mCoroutine;

#if UNITY_EDITOR

        public StackTrace StackTraceInfo { get; private set; }
        public DateTime createTime { get; private set; }
        public DateTime stopTime { get; private set; }
        public double elapsedTime { get; private set; }
        public int RerunNumber { get; private set; }

        public void RerunInEditor()
        {
            if (state == CoroutineState.Running)
            {
                Main.coroutiner.StopCoroutine(mCoroutine);
                state = CoroutineState.Stop;
            }

            mEnumerator = mAction.Method.Invoke(targetObj, args) as IEnumerator; //调用绑定的委托函数的返回值
            if (mEnumerator != null)
            {
                mCoroutine = Main.coroutiner.StartCoroutine(this);
                state = CoroutineState.Running;
                RerunNumber += 1;
            }
        }
#endif
        //运行协程
        public void Run()
        {
            mEnumerator = mAction.Method.Invoke(targetObj, args) as IEnumerator;//这里的用法不懂
            if (mEnumerator != null)
            {
                mCoroutine = Main.coroutiner.StartCoroutine(this);
                mState = CoroutineState.Running;

#if UNITY_EDITOR
                StackTraceInfo = new StackTrace(true);
                RerunNumber = 1;
#endif
            }
        }

        //停止当前协程
        public void Stop()
        {
            if (state == CoroutineState.Running)
            {
                Main.coroutiner.StopCoroutine(mCoroutine);
                state = CoroutineState.Stop;
            }
        }
        //重新唤起时需要判断是否在运行状态，如果在运行状态中先停止然后在重新启动
        public void Return()
        {
            Stop();
            mEnumerator = mAction.Method.Invoke(targetObj, args) as IEnumerator;
            if (mEnumerator != null)
            {
                mCoroutine = Main.coroutiner.StartCoroutine(this);
                state = CoroutineState.Running;
#if UNITY_EDITOR
                StackTraceInfo = new StackTrace(true);
                RerunNumber += 1;
#endif
            }
        }
        public CoroutineState state
        {
            get { return mState; }
            set
            {
                mState = value;
                switch (mState)
                {
                    case CoroutineState.Stop:
                        stopTime = DateTime.Now;
                        break;
                    case CoroutineState.Running:
                        createTime = DateTime.Now;
                        break;
                    case CoroutineState.Finish:
                        elapsedTime = (stopTime - createTime).TotalSeconds;
                        break;
                }
            }
        }

        //给协程绑定委托函数
        public CoroutineEnumerator Fill(Delegate action, object[] args)
        {
            mID = Guid.NewGuid().ToString();
            this.mAction = action;
            this.args = args;
            this.targetObj = action.Target;

            return this;
        }
        public object Current
        {
            get { return mEnumerator.Current; }
        }

        public bool MoveNext()
        {
            bool isNext = mEnumerator.MoveNext();//mEnumerator调用委托函数返回的迭代器，如果迭代器没有下一个则结束
            if (!isNext)
            {
                state = CoroutineState.Finish;
            }
            return isNext;
        }

        public void Reset()
        {
            mAction = null;
            targetObj = null;
            args = null;
            mEnumerator = null;
            mCoroutine = null;

#if UNITY_EDITOR
            StackTraceInfo = null;
#endif
        }
    }

    public enum CoroutineState
    {
        Running,
        Stop,
        Finish,
    }
}
