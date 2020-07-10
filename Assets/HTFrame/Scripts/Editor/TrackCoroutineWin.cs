using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HT;
using System;
using System.Diagnostics;
public class TrackCoroutineWin : HTBaseWindow
{
    private Coroutiner coroutine;
    private Vector2 scrollViewPos;

    private Vector2 traceScrollPos;
    private CoroutineEnumerator mCurEnumerator;//当前正在运行的
    private string traceInfo;
    private Dictionary<Delegate, bool> list = new Dictionary<Delegate, bool>();
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

        GUI.color = new Color(0.7f, 0.7f, 0.7f, 1);
        EditorGUILayout.BeginVertical("Box");
        scrollViewPos = GUILayout.BeginScrollView(scrollViewPos);
        ManagerCoroutiner();
        GUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        GUI.color = defaultColor;
        if (mCurEnumerator != null)
            OnGUITrace();
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
        GUILayout.Label("NO", EditorStyles.toolbarButton, GUILayout.Width(_firstLineBlank));
        GUILayout.Label("ID", EditorStyles.toolbarButton, GUILayout.Width(_IDWidth));
        GUILayout.Label("State", EditorStyles.toolbarButton, GUILayout.Width(_stateWidth));
        GUILayout.Label("CreateTime", EditorStyles.toolbarButton, GUILayout.Width(_creationTimeWidth));
        GUILayout.Label("StopTime", EditorStyles.toolbarButton, GUILayout.Width(_stoppingTimeWidth));
        GUILayout.Label("ElapsedTime", EditorStyles.toolbarButton, GUILayout.Width(_elapsedTimeWidth));
        GUILayout.Label("Return Number", EditorStyles.toolbarButton, GUILayout.Width(_rerunNumberWidth));
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Clear", EditorStyles.toolbarButton, GUILayout.Width(120)))
        {

        }
    }
    public void ManagerCoroutiner()
    {
        //将按delegate分类的协程重新包装到list
        int indexId = 1;
        foreach (var item in coroutine.warehouse)
        {
            if (!list.ContainsKey(item.Key))
                list.Add(item.Key, false);

            GUI.color = defaultColor;
            EditorGUILayout.BeginHorizontal("IN BigTitle");
            list[item.Key] = EditorGUILayout.Foldout(list[item.Key],
            string.Format("id = {0},目标对象 = {1},委托方法名 = {2}, 调用该方法的协程个数 = {3}", indexId, item.Key.Target.GetType().ToString(), item.Key.Method, item.Value.Count), true);
            EditorGUILayout.EndHorizontal();
            indexId++;
            if (list[item.Key])
            {
                int indexChild = 0;
                foreach (CoroutineEnumerator c in item.Value)
                {
                    GUI.color = mCurEnumerator == c ? Color.cyan : defaultColor;
                    EditorGUILayout.BeginHorizontal("Badge");
                    EditorGUILayout.LabelField(indexChild.ToString(), GUILayout.Width(_firstLineBlank));
                    EditorGUILayout.LabelField(c.id, GUILayout.Width(_IDWidth));
                    EditorGUILayout.LabelField(c.state.ToString(), GUILayout.Width(_stateWidth));
                    EditorGUILayout.LabelField(c.createTime.ToString("mm:ss:ff"), GUILayout.Width(_creationTimeWidth));
                    if (c.state == CoroutineState.Running)
                    {
                        EditorGUILayout.LabelField("--:--:--", GUILayout.Width(_stoppingTimeWidth));
                        EditorGUILayout.LabelField("--", GUILayout.Width(_elapsedTimeWidth));
                    }
                    else
                    {
                        EditorGUILayout.LabelField(c.stopTime.ToString("mm:ss:ff"), GUILayout.Width(_stoppingTimeWidth));
                        EditorGUILayout.LabelField(c.elapsedTime.ToString("f3"), GUILayout.Width(_elapsedTimeWidth));
                    }
                    EditorGUILayout.LabelField(c.RerunNumber.ToString(), GUILayout.Width(_rerunNumberWidth));
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Return"))
                    {
                        c.Return();
                    }
                    GUI.enabled = c.state == CoroutineState.Running ? true : false;
                    if (GUILayout.Button("Stop"))
                    {
                        c.Stop();
                    }
                    GUI.enabled = true;
                    EditorGUILayout.EndHorizontal();
                    indexChild++;

                    if (Event.current != null && Event.current.type == EventType.MouseDown)
                    {
                        Rect rect = GUILayoutUtility.GetLastRect();
                        if (rect.Contains(Event.current.mousePosition))
                        {
                            SelectedIEnumerator(c);
                            Event.current.Use();
                        }
                    }
                }
            }
            GUI.color = defaultColor;
        }
    }

    void OnGUITrace()
    {
        EditorGUILayout.BeginVertical("Box");
        traceScrollPos = GUILayout.BeginScrollView(traceScrollPos);
        EditorGUILayout.TextArea(traceInfo);
        GUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
    void SelectedIEnumerator(CoroutineEnumerator ie)
    {
        mCurEnumerator = ie;
        traceInfo = ie.id + "\r\n\r\n" + GetTraceInfo();
        Repaint();
    }

    string GetTraceInfo()
    {
        string assetPath = Application.dataPath.Replace("/","\\");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        StackFrame[] frames = mCurEnumerator.StackTraceInfo.GetFrames();
        foreach (StackFrame f in frames)
        {
            if (f.GetMethod().Name == "Run" || f.GetMethod().Name == "Rerun")
            {
                continue;
            }
            sb.Append(f.GetMethod().DeclaringType.FullName);
            sb.Append(".");
            sb.Append(f.GetMethod().Name+"()--");
            sb.Append("Path:");
            sb.Append(f.GetFileName().Replace(assetPath,""));
            sb.Append(":");
            sb.Append(f.GetFileLineNumber());
        }
        return sb.ToString();
    }


}
