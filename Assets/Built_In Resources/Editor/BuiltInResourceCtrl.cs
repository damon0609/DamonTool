using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
public class BuiltInResourceCtrl : EditorWindow {
    private struct TextureInfo {
        public int id;
        public Texture texture;
        public string name;
        public Vector2 size;
        public string path;
    }
    private ReorderableList reorderableList;
    private Vector2 pos;
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
    
    [MenuItem ("Tool/BuiltIn Resources List")]
    static void OpenWin () {
        BuiltInResourceCtrl win = GetWindow<BuiltInResourceCtrl> (true);
        win.titleContent = new GUIContent ("BuiltInResources");
        win.Show ();
=======
>>>>>>> Stashed changes
    private bool on = false;

    private float horizontalRate = 0.5f;

    [MenuItem("Tool/BuiltIn Resources List")]
    static void OpenWin()
    {
        BuiltInResourceCtrl win = GetWindow<BuiltInResourceCtrl>(true);
        win.titleContent = new GUIContent("BuiltInResources");
        win.Show();
>>>>>>> ccd467e8f5c1afe6d06d85e6b3d18f4edf2ab905
    }

    private void OnEnable () {

        List<TextureInfo> infoes = new List<TextureInfo> ();
        Texture[] list = Resources.FindObjectsOfTypeAll (typeof (Texture)) as Texture[];
        for (int i = 0; i < list.Length; i++) {
            Texture t = list[i];
            TextureInfo info = new TextureInfo {
                id = i,
                name = t.name,
                texture = t,
                size = new Vector2 (t.width, t.height),
                path = string.IsNullOrEmpty (AssetDatabase.GetAssetPath (t)) ? "未知路径" : AssetDatabase.GetAssetPath (t)
            };
            infoes.Add (info);
        }

        reorderableList = new ReorderableList (infoes, typeof (Texture2D));
        reorderableList.headerHeight = EditorGUIUtility.singleLineHeight;
        reorderableList.elementHeight = 60;
        reorderableList.displayAdd = reorderableList.displayRemove = false;
        reorderableList.drawHeaderCallback += r => {
            EditorGUI.LabelField (r, "内置资源一览表");
        };
        reorderableList.drawElementCallback += DrawElement;
    }

    void DrawElement (Rect rect, int index, bool isActive, bool isFocused) {
        TextureInfo item = (TextureInfo) reorderableList.list[index];
        Rect texRect = new Rect (rect.x, rect.y + 20, 20, 20);
        GUI.DrawTexture (texRect, item.texture);

        Rect nameRect = new Rect (texRect.x + texRect.width + 5, rect.y, 200, EditorGUIUtility.singleLineHeight);
        GUI.Label (nameRect, "Name:" + item.name);

        Rect sizeRect = new Rect (texRect.x + texRect.width + 5, nameRect.y + EditorGUIUtility.singleLineHeight, 200, EditorGUIUtility.singleLineHeight);
        GUI.Label (sizeRect, "Size:" + item.size.ToString ());

        Rect pathRect = new Rect (texRect.x + texRect.width + 5, sizeRect.y + EditorGUIUtility.singleLineHeight, 200, EditorGUIUtility.singleLineHeight);
        GUI.Label (pathRect, "Path:" + item.path.ToString ());
    }

<<<<<<< HEAD
    private void OnGUI () {
        Rect rect = new Rect (0, 0, position.width / 2, position.height); //窗口矩形
        Rect viewRect = new Rect (rect.x, rect.y, rect.width, reorderableList.list.Count * 60); //视图矩形
        pos = GUI.BeginScrollView (rect, pos, viewRect);
        if (reorderableList != null)
            reorderableList.DoList (viewRect);
        GUI.EndScrollView ();
=======
    private void OnGUI()
    {
        Rect rect = new Rect(0, 0, (int)(position.width * horizontalRate), position.height);//窗口矩形
        HorizotalCtrl(rect, position);
        Rect viewRect = new Rect(rect.x, rect.y, rect.width, reorderableList.list.Count * 60);//视图矩形
        pos = GUI.BeginScrollView(rect, pos, viewRect);
        if (reorderableList != null)
            reorderableList.DoList(viewRect);
        GUI.EndScrollView();

        Rect otherRect = new Rect(rect)
        {
            x = rect.width + rect.x,
            width = position.width - rect.width,
        };
        // EditorGUI.DrawRect(otherRect,Color.gray);
<<<<<<< Updated upstream
    }
    void HorizotalCtrl(Rect preRect, Rect positon)
    {
        Rect rect = new Rect(preRect)
        {
            x = preRect.x + preRect.width - 5,
            width = 10,
        };
        Event e = Event.current;
        EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeHorizontal);
        if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            on = true;

        if (e.type == EventType.MouseUp)
            on = false;

        if (on && e.type == EventType.MouseDrag)
        {
            horizontalRate = e.mousePosition.x / position.width;
            Repaint();
        }

        EditorGUI.DrawRect(rect, Color.gray);
    }

    private void Update()
    {
=======
>>>>>>> Stashed changes
    }
    void HorizotalCtrl(Rect preRect, Rect positon)
    {
        Rect rect = new Rect(preRect)
        {
            x = preRect.x + preRect.width - 5,
            width = 10,
        };
        Event e = Event.current;
        EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeHorizontal);
        if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            on = true;

        if (e.type == EventType.MouseUp)
            on = false;

        if (on && e.type == EventType.MouseDrag)
        {
            horizontalRate = e.mousePosition.x / position.width;
            Repaint();
        }

        EditorGUI.DrawRect(rect, Color.gray);
    }

    private void Update()
    {
>>>>>>> ccd467e8f5c1afe6d06d85e6b3d18f4edf2ab905
    }
}