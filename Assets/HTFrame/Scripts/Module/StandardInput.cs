using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damon.Tool;
namespace HT {

    public class StandardInput : InputDevicesBase {

        public class Button {
            public string name;
            private bool mPress = false;
            private int mPressFrame = -5;
            private int mReleasedFrame = -5;
            public Button (string name) {
                this.name = name;
            }

            internal void Press () {

                if (mPress) return;
                mPress = true;
                mPressFrame = Time.frameCount;
            }

            internal void Released () {
                if (!mPress) return;
                mPress = false;
                mReleasedFrame = Time.frameCount;
            }

            internal bool GetButtonDown () {
                Press();
                return (mPressFrame - Time.frameCount == -1);
            }

            internal bool GetButtonUp()
            {
                Released();
                return mReleasedFrame - mPressFrame ==1;
            }
        }
        public class InputButtonType {
            public const string mouseLeft = "Mouse X";
            public const string mouseRight = "Mouse Y";
            public const string mouseWheel = "MouseMiddle";

        }

        public class InputAxis {
            public const string horizontal = "";
            public const string vertical = "";
        }

        public StandardInput () { }

        public override void Start () {
            base.Start ();
            Main.inputManager.RegisterButton ("Mouseleft", new StandardInput.Button (InputButtonType.mouseLeft));

              Main.inputManager.RegisterButton ("MouseRight", new StandardInput.Button (InputButtonType.mouseRight));
        }

        public override void Update () {
            base.Update ();
            if(Input.GetMouseButtonDown(0))
            {
                //Main.inputManager.GetButtonDown("Mouseleft");
            }
        }

        public override void OnDestroy () {
            base.OnDestroy ();
        }
    }
}