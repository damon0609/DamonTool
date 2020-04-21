using System;
using System.Collections.Generic;
using HT;
using UnityEditor;
using UnityEngine;
[CustomEditor (typeof (EntityManager))]
[GitHubURL ("https://github.com/")]
[CSDNURL ("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
public class EntityManagerInspector : HTBaseEditor<EntityManager> {

    public struct EntityInfo {
        public string typeName;
        public GameObject entity;
    }

    private EntityManager dateSetManager;

    private SerializedProperty mEntityNames;
    private SerializedProperty mEntityGoes;
    private List<Type> mEntites = new List<Type> ();

    private Dictionary<string, bool> onDic = new Dictionary<string, bool> ();
    private List<int> disableList = new List<int> ();

    private List<EntityInfo> entitesInfo = new List<EntityInfo> ();

    private SerializedProperty initPro;

    protected override void OnDefaultEnable () {
        base.OnDefaultEnable ();
        dateSetManager = e as EntityManager;
        mEntityNames = serializedObject.FindProperty ("defineEntityNames");
        mEntityGoes = serializedObject.FindProperty ("defineEntityGos");

        Debug.Log(mEntityNames.arraySize);

        //获取工程中所有的实体类
        List<Type> list = GlobalTool.GetRuntimeTypes ();
        foreach (Type t in list) {
            if (t.IsSubclassOf (typeof (Entity))) {
                mEntites.Add (t);
            }
        }
        initPro = serializedObject.FindProperty ("isInit");
    }

    protected override void OnDefaultInspectorGUI () {
        base.OnDefaultInspectorGUI ();
        EditorGUILayout.HelpBox ("Entity Manager,Controller all EntityLogic", MessageType.Info);

        EditorGUILayout.PropertyField (initPro);
        GUILayout.BeginVertical ("Box");
        GUILayout.Label ("Entity:");

        EntityItem ();
        GUILayout.BeginHorizontal ();
        if (GUILayout.Button ("New", EditorStyles.miniButton)) {
            entitesInfo.Add (new EntityInfo { typeName = "<None>", entity = null });
            HasChanged ();
        }
        if (GUILayout.Button ("Clear", EditorStyles.miniButton)) {
            mEntityNames.ClearArray ();
            disableList.Clear ();
            entitesInfo.Clear ();
        }
        GUILayout.EndHorizontal ();

        GUILayout.EndVertical ();
    }

    void EntityItem () {
        for (int i = entitesInfo.Count - 1; i >= 0; i--) {
            GUILayout.BeginVertical ("Box");
            GUILayout.BeginHorizontal ();
            GUILayout.Label ("Type", GUILayout.Width (40));
            if (GUILayout.Button (entitesInfo[i].typeName, EditorStyles.miniPullDown)) {
                int curIndex = i;
                GenericMenu gm = new GenericMenu ();
                for (int m = 0; m < mEntites.Count; m++) {
                    GUIContent temp = new GUIContent ();
                    temp.text = mEntites[m].FullName;
                    if (disableList.Contains (m)) {
                        gm.AddDisabledItem (temp, false);
                    } else {
                        gm.AddItem (temp, " " == mEntites[m].FullName, (object obj) => {
                            int g = (int) obj;
                            disableList.Add (g);
                            EntityInfo tempInfo = entitesInfo[curIndex];
                            tempInfo.typeName = mEntites[g].Name;
                            entitesInfo[curIndex] = tempInfo;
                            mEntityNames.arraySize++;
                            int index = mEntityNames.arraySize - 1;
                            SerializedProperty pro = mEntityNames.GetArrayElementAtIndex (index);
                            pro.stringValue = mEntites[g].Name;
                        }, m);
                    }
                }
                gm.ShowAsContext ();
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button ("Delete", EditorStyles.miniButton, GUILayout.Width (40))) {
                entitesInfo.RemoveAt (i);
            }

            GUILayout.EndHorizontal ();
            GUILayout.Space (5);

            //Entity 项
            GUILayout.BeginHorizontal ();
            GUI.backgroundColor = Color.white;
            GUILayout.Label ("Enity", GUILayout.Width (40));
            EntityInfo info = entitesInfo[i];
            info.entity = (GameObject) EditorGUILayout.ObjectField (info.entity, typeof (GameObject), true);
            GUILayout.EndHorizontal ();

            GUILayout.EndVertical ();
        }

    }
}