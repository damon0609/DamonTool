using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Damon.MVC {
    public class BaseView : IView {

        protected string m_Name;

        public string name{
            get{return m_Name;}
        }
       
        protected IController mController;
        public BaseView (IController controller,string name) {
            this.mController = controller;
            this.m_Name = name;
            ViewRegister.Register(this);
        }
        public IController controller {
            get {
                return mController;
            }

            set {
                if (mController != value) {
                    mController = value;
                }
            }
        }
        protected bool mIsActive = true;
        public bool isActive {
            get { return mIsActive; }
            set { mIsActive = value; }
        }
        public virtual void Init () {

        }
        public virtual void OnDestroy () {

        }
        public virtual void OnGUI () {

        }
    }
}