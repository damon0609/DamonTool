using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ProcedureWin : HTBaseWindow
{
    private int index;

    [SerializeField]
    private static List<Procedure> procedure = new List<Procedure>();
    private List<BezierInfo> list = new List<BezierInfo>();
    private Event e;
    private BezierInfo curBezier;
    private class BezierInfo
    {
        public bool isDrawing = false;
        public Vector3 startPos;
        public Vector3 endPos;

        public void DrawingBeziering(Vector3 endPos)
        {
            this.endPos = endPos;
            DrawBeziered();
        }
        public void DrawBeziered()
        {
            Vector3 startPos01 = startPos + Vector3.right * 50;
            Vector3 endPos01 = endPos + Vector3.left * 50;
            Handles.DrawBezier(startPos, endPos, startPos01, endPos01, Color.black, null, 5);
        }
    }
    protected override void Initialize()
    {
    }
    protected override void OnBodyGUI()
    {
        e = Event.current;

        DrawWinGroup();
        CreateProcedureItem();
        //DrawBezier();
    }

    void DrawWinGroup()
    {
        BeginWindows();
        foreach (var p in procedure)
        {
            p.OnGUI();
        }
        EndWindows();
    }
    void DrawBezier()
    {
        //绘制已经完成的贝塞尔
        for (int i = 0; i < list.Count; i++)
        {
            BezierInfo info = list[i];
            info.DrawBeziered();
        }

        //绘制中的贝塞尔
        if (curBezier != null && curBezier.isDrawing)
        {
            Vector3 endPos = new Vector3(e.mousePosition.x, e.mousePosition.y, 0);
            curBezier.DrawingBeziering(endPos);
            if (e.type == EventType.MouseUp)
            {
                curBezier.endPos = endPos;
                curBezier.isDrawing = false;
                list.Add(curBezier);
                curBezier = null;
            }
            Repaint();
        }
    }
    void CreateProcedureItem()
    {
        if (e.type == EventType.MouseDown && e.button == 1)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Create Procedure"), false, () =>
            {
                Procedure p = new Procedure(index, new Rect(e.mousePosition.x, e.mousePosition.y, 300, 250));
                procedure.Add(p);
                p.drawBezier += v =>
                {
                    BezierInfo info = new BezierInfo();
                    info.startPos = v;
                    info.isDrawing = true;
                    curBezier = info;
                };
                index++;
            });
            menu.ShowAsContext();
            e.Use();
        }
    }
}

[System.Serializable]
public class Procedure : UnityEngine.Object
{
    [SerializeField]
    public string procedureName;

    [SerializeField]
    public List<Procedure> targetProcedure;

    [SerializeField]
    private Dictionary<int, bool> toggles = new Dictionary<int, bool>();

    private int count;
    public Rect rect;
    public int index;
    private Vector2 pos;
    public Action<Vector3> drawBezier;

    private GUIContent g1;
    private GUIContent g2;
    public Procedure(int index, Rect rect)
    {
        g1 = new GUIContent("slider thumb@2x");
        g1.text = "绑定目标流程";
        g2 = new GUIContent("slider thumb act@2x");
        g2.text = "绑定目标流程";
        
        EditorGUIUtility.FindTexture("Folder Icon");


        this.rect = rect;
        this.index = index;
        targetProcedure = new List<Procedure>();

    }

    public void OnGUI()
    {
        rect = GUI.Window(index, rect, (int id) =>
               {
                   EditorGUILayout.BeginHorizontal();
                   EditorGUILayout.LabelField("Name:", GUILayout.MaxWidth(60));
                   procedureName = EditorGUILayout.TextField(procedureName);
                   GUILayout.FlexibleSpace();
                   EditorGUILayout.EndHorizontal();
                   GUILayout.Space(2);
                   EditorGUILayout.BeginHorizontal("Box");
                   EditorGUILayout.LabelField("目标状态个数:" + count, GUILayout.MaxWidth(100));
                   GUILayout.FlexibleSpace();
                   if (GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(30)))
                   {
                       toggles.Add(count, false);
                       count++;
                   }
                   if (GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(30)))
                   {
                       if (count > 0)
                       {
                           count--;
                           toggles.Remove(count);
                       }
                   }
                   EditorGUILayout.EndHorizontal();
                   DrawTargetProcedure(rect);
                   GUI.DragWindow();
               }, "Procedure_" + index);
    }

    void DrawTargetProcedure(Rect r)
    {
        EditorGUILayout.BeginVertical("Box");
        pos = EditorGUILayout.BeginScrollView(pos);
        int tempIndex = 0;
        for (int i = 0; i < count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Procedure:" + i, GUILayout.MaxWidth(100));
            // GUI.DrawTexture();
            // EditorGUILayout.(toggles[i]?g2:g1);
            Event e = Event.current;
            if (e.type == EventType.MouseDown && GUILayoutUtility.GetLastRect().Contains(e.mousePosition))
            {
                toggles[i] = true;
                tempIndex = i;
                Vector3 startPos = new Vector3(rect.x + rect.width, rect.y + rect.height / 2, 0);
                if (drawBezier != null)
                    drawBezier(startPos);
                e.Use();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
    public virtual void EnterProcedure()
    {

    }
    public virtual void ExitProcedure()
    {

    }
}
