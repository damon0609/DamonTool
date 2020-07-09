using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using HT;
[GitHubURL("https://github.com/")]
[CSDNURL("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
[CustomEditor(typeof(Coroutiner))]
public class CoroutinerInspector : HTBaseEditor<CoroutinerInspector>
{
    protected override void OnDefaultEnable()
    {
        base.OnDefaultEnable();
    }
    protected override void OnDefaultInspectorGUI()
    {
        base.OnDefaultInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Coroutiner, Execution and destruction of unified scheduling Coroutine!", MessageType.Info);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Coroutiner Tracker", "LargeButton"))
        {
            TrackCoroutineWin win = EditorWindow.GetWindow<TrackCoroutineWin>();
            win.titleContent = new GUIContent("TrackerCoroutine");
            win.minSize = new Vector2(400,400);
            win.maxSize = new Vector2(Screen.currentResolution.width,Screen.currentResolution.height);
            win.Init((Coroutiner)target);
            win.Show();
        }
        EditorGUILayout.EndHorizontal();

    }
}
