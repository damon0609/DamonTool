using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CSVWindow : EditorWindow {

    [MenuItem ("Tools/CSV")]
    public static void Createwin () {

        CSVWindow window = EditorWindow.GetWindow<CSVWindow> (true);
        window.titleContent = new GUIContent ("CSV");
        window.Show ();
    }

    private CSVView view;

    private CSVController mCSVController;
    private void OnEnable () {

        CSVModel model = new CSVModel ();
        mCSVController = new CSVController (model);
        view = new CSVView (mCSVController, "csv");
        view.Init ();

    }
    private void OnGUI () {
        if (view != null) {
            view.OnGUI ();
        }
    }

    private void OnDestroy () {
        if (view != null) {
            view.OnDestroy ();
        }
    }
}