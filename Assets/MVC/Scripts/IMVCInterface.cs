using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModel {
    ModelKind modelType { get; set; }
}

public interface IController {
    IModel model { get; set; }
}

public interface IView {

    IController controller { get; set; }
    void Init ();
    void OnGUI ();
    void OnDestroy ();

}