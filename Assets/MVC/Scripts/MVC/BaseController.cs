using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Damon.MVC {
    public class BaseController : IController {
        private IModel mModel;
        public IModel model {
            get {
                return mModel;
            }

            set {
                if (mModel != value) {
                    mModel = value;
                }
            }
        }
        public BaseController (IModel model) {
            this.mModel = model;
        }

        public virtual void GenerateCSV () {
            Debug.Log ("GenerateCSV");
        }
    }
}