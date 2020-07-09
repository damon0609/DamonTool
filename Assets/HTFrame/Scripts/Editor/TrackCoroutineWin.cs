using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HT;

public class TrackCoroutineWin : HTBaseWindow
{
    private Coroutiner coroutine;
    private Vector2 scrollViewPos;

    private Color defaultColor;
    protected override void Initialize()
    {
        mActiveTitle = true;
        defaultColor = GUI.color;
    }
    public void Init(Coroutiner coroutine)
    {
        this.coroutine = coroutine;
    }
    protected override void OnBodyGUI()
    {
        base.OnBodyGUI();

        GUI.color = new Color(0.7f,0.7f,0.7f,1);
        EditorGUILayout.BeginVertical("Box");
        scrollViewPos = GUILayout.BeginScrollView(scrollViewPos);

        GUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        GUI.color = defaultColor;
    }
    private int _firstLineBlank = 40;
    private int _IDWidth = 300;
    private int _stateWidth = 100;
    private int _creationTimeWidth = 100;
    private int _stoppingTimeWidth = 100;
    private int _elapsedTimeWidth = 100;
    private int _rerunNumberWidth = 100;

    protected override void OnGUIWindowTitle()
    {
        GUILayout.Label("NO", EditorStyles.toolbarButton, GUILayout.Width(30));
        GUILayout.Label("ID", EditorStyles.toolbarButton, GUILayout.Width(100));
        GUILayout.Label("State", EditorStyles.toolbarButton, GUILayout.Width(100));
        GUILayout.Label("CreateTime", EditorStyles.toolbarButton, GUILayout.Width(100));
        GUILayout.Label("ElapsedTime", EditorStyles.toolbarButton, GUILayout.Width(100));
        GUILayout.Label("StopTime", EditorStyles.toolbarButton, GUILayout.Width(200));
        GUILayout.Label("Return Number", EditorStyles.toolbarButton, GUILayout.Width(100));
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Clear", EditorStyles.toolbarButton, GUILayout.Width(120)))
        {

        }
    }
    public void AddCoroutiner()
    {


    }
}
