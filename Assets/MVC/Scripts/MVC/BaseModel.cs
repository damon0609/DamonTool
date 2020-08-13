using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Damon.MVC {
    public class BaseModel : IModel {

        public BaseModel () {
            Init ();
        }
        protected virtual void Init () {

        }
    }
}