 using System.Collections.Generic;
 using System.Collections;
 using System;
 using Damon.Tool;
 using Damon;
 namespace HT {

     [InternalModule (HTFrameworkModuleType.Event)]
     public class EventManager : InternalBaseModule {

         private Dictionary<Type, DAction<Object, BaseEvent>> mEvents = new Dictionary<Type, DAction<object, BaseEvent>> ();

         #region  注册事件和移除事件
         public void Register (Type type, DAction<Object, BaseEvent> action) {
             if (!mEvents.ContainsKey (type)) {
                 mEvents.Add (type, action);
             } else {
                 mEvents[type] += action;
             }
             this.d ("Register", type.ToString ());
         }
         public void Register<T> (DAction<Object, BaseEvent> action) where T : BaseEvent {
             Type t = typeof (T);
             Register (t, action);
         }
         public void UnRegister (Type type) {
             DAction<Object, BaseEvent> action;
             if (mEvents.ContainsKey (type)) {
                 action = mEvents[type];
                 mEvents[type] -= action;
                 mEvents.Remove (type);
                 this.d ("UnRegister", type.ToString (), false);
             }
         }
         public void UnRegister<T> () where T : BaseEvent {
             Type t = typeof (T);
             UnRegister (t);
         }
         #endregion

         #region  触发事件
         public void Trigger (Object sender, BaseEvent e) {
             DAction<Object, BaseEvent> action;
             if (mEvents.TryGetValue (e.GetType (), out action)) {
                 if (action != null)
                     action (sender, e);
                 this.d ("damon", e.GetType ().ToString ());
             }
         }
         #endregion

         #region  清除事件
         public void ClearEvent (Type t) {
             DAction<Object, BaseEvent> action = null;
             if (mEvents.TryGetValue (t, out action)) {
                 mEvents[t] = null;
                 mEvents.Remove (t);
             }
         }
         public void ClearEvent<T> () {
             Type t = typeof (T);
             ClearEvent (t);
         }

         #endregion

         #region  生命周期函数
         public override void OnInitialization () {
             base.OnInitialization ();
             List<Type> types = GlobalTool.GetRuntimeTypes ();
             foreach (Type t in types) {
                 if (t.IsSubclassOf (typeof (BaseEvent))) {
                     if (!mEvents.ContainsKey (t)) {
                         mEvents.Add (t, null);
                     }
                 }
             }
         }
         public override void OnPause () {
             base.OnPause ();
         }

         public override void OnPreparatory () {
             base.OnPreparatory ();
         }

         public override void OnRefresh () {
             base.OnRefresh ();
         }

         public override void OnResume () {
             base.OnResume ();
         }

         public override void OnTermination () {
             base.OnTermination ();
         }
         #endregion
     }
 }