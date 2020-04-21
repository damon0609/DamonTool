using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using HT;

[GitHubURL ("https://github.com/")]
[CSDNURL ("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
[CustomEditor (typeof (InputManager))]
public class InputManagerInspector : HTBaseEditor<InputManager> {
    private InputManager inputManager;

    protected override void OnDefaultEnable () {
        base.OnDefaultEnable ();
        inputManager = e as InputManager;
    }

    protected override void OnDefaultInspectorGUI () {
        base.OnDefaultInspectorGUI ();
    }
}