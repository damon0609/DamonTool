using Damon.Tool.Pool;
using System;
using System.Threading;
using UnityEngine;
namespace Damon.Tool.ThreadPool
{
    public class ThreadInfo
    {
        public int priority;
        private bool mIsWork = false;
        public bool isWorking
        {
            get { return mIsWork; }
            set
            {
                if (mIsWork != value)
                {
                    mIsWork = value;
                    if(mIsWork)
                        thread.Start();
                }
            }
        }
        private Thread thread;
        private Action mAction;

        public ThreadInfo(int id, bool isWork, Action action)
        {
            this.priority = id;
            this.mAction = action;
            this.thread = new Thread(() =>
            {
                if (action != null)
                    action();
            });
            this.isWorking = isWork;
        }
    }

    public class ThreadManager
    {
        public ThreadPool threadPool;

        public ThreadManager()
        {
            threadPool = new ThreadPool(()=> {
                Debug.Log("分配池中对象");
                ThreadInfo info = new ThreadInfo(1,true,()=> { Debug.Log("开启线程中回调函数"); });
                return info;
            },()=> { Debug.Log("回收时回调函数"); });

        }

        public ThreadInfo Allocate()
        {
            ThreadInfo threadInfo = threadPool.Allocate();
            return threadInfo;
        }

        public void Recycle(ThreadInfo info)
        {
            threadPool.Recycle(info);
        }


        public class ThreadFactory<T> : IObjectFactory<T>
        {
            Func<T> mFunc;
            public ThreadFactory(Func<T> action)
            {
                this.mFunc = action;
            }
            public T Create()
            {
                return mFunc();
            }
        }

        //在这里已经将泛型类型具象化
        public class ThreadPool : Pool<ThreadInfo>
        {
            Func<ThreadInfo> mFunc;
            Action mAction;
            public ThreadPool(Func<ThreadInfo> func,Action action)
            {
                mFactory = new ThreadFactory<ThreadInfo>(func);
                mAction = action;
            }

            public override bool Recycle(ThreadInfo obj)
            {
                mCacheStack.Push(obj);
                if (mAction != null)
                    mAction();
                return true;
            }
        }
    }
}
