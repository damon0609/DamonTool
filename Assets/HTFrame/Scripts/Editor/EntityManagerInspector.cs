using System;
using System.Collections.Generic;
using HT;
using UnityEditor;
using UnityEngine;
[CustomEditor (typeof (EntityManager))]
[GitHubURL ("https://github.com/")]
[CSDNURL ("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
public class EntityManagerInspector : HTBaseEditor<EntityManager> {
    private EntityManager dateSetManager;

    private SerializedProperty mEntityNames;
    private SerializedProperty mEntityGoes;

    private List<Type> mEntites = new List<Type> ();

    private Dictionary<string, bool> onDic = new Dictionary<string, bool> ();

    private List<string> btnStrs = new List<string> ();

    protected override void OnDefaultEnable () {
        base.OnDefaultEnable ();
        dateSetManager = e as EntityManager;
        mEntityNames = serializedObject.FindProperty ("defineEntityNames");
        mEntityGoes = serializedObject.FindProperty ("defineEntityGos");

        List<Type> list = GlobalTool.GetRuntimeTypes ();
        foreach (Type t in list) {
            if (t.IsSubclassOf (typeof (Entity))) {
                mEntites.Add (t);
            }
        }

    }

    protected override void OnDefaultInspectorGUI () {
        base.OnDefaultInspectorGUI ();
        EditorGUILayout.HelpBox ("Entity Manager,Controller all EntityLogic", MessageType.Info);

        GUILayout.BeginVertical ("Box");
        GUILayout.Label ("Entity:");

        EntityItem ();
        GUILayout.BeginHorizontal ();
        if (GUILayout.Button ("New", EditorStyles.miniButton)) {
            btnStrs.Add ("<None>");
        }
        if (GUILayout.Button ("Clear", EditorStyles.miniButton)) {
            btnStrs.Clear ();
            mEntityNames.ClearArray ();
        }
        GUILayout.EndHorizontal ();

        GUILayout.EndVertical ();
    }

    void EntityItem () {
        for (int i = 0; i < btnStrs.Count; i++) {
            GUILayout.BeginVertical ("Box");
            GUILayout.BeginHorizontal ();
            GUILayout.Label ("Type", GUILayout.Width (40));
            if (GUILayout.Button (btnStrs[i], EditorStyles.miniPullDown)) {
                int curIndex = i;
                GenericMenu gm = new GenericMenu ();
                for (int m = 0; m < mEntites.Count; m++) {
                    GUIContent temp = new GUIContent ();
                    temp.text = mEntites[m].FullName;

                    gm.AddItem (temp, " " == mEntites[m].FullName, (object obj) => {
                        GUIContent g = (GUIContent) obj;
                        btnStrs[curIndex] = g.text.ToString ();
                        mEntityNames.arraySize++;
                        int index = mEntityNames.arraySize - 1;
                        SerializedProperty pro = mEntityNames.GetArrayElementAtIndex (index);
                        Debug.Log ("添加:" + g.text+"---"+mEntityNames.arraySize);
                    }, temp);
                }
                gm.ShowAsContext ();
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button ("Delete", EditorStyles.miniButton, GUILayout.Width (40))) {

            }
            int count = mEntityNames.arraySize;
            Debug.Log ("----"+count);

            for (int n = 0; n < count; n++) {
                string tempName = mEntityNames.GetArrayElementAtIndex (n).stringValue;
                Debug.Log (tempName);
            }
            GUILayout.EndHorizontal ();

            GUILayout.Space (5);
            //Entity 项
            GUILayout.BeginHorizontal ();
            GUI.backgroundColor = Color.white;
            GUILayout.Label ("Enity", GUILayout.Width (40));
            EditorGUILayout.ObjectField (null, typeof (GameObject), true);
            GUILayout.EndHorizontal ();

            GUILayout.EndVertical ();
        }

    }
}