using System.Collections;
using System.Collections.Generic;
using Damon.Tool;
using UnityEngine;
namespace PUI {
    public class PUIRoot : PUINode {

        [SerializeField]
        private float mDistance = 1;
        public float distance {
            get { return mDistance; }
            set {
                if (mDistance != value) {
                    if (value == 0) {
                        this.w ("mDistance", "UIRoot距离最好不设置为0");
                    }
                    mDistance = value;
                }
            }
        }
        [SerializeField]
        private float mHeight = 1.67f;
        public float height {
            get { return mHeight; }
            set {
                if (mHeight != value) {
                    mHeight = value;
                }
            }
        }

        private bool mIsShow = false;
        public bool isShow {
            set {
                mIsShow = value;
            }
        }
        private Color mColor = Color.red;
        public Color color { set { mColor = value;} }

        void Start () {

        }
        void Update () {

        }
        public void OnDrawGizmosItem () {
            GizmosTool.OnDrawRect();
            //GizmosTool.OnDrawSphere(transform.position,1f);

        }
    }
}