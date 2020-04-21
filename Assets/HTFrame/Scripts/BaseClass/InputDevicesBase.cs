using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damon.Tool;
namespace HT {
    public abstract class InputDevicesBase :ILog {

        protected bool mActive = true;
        public bool active {
            get { return mActive; }
            set { mActive = value; }
        }
        public virtual void Start () { }
        public virtual void Update () {

        }
        public virtual void OnDestroy () {

        }
    }

    public sealed class VirtualInputDevices:ILog {

        private Dictionary<string, StandardInput.Button > buttons = new Dictionary<string, StandardInput.Button > ();

        public bool GetButtonDown(string name)
        {
            bool on = false;
            if(buttons.ContainsKey(name))
            {
                on = buttons[name].GetButtonDown();
            }
            return on;
        }

        public void Register (string name, StandardInput.Button buttonType) {
            if (!buttons.ContainsKey (name)) {
                buttons[name] = buttonType;
            }
            else
            {
                this.d("damon","按钮已经被注册");
            }
        }
    }
}