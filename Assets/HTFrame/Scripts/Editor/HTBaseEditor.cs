using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class HTBaseEditor<T> : Editor where T : UnityEngine.Object {
    private string gitURL;
    private string csdnURL;
    private Texture gitIcon;
    private Texture csdnIcon;
    private bool isEnableBaseInspector = false;
    private bool isRunTimeData = true;
    private Dictionary<string, SerializedProperty> mProperies = new Dictionary<string, SerializedProperty> ();
    protected T e;

    protected float FloatField (string name, float value) {
        return EditorGUILayout.FloatField (name, value);
    }

    protected void Button (Action action, string name, GUILayoutOption option) {
        if (GUILayout.Button (name, option)) {
            if (action != null)
                action ();
            HasChanged ();
            Undo.RecordObject (target, "Click Button");
        }
    }
    protected T ObjectField<T> (string name, T t, bool allowMutil = false, params GUILayoutOption[] option) where T : UnityEngine.Object {
        return EditorGUILayout.ObjectField (name, t, typeof (T), allowMutil, option) as T;
    }
    protected void Button (Action action, string name, GUIStyle style, GUILayoutOption option) {
        if (GUILayout.Button (name, style, option)) {
            if (action != null)
                action ();
            HasChanged ();
            Undo.RecordObject (target, "Click Button");
        }
    }

    protected SerializedProperty GetProperty (string name) {
        SerializedProperty pro = null;
        if (mProperies.ContainsKey (name))
            pro = mProperies[name];
        else {
            pro = serializedObject.FindProperty (name);
            if (pro != null)
                mProperies[name] = pro;
            else
                Debug.Log ("property name is error");
        }
        return pro;
    }

    private void OnEnable () {
        e = target as T;
        System.Object[] objs = GetType ().GetCustomAttributes (typeof (Attribute), false);
        foreach (System.Object obj in objs) {
            Type type = obj.GetType ();
            if (type == typeof (CSDNURLAttribute)) {
                CSDNURLAttribute csdn = (CSDNURLAttribute) (obj);
                csdnURL = csdn.url;
                csdnIcon = AssetDatabase.LoadAssetAtPath<Texture> (csdn.path);
            } else if (type == typeof (GitHubURLAttribute)) {
                GitHubURLAttribute git = (GitHubURLAttribute) obj;
                gitURL = git.url;
                gitIcon = AssetDatabase.LoadAssetAtPath<Texture> ("Assets/HTFrame/Assets/Texture/01.jpg");
            }
        }
        OnDefaultEnable ();
    }
    protected virtual void OnDefaultEnable () {

    }
    protected virtual void OnDefaultInspectorGUI () {

    }

    protected virtual void OnInspectorRuntimeGUI () {

    }

    public sealed override void OnInspectorGUI () {
        GUILayout.BeginHorizontal ();
        GUILayout.FlexibleSpace ();
        if (!string.IsNullOrEmpty (gitURL) && !string.IsNullOrEmpty (csdnURL)) {
            if (gitIcon == null || csdnIcon == null) return;
            if (GUILayout.Button (gitIcon, EditorStyles.miniButtonMid, GUILayout.Width (32), GUILayout.Height (32))) {
                Application.OpenURL (gitURL);
            }
            EditorGUIUtility.AddCursorRect (GUILayoutUtility.GetLastRect (), MouseCursor.Link);

            if (GUILayout.Button (csdnIcon, EditorStyles.miniButtonMid, GUILayout.Width (32), GUILayout.Height (32))) {
                Application.OpenURL (csdnURL);
            }
            EditorGUIUtility.AddCursorRect (GUILayoutUtility.GetLastRect (), MouseCursor.Link);
        }
        GUILayout.EndHorizontal ();

        if (isEnableBaseInspector) {
            base.OnInspectorGUI ();
        }

        OnDefaultInspectorGUI ();

        if (isRunTimeData && EditorApplication.isPlaying) {
            GUI.backgroundColor = Color.cyan;
            GUI.color = Color.white;

            GUILayout.BeginVertical (EditorStyles.helpBox);

            GUILayout.BeginHorizontal ();
            GUILayout.Label ("Runtime Data", EditorStyles.boldLabel);
            GUILayout.EndHorizontal ();

            OnInspectorRuntimeGUI ();

            GUILayout.EndVertical ();
        }

        serializedObject.ApplyModifiedProperties ();
    }

    protected void HasChanged () {
        if (!EditorApplication.isPlaying) {
            EditorUtility.SetDirty (target);
            Component com = target as Component;
            if (com != null && com.gameObject.scene != null) {
                EditorSceneManager.MarkSceneDirty (com.gameObject.scene);
            }
        }
    }
}