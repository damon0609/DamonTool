using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[GitHubURL("https://github.com/")]
[CSDNURL("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
[CustomEditor(typeof(ResourcesManager))]
public class ResourcesManagerInspector : HTBaseEditor<ResourcesManager>
{
    private string rootAssetBundlePath;
    private AssetBundleManifest manifest;
    private Dictionary<string, AssetBundle> assetBundles;
    protected override void OnDefaultEnable()
    {
        //初始化AssetBundle相关数据，运行时不可以更改的数据
        rootAssetBundlePath = (string)e.GetType().GetField("assetBundleRootPath", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(e);
        manifest = (AssetBundleManifest)e.GetType().GetField("assetBundleManifest", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(e);
        assetBundles = (Dictionary<string, AssetBundle>)e.GetType().GetField("assetBundles", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(e);
    }
    protected override void OnDefaultInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Coroutiner, Execution and destruction of unified scheduling Coroutine!", MessageType.Info);
        EditorGUILayout.EndHorizontal();

        e.resourcesMode = (ResourcesMode)EditorGUILayout.EnumPopup("Resources Mode", e.resourcesMode, GUILayout.Height(16));
        GUILayout.Space(2);
        if (e.resourcesMode == ResourcesMode.Resources)
        {
            if (GUILayout.Button("Resource Folder View"))
            {
                ResourcesWin win = EditorWindow.GetWindow<ResourcesWin>();
                win.titleContent = EditorGUIUtility.IconContent("ViewToolOrbit");
                win.titleContent.text = "Resources Win";
                win.Show();
            }
        }
        else if (e.resourcesMode == ResourcesMode.AssetBundle)
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Manifest Name:", GUILayout.Width(120));
            e.assetBundleManifestName = EditorGUILayout.TextField(e.assetBundleManifestName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            e.isEditorMode = EditorGUILayout.Toggle("IsEditorMode", e.isEditorMode);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            e.isCahce = EditorGUILayout.Toggle("IsCache", e.isCahce);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }

    protected override void OnInspectorRuntimeGUI()
    {
        if (e.resourcesMode == ResourcesMode.AssetBundle)
        {
            if (!e.isEditorMode)
            {
                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Root Path");
                EditorGUILayout.TextField(rootAssetBundlePath);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Manifest");
                EditorGUILayout.ObjectField(manifest, typeof(AssetBundleManifest), false);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("AssetBundle Count:"+assetBundles.Count);
                EditorGUILayout.EndHorizontal();

                foreach (string s in assetBundles.Keys)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(s);
                    EditorGUILayout.ObjectField(assetBundles[s], typeof(AssetBundle), false);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
            else
            {
                GUILayout.BeginHorizontal("Box");
                GUILayout.Label("No runtime data");
                GUILayout.EndHorizontal();
            }
        }
        else
        {
            GUILayout.BeginHorizontal("Box");
            GUILayout.Label("No runtime data");
            GUILayout.EndHorizontal();
        }
    }
}
