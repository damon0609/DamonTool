using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTest : MonoBehaviour {
    private IView view;

    void Start () {
        CSVModel model = new CSVModel();
        view = new BaseView (new CSVController(model));
        view.Init();
    }

    private void OnGUI() {
        if(view!=null)
        {
            view.OnGUI();
        }
    }

    private void OnDestroy() {
        view.OnDestroy();
    }

    void Update () {

    }
}