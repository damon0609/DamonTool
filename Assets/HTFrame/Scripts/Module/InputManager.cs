using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Damon.Tool;
using UnityEngine;
using UnityEngine.Events;

namespace HT
{

    [InternalModule(HTFrameworkModuleType.Input)]
    public class InputManager : InternalBaseModule
    {

        public UnityEvent testEvent;

        private InputDevicesBase inputDevicesBase;
        private VirtualInputDevices mVirtualInputDevices;

        #region 注册按钮
        public void RegisterButton(string name, StandardInput.Button type)
        {
            if (mVirtualInputDevices != null)
            {
                mVirtualInputDevices.Register(name, type);
            }
        }
        #endregion

        #region  获取指定按钮的状态
        public bool GetButtonDown(string name)
        {
            return mVirtualInputDevices.GetButtonDown(name);
        }

        public bool GetButtonUp(string name)
        {
            return false;
        }
        public bool GetButton(string name)
        {
            return false;
        }
        #endregion

        public override void OnInitialization()
        {
            base.OnInitialization();
            mVirtualInputDevices = new VirtualInputDevices();
            inputDevicesBase = new StandardInput();
        }
        public override void OnPause()
        {
            inputDevicesBase.active = false;
        }
        public override void OnPreparatory()
        {

            if (inputDevicesBase != null)
                inputDevicesBase.Start();
        }
        public override void OnRefresh()
        {

            if (inputDevicesBase != null)
                inputDevicesBase.Update();
        }

        public override void OnResume()
        {
            inputDevicesBase.active = true;
        }

        public override void OnTermination()
        {

            if (inputDevicesBase != null)
                inputDevicesBase.OnDestroy();
        }
    }
}