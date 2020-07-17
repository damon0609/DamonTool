using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Damon;
using System.Reflection;
[GitHubURL("https://github.com/")]
[CSDNURL("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
[CustomEditor(typeof(NetWorkManager))]
public class NetWorkManagerInspector : HTBaseEditor<NetWorkManager>
{
    private string deleteStr = "";
    private GUIStyle style;
    protected override void OnDefaultEnable()
    {
        base.OnDefaultEnable();
        style = new GUIStyle(EditorStyles.label);
        style.normal.textColor = Color.red;

        List<Type> list = GlobalTool.GetRuntimeTypes<Type>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly b in assemblies)
        {
            Type[] types = b.GetTypes();
            foreach (var t in types)
            {
                if (t.IsSubclassOf(typeof(BaseProtocolChannel)))
                    e.mProtocolTypes.Add(t.FullName);
            }
        }
    }
    void GenericMenus()
    {
        GenericMenu menu = new GenericMenu();
        for (int i = 0; i < e.mProtocolTypes.Count; i++)
        {
            menu.AddItem(new GUIContent(e.mProtocolTypes[i]), false, (object obj) =>
            {
                string str = obj as string;
                if (str == "TCPProtocolChannel")
                {
                    TCPProtocolChannel tt = Activator.CreateInstance<TCPProtocolChannel>();
                    e.protocols.Add(str, tt);
                }
                else if (str == "UDPProtocolChannel")
                {
                    UDPProtocolChannel ut = Activator.CreateInstance<UDPProtocolChannel>();
                    e.protocols.Add(str, ut);
                }
                Repaint();
            }, e.mProtocolTypes[i]);

            if (e.protocols.ContainsKey(e.mProtocolTypes[i]))
                menu.AddDisabledItem(new GUIContent(e.mProtocolTypes[i]));

        }
        menu.ShowAsContext();
    }
    protected override void OnDefaultInspectorGUI()
    {
        base.OnDefaultInspectorGUI();

        EditorGUILayout.HelpBox("network manager", MessageType.Info);

        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.BeginHorizontal();
        e.ip = EditorGUILayout.TextField(new GUIContent("server ip"), e.ip);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        e.serverPort = EditorGUILayout.IntField(new GUIContent("server port"), e.serverPort);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        e.clientIP = EditorGUILayout.TextField(new GUIContent("client ip"), e.clientIP);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        e.clentPort = EditorGUILayout.IntField(new GUIContent("client port"), e.clentPort);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");


        EditorGUILayout.BeginVertical();
        foreach (string str in e.protocols.Keys)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("[网络通道:" + str + ",连接状态:" + e.protocols[str].state + "]"), style);
            if (GUILayout.Button("delete", GUILayout.Width(60)))
            {
                deleteStr = str;
                Repaint();
            }
            EditorGUILayout.EndHorizontal();
        }

        if (e.protocols.ContainsKey(deleteStr))
        {
            e.protocols[deleteStr] = null;
            e.protocols.Remove(deleteStr);
            deleteStr = string.Empty;
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.LabelField("Add Channel");
        if (GUILayout.Button(new GUIContent("Add Channel"), EditorStyles.miniPullDown))
        {
            GenericMenus();
        }
        EditorGUILayout.EndVertical();
    }
}
