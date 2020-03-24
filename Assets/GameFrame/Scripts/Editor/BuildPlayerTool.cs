using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using UnityEditor.Build.Reporting;

public class BuildPlayerTool : EditorWindow
{
    private Vector2 pos;
    private BuildTarget buildTargetGroup = BuildTarget.Android;

    private string productName;
    private string apkName;
    private string outPath = string.Empty;
    private List<string> scenes = new List<string>();
    private Rect viewRect = new Rect();
    private float scrollViewHeight;

    private void OnEnable()
    {

    }

    void OnGUILayout()
    {
        Rect rect = position;
        rect.height = position.height - 200;
        AddScene(rect);
        EditorGUILayout.BeginScrollView(pos, GUILayout.MaxHeight(rect.height));
        foreach (string path in scenes)
        {
            EditorGUILayout.LabelField(path);
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        productName = EditorGUILayout.TextField("Product name", productName);
        apkName = EditorGUILayout.TextField("APK name", apkName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        outPath = EditorGUILayout.TextField("Out Path", outPath);
        if (GUILayout.Button("打开", GUILayout.MaxWidth(120)))
        {
            outPath = EditorUtility.OpenFolderPanel("存储为", "", "");
            UnityEngine.Debug.Log(outPath);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        buildTargetGroup = (BuildTarget)EditorGUILayout.EnumPopup("Build Target", buildTargetGroup);


        EditorGUILayout.Space();
        if (GUILayout.Button("Build"))
        {
            Build(false);
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Build And Install"))
        {
            Build(true);
        }
    }

    private void OnGUI()
    {
        OnGUILayout();
        //OnDrawEditorGUI();
    }

    void OnDrawEditorGUI()
    {
        GUI.Label(new Rect(10, 2, position.width, 15), "Build Scenes");

        Rect rect = position;
        rect.x = 10;
        rect.y = 10 + 10;
        rect.width = position.width - 20;
        rect.height = 200;
        GUI.Box(rect, "");

        viewRect = rect;
        scrollViewHeight = scenes.Count * EditorGUIUtility.singleLineHeight;
        if (scrollViewHeight >= rect.height)
            viewRect.height = scrollViewHeight;
        pos = GUI.BeginScrollView(rect, pos, viewRect);
        for (int i = 0; i < scenes.Count; i++)
        {
            if (i != 0)
                rect.y += 15;
            EditorGUI.LabelField(rect, scenes[i]);
        }
        GUI.EndScrollView();
        OnDrawSetting();
        AddScene(rect);
    }

    void AddScene(Rect rect)
    {
        Event e = Event.current;
        if (e.type == EventType.DragExited)
        {
            Vector2 temp = new Vector2(position.x, position.y);
            if (rect.Contains(e.mousePosition + temp))
            {
                string[] paths = DragAndDrop.paths;
                if (paths != null && paths.Length > 0)
                {
                    foreach (string path in paths)
                    {
                        string extension = path.Substring(path.LastIndexOf('.') + 1);
                        if (extension != "unity")
                            continue;
                        UnityEngine.Debug.Log("---" + temp + "---" + path);

                        lock (scenes)
                        {
                            if (!scenes.Contains(path))
                                scenes.Add(path);
                        }

                    }
                }
            }
        }
    }

    void OnDrawSetting()
    {
        Rect rect = position;
        rect.height = EditorGUIUtility.singleLineHeight;

        rect.x = 10;
        rect.y = 225;
        rect.width = position.width - 300;
        EditorGUI.PrefixLabel(rect, new GUIContent("Build Target"));

        rect.x = position.width - 310;
        rect.width = position.width - 260;
        buildTargetGroup = (BuildTarget)EditorGUI.EnumPopup(rect, buildTargetGroup);

        rect.x = 10;
        rect.y = 245;
        rect.width = position.width - 300;
        EditorGUI.LabelField(rect, new GUIContent("Apk Name"));

        rect.x = rect.width - 10;
        rect.y = 245;
        rect.width = position.width - 300;
        apkName = EditorGUI.TextField(rect, apkName);

        rect.x = rect.width + 100;
        EditorGUI.LabelField(rect, new GUIContent("Product Name"));


        rect.x = rect.width + 190;
        productName = EditorGUI.TextField(rect, productName);

        rect.x = 10;
        rect.y = 265;
        EditorGUI.LabelField(rect, new GUIContent("Out Path"));


        rect.x = position.width - 310;
        rect.width = position.width - 180;
        outPath = EditorGUI.TextField(rect, outPath);

        rect.x = rect.width + 100;
        rect.width = position.width - 330;
        if (GUI.Button(rect, "打开"))
        {
            outPath = EditorUtility.OpenFolderPanel("存储为", "", "");
        }

        rect.x = 10;
        rect.y = 285;
        rect.width = position.width - 20;
        if (GUI.Button(rect, "Build"))
            Build(false);

        rect.x = 10;
        rect.y = 305;
        rect.width = position.width - 20;
        if (GUI.Button(rect, "Build And Install"))
            Build(true);
    }

    void Build(bool isInstall)
    {
        if (productName == string.Empty || apkName == string.Empty)
        {
            EditorUtility.DisplayDialog("Warning", "product name or apk name is null", "知道了");
            return;
        }

        PlayerSettings.productName = productName;
        PlayerSettings.companyName = "Pico";
        string identify = "com.Pico." + productName;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, identify);
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.options = BuildOptions.None;
        buildPlayerOptions.scenes = scenes.ToArray();
        buildPlayerOptions.locationPathName = outPath + "/" + apkName + ".apk";
        buildPlayerOptions.target = BuildTarget.Android;

        BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = buildReport.summary;
        if (summary.result == BuildResult.Succeeded)
        {
            UnityEngine.Debug.Log("APK大小" + summary.totalSize + "--花费时间:" + summary.totalTime);
            if (isInstall)
                Install();
        }

        if (summary.result == BuildResult.Failed)
        {
            UnityEngine.Debug.Log("打包失败:" + summary.totalErrors);
        }
    }

    void Install()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();

        string command = "/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
        startInfo.FileName = command;
        startInfo.Arguments = "adb devices";
        startInfo.CreateNoWindow = true;
        ;        //startInfo.Arguments = "adb install -r "+outPath;
        Process process = Process.Start(startInfo);

    }

    [MenuItem("Tool/Build")]
    static void BuildAPK()
    {
        BuildPlayerTool win = EditorWindow.GetWindow<BuildPlayerTool>();
        win.titleContent = new GUIContent("Build Setting");
        win.Show();
    }
}
