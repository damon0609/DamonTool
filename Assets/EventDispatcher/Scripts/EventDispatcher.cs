using System;
using System.Collections.Generic;

namespace Damon.Tool.Events {
    public class EventDispatcher {
        private Dictionary<int, Listener> mRegisterListeners = new Dictionary<int, Listener> ();
        private Dictionary<string, List<int>> mRegisterListenerEvents = new Dictionary<string, List<int>> (); //支持同一事件名下多委托函数的调用
        private Stack<Listener> mFreeListener = new Stack<Listener> ();
        private int id = -1;

        private Queue<Dispatched> dispatchers = new Queue<Dispatched> ();
        private Stack<Dispatched> mFreeDispathers = new Stack<Dispatched> ();

        public void Dispatcheding () {
            while (dispatchers.Count > 0) {
                Dispatched d = dispatchers.Dequeue ();
                Call (d.dispatchName, d.obj1, d.obj2, d.obj3);
                mFreeDispathers.Push (d); //将使用过的派发器添加到栈顶
            }
        }

        #region 事件触发部分
        public void Trigger (string eventName) {
            Call (eventName, null, null, null);
        }
        public void Trigger (string eventName, object obj1) {
            Call (eventName, obj1, null, null);
        }

        public void Trigger (string eventName, object obj1, object obj2) {
            Call (eventName, obj1, obj2, null);
        }

        public void Trigger (string eventName, object obj1, object obj2, object obj3) {
            Call (eventName, obj1, obj2, obj3);
        }

        private void Call (string eventName, object obj1, object obj2, object obj3) {
            List<int> listenerList;
            if (mRegisterListenerEvents.TryGetValue (eventName, out listenerList)) {
                for (int i = listenerList.Count - 1; i >= 0; --i) {
                    Listener listener;
                    if (mRegisterListeners.TryGetValue (listenerList[i], out listener)) {
                        if (listener.mAction != null)
                            listener.mAction (obj1, obj2, obj3);

                        mFreeListener.Push (listener); //回收侦听器
                        mRegisterListeners.Remove (listenerList[i]);
                        listenerList.RemoveAt (i);
                    } else
                        listenerList.RemoveAt (i);
                }
                if (listenerList.Count == 0)
                    mRegisterListenerEvents.Remove (eventName);
            }
        }
        #endregion

        #region 事件注册部分
        public int Register<T1, T2, T3> (string eventName, Func<T1, T2, T3, bool> func) {
            return On (eventName, delegate (object obj1, object obj2, object obj3) {
                T1 t1;
                try { t1 = (T1) obj1; } catch { t1 = default (T1); }
                T2 t2;
                try { t2 = (T2) obj2; } catch { t2 = default (T2); }
                T3 t3;
                try { t3 = (T3) obj3; } catch { t3 = default (T3); }
                return func (t1, t2, t3);
            });
        }
        public int Register<T1, T2, T3> (string eventName, Action<T1, T2, T3> action) {
            return On (eventName, delegate (object obj1, object obj2, object obj3) {
                T1 t1;
                try { t1 = (T1) obj1; } catch { t1 = default (T1); }
                T2 t2;
                try { t2 = (T2) obj2; } catch { t2 = default (T2); }
                T3 t3;
                try { t3 = (T3) obj3; } catch { t3 = default (T3); }
                action (t1, t2, t3);
                return true;
            });
        }

        public int Register<T1, T2> (string eventName, Action<T1, T2> action) {
            return On (eventName, delegate (object obj1, object obj2, object obj3) {

                T1 t1;
                try { t1 = (T1) obj1; } catch { t1 = default (T1); }
                T2 t2;
                try { t2 = (T2) obj2; } catch { t2 = default (T2); }
                action (t1, t2);
                return true;
            });
        }

        public int Register<T> (string eventName, Action<T> action) {
            return On (eventName, delegate (object obj1, object obj2, object obj3) {
                T t1;
                try {
                    t1 = (T) obj1;
                } catch {
                    t1 = default (T);
                }
                action (t1);
                return true;
            });
        }

        public int Register (string eventName, Action action) {
            return On (eventName, delegate (object obj1, object obj2, object obj3) {
                action ();
                return true;
            });
        }

        private int On (string eventName, Func<object, object, object, bool> func) {
            Listener listener = (mFreeListener.Count == 0) ? new Listener () : mFreeListener.Pop ();
            id++;

            listener.id = id;
            listener.mAction = func;

            if (mRegisterListeners != null)
                mRegisterListeners.Add (id, listener);

            List<int> eventListener;
            if (!mRegisterListenerEvents.TryGetValue (eventName, out eventListener)) {
                eventListener = mRegisterListenerEvents[eventName] = new List<int> ();
            }
            eventListener.Add (id);

            return id;
        }

        #endregion

        #region 事件派发器
        public void Dispatch (string dispathName) {
            Dispatched d = mFreeDispathers.Count > 0 ? mFreeDispathers.Pop () : new Dispatched ();
            d.Set (dispathName);
            dispatchers.Enqueue (d);
        }

        public void Dispatch (string dispathName, Object obj) {
            Dispatched d = mFreeDispathers.Count > 0 ? mFreeDispathers.Pop () : new Dispatched ();
            d.Set (dispathName, obj);
            dispatchers.Enqueue (d);
        }
        public void Dispatch (string dispathName, Object obj1, Object obj2) {
            Dispatched d = mFreeDispathers.Count > 0 ? mFreeDispathers.Pop () : new Dispatched ();
            d.Set (dispathName, obj1, obj2);
            dispatchers.Enqueue (d);
        }

        public void Dispatch (string dispathName, Object obj1, Object obj2, Object obj3) {
            Dispatched d = mFreeDispathers.Count > 0 ? mFreeDispathers.Pop () : new Dispatched ();
            d.Set (dispathName, obj1, obj2, obj3);
            dispatchers.Enqueue (d);
        }
        #endregion

        public class Dispatched {
            public string dispatchName;
            public Object obj1, obj2, obj3;

            public Dispatched Set (string name, Object obj1 = null, Object obj2 = null, Object obj3 = null) {
                this.dispatchName = name;
                this.obj1 = obj1;
                this.obj2 = obj2;
                this.obj3 = obj3;
                return this;
            }

        }

        class Listener {
            public int id;
            public Func<object, object, object, bool> mAction;
        }
    }
}