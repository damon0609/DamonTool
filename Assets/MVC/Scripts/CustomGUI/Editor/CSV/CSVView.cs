using System.Collections;
using System.Collections.Generic;
using Damon.CustomGUI;
using Damon.MVC;
using UnityEditor;
using UnityEngine;
public class CSVView : BaseView {

    private CSVController csvCtrl;
    private List<CSVModel.CSVInfo> list;
    private GUIStyle label;
    private List<Button01> buttons = new List<Button01> ();
    public CSVView (IController controller, string name) : base (controller, name) {
        label = new GUIStyle ("Button");
        label.normal.textColor = Color.red;
    }
    public override void Init () {
        csvCtrl = controller as CSVController;
        list = csvCtrl.csvList;
        foreach (var item in list) {
            Button01 btn = new Button01 (item.name);
            buttons.Add (btn);
        }
        for (int i = 0; i < buttons.Count; i++) {
            Button01 btn = buttons[i];
            btn.style = label;
            btn.onClick += () => { Debug.Log (btn.label); };
            if (i % 2 == 0) {
                btn.drawer += r => {
                    if(GUILayout.Button (btn.label+"---"))
                    {
                        btn.onClick();
                    }
                };
            }
        }
    }
    public override void OnGUI () {
        GUILayout.BeginVertical ();
        foreach (var item in buttons) {
            item.OnDraw ();
        }
        GUILayout.EndVertical ();
    }
}