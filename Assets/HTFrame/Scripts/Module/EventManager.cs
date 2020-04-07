 using System.Collections.Generic;
 using System.Collections;
 using System;
 using Damon;
 namespace HT {

     [InternalModule (HTFrameworkModuleType.Event)]
     public class EventManager : InternalBaseModule {

         private Dictionary<Type, DAction<Object, BaseEvent>> mEvents = new Dictionary<Type, DAction<object, BaseEvent>> ();

         #region  注册事件和移除事件
         public void Register (Type type, DAction<Object, BaseEvent> action) {

         }
         public void Register<T> () where T : BaseEvent {

         }
         public void UnRegister (Type type) {

         }
         public void UnRegister<T> () where T : BaseEvent {

         }
         #endregion

         #region  触发事件
         public void Trigger (Type type) {

         }
         #endregion

         #region  生命周期函数
         public override void OnInitialization () {
             base.OnInitialization ();

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