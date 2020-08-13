using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.MVC {
    public interface IModel {
        
    }
    public interface IController {
        IModel model { get; set; }
    }
    public interface IView {
        string name { get; }
        IController controller { get; set; }
        bool isActive { get; set; }
        void Init ();
        void OnGUI ();
        void OnDestroy ();

    }
}