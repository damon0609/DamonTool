using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
public class ResourcesWin : HTBaseWindow
{

    public sealed class ResourcesInfo
    {
        private List<string> extensions = new List<string> { ".png", ".jpg", ".csv", ".xml", ".asset", ".shader" };
        public string path;
        public void Add(Object obj)
        {
            if (objects != null)
            {
                objects.Add(obj);
            }
        }
        public Object GetObject(int index)
        {
            if (index <= objCount - 1)
            {
                return objects[index];
            }
            else
            {
                Debug.LogError("index is between 0 to" + objCount);
                return null;
            }
        }
        private List<Object> objects;
        public bool on = false;

        public Vector2 pos;
        public int objCount
        {
            get { return objects.Count; }
        }

        public ResourcesInfo(string path)
        {
            this.path = path;
            objects = new List<Object>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            GetObject(directoryInfo);
        }

        void GetObject(DirectoryInfo parent)
        {
            FileSystemInfo[] infos = parent.GetFileSystemInfos();
            foreach (FileSystemInfo info in infos)
            {
                if (info is FileInfo)
                {
                    string path = info.FullName;
                    if (extensions.Contains(Path.GetExtension(path)))
                    {
                        path = path.Replace("\\", "/").Replace(Application.dataPath, "Assets");
                        Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
                        if (obj != null)
                            objects.Add(obj);
                    }
                }
                else if (info is DirectoryInfo)
                {
                    GetObject(info as DirectoryInfo);//这里Resources里面还有文件夹，只是把文件过滤出来进行罗列
                }
            }
        }
    }
    private List<ResourcesInfo> list = new List<ResourcesInfo>();
    void GetResourcesFolder(string path)
    {
        DirectoryInfo info = new DirectoryInfo(path);
        if (info.Exists)
        {
            DirectoryInfo[] dirs = info.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                if (dir.Name == "Resources")
                {
                    dir.GetFileSystemInfos();
                    list.Add(new ResourcesInfo(dir.FullName));
                }
                else
                {
                    GetResourcesFolder(dir.FullName);
                }
            }
        }
    }
    protected override void Initialize()
    {
        GetResourcesFolder(Application.dataPath);
    }
    protected override void OnBodyGUI()
    {
        foreach (ResourcesInfo item in list)
        {
            EditorGUILayout.BeginVertical("GroupBox");

            EditorGUILayout.BeginHorizontal();
            GUIContent g = new GUIContent();
            g.text = item.path + "(" + item.objCount + ")";
            item.on = EditorGUILayout.Foldout(item.on, g,true);
            EditorGUILayout.EndHorizontal();
            if (item.on)
            {
                Expand(item);
            }
            EditorGUILayout.EndVertical();
        }
    }

    void Expand(ResourcesInfo info)
    {
        info.pos = EditorGUILayout.BeginScrollView(info.pos);
        for (int i = 0; i < info.objCount; i++)
        {
            Object obj = info.GetObject(i);
            GUIContent g = EditorGUIUtility.ObjectContent(obj, typeof(Object));
            g.text = AssetDatabase.GetAssetPath(obj);
            if (GUILayout.Button(g, "PR PrefabLabel", GUILayout.Height(20)))
            {
                EditorGUIUtility.PingObject(obj);
                Selection.activeObject = obj;
            }
        }
        EditorGUILayout.EndScrollView();
    }
}
