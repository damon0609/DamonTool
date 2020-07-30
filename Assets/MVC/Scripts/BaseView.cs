using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class CSVView : BaseView
{
    public CSVView(IController controller) : base(controller)
    {

    }
   
}
public class BaseView : IView {
    protected IController mController;
    public BaseView (IController controller) {
        this.mController = controller;
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

    private Vector2 pos;

    public void Init () {

    }

    public void OnDestroy () {

    }
    public void OnGUI () {
        Button ("GenterateCSV");

    }
    protected virtual void Label(IList<string> list){
        GUILayout.Box ("");
        pos = GUILayout.BeginScrollView (pos);
        for (int i = 0; i < list.Count; i++) {
            GUILayout.Label (list[i]);
        }
        GUILayout.EndScrollView ();
    }

    protected virtual void Button (string name) {
        if (GUILayout.Button (name)) {
            ((BaseController) controller).GenerateCSV ();
        }
    }
}