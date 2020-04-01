using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[GitHubURL("https://github.com/")]
[CSDNURL("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.jpg")]
[CustomEditor(typeof(DateSetManager))]
public class DateSetManagerInspector : HTBaseEditor<DateSetManager>
{
    private DateSetManager dateSetManager;

    protected override void OnDefaultEnable()
    {
        base.OnDefaultEnable();
        dateSetManager = e as DateSetManager;
    }

    protected override void OnDefaultInspectorGUI()
    {
        base.OnDefaultInspectorGUI();
    }
}
