using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Damon.EditorTool;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomPropertyDrawer (typeof (SettingItem))]
public class SettingItemPropertyDrawer : CustomPropertyDrawerBase {
    public SettingItemPropertyDrawer () : base () {
    }
    private bool on = false;
    protected override void Init (SerializedProperty property) {
        base.Init (property);

        IncreaseIndent ();
        DrawCustom (r => {
            on = EditorGUI.Foldout (r, on, "SettingItem");
        }, EditorGUIUtility.singleLineHeight);
        DecreaseIndent ();

        IncreaseIndent ();
        DrawPropertyConditionally ("nameValue", on);
        DecreaseIndent ();

        IncreaseIndent ();
        DrawPropertyConditionally ("nameLabel", on);
        DecreaseIndent ();

        IncreaseIndent ();
        DrawPropertyConditionally ("button01", on);
        DecreaseIndent ();

        IncreaseIndent ();
        DrawPropertyConditionally ("button02", on);
        DecreaseIndent ();
    }
}