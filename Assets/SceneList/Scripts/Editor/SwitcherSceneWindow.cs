using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;
public class SwitcherSceneWindow : EditorWindow
{

    [MenuItem("Tool/SwitcherSceneWindow")]
    private static void ShowWindow()
    {
        var window = GetWindow<SwitcherSceneWindow>(false, "SwitcherSceneWindow", true);
        window.Show();
    }
    [SerializeField]
    public class SceneListData
    {
        public List<string> guids = new List<string>();
        public bool closed = true;
        public bool loadAdditive = true;
        public bool sortRecentTop = false;
    }

    public SceneListData mSceneListData;
    private const string prefabsKey = "scenedata";
    private float margin = 5;

    private bool advanced = false;

    private Vector2 mScrollViewPos = Vector2.zero;

    private GUIStyle toolBar;
    private void OnEnable()
    {
        if (mSceneListData == null)
        {
            string jsonStr = PlayerPrefs.GetString(prefabsKey, string.Empty);
            if (jsonStr == string.Empty || jsonStr == null)
            {
                mSceneListData = new SceneListData();
            }
            else
            {
                mSceneListData = JsonUtility.FromJson<SceneListData>(PlayerPrefs.GetString(prefabsKey));
            }
        }

        toolBar = new GUIStyle(EditorStyles.toolbarButton);
        toolBar.alignment = TextAnchor.MiddleCenter;
    }

    private void OnDisable()
    {
        string jsonStr = JsonUtility.ToJson(mSceneListData);
        PlayerPrefs.SetString(prefabsKey, jsonStr);
    }
    private void OnGUI()
    {
        if (Application.isPlaying)
        {
            Scene scene = EditorSceneManager.GetActiveScene();
            GUILayout.Label("Scene Switching Disabled While Playing");
            GUILayout.Label(string.Format("active scene name:{0}", scene.name));
            GUILayout.Label(string.Format("total scene count:{0}", EditorSceneManager.loadedSceneCount));
            return;
        }

        Event e = Event.current;

        switch (e.type)
        {
            case EventType.DragUpdated:
                DragAndDrop.visualMode = IsVaildDrag(DragAndDrop.objectReferences) ? DragAndDropVisualMode.Move : DragAndDropVisualMode.Rejected;
                break;
            case EventType.DragPerform:
                AddScene(DragAndDrop.objectReferences);//添加场景完成在绘制gui
                return;
            case EventType.DragExited:
                break;
        }


        if (mSceneListData != null && mSceneListData.guids.Count > 0)
        {
            DrawSceneList();
        }
        else
        {
            Rect rect = GUILayoutUtility.GetRect(0, 0, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            rect.x += margin;
            rect.y += margin;
            rect.width -= 2 * margin;
            rect.height -= 2 * margin;

            GUI.Box(rect, "Drag Scene to here", new GUIStyle("GroupBox"));
        }
    }

    private void DrawSceneList()
    {
        mScrollViewPos = GUILayout.BeginScrollView(mScrollViewPos);
        foreach (string guid in mSceneListData.guids)
        {
            GUILayout.BeginVertical(new GUIStyle("GroupBox"));
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("↑", toolBar, GUILayout.MaxWidth(20)))
            {
                MoveUp(guid);
                return;
            }
            if (GUILayout.Button("↓", toolBar, GUILayout.MaxWidth(20)))
            {
                MoveDown(guid);
                return;
            }

            string path = AssetDatabase.GUIDToAssetPath(guid);
            string name = Path.GetFileNameWithoutExtension(path);
            if (GUILayout.Button(name, EditorStyles.toolbarButton))
            {
                SwitchToScene(guid);
                if (mSceneListData.sortRecentTop)
                {
                    MoveTop(guid);
                }
                return;
            }

            if (GUILayout.Button("X", GUILayout.MaxWidth(20)))
            {
                DeleteScene(guid);
                return;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        GUILayout.BeginHorizontal();

        advanced = GUILayout.Toggle(advanced, "Advanced");
        GUILayout.FlexibleSpace();
        if (advanced)
        {
            mSceneListData.sortRecentTop = GUILayout.Toggle(mSceneListData.sortRecentTop, "Recent Top");
            mSceneListData.loadAdditive = GUILayout.Toggle(mSceneListData.loadAdditive, "Single");
            mSceneListData.closed = GUILayout.Toggle(mSceneListData.closed, "Closed");
        }
        GUILayout.EndHorizontal();

        GUILayout.EndScrollView();
    }

    void DeleteScene(string guid)
    {
        mSceneListData.guids.Remove(guid);
        string path = AssetDatabase.GUIDToAssetPath(guid);
        Scene scene = SceneManager.GetSceneByPath(path);
        EditorSceneManager.CloseScene(scene, true);
        if (AssetDatabase.DeleteAsset(path))
        {
            Debug.Log("移除:" + scene.name);
        }
    }
    void SwitchToScene(string guid)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        Scene scene = EditorSceneManager.OpenScene(path, mSceneListData.loadAdditive ? OpenSceneMode.Additive : OpenSceneMode.Single);

        //Closed other scene
        if (!mSceneListData.closed)
            return;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene other = SceneManager.GetSceneAt(i);
            if (other != scene && other.isLoaded)
            {
                EditorSceneManager.CloseScene(other, true);
            }
        }


    }

    void MoveUp(string guid)
    {
        int index = mSceneListData.guids.IndexOf(guid);
        mSceneListData.guids.RemoveAt(index);
        if (index > 0)
        {
            index--;
        }
        mSceneListData.guids.Insert(index, guid); //需要关注一下索引号是否越界
    }
    void MoveDown(string guid)
    {
        int index = mSceneListData.guids.IndexOf(guid);
        mSceneListData.guids.RemoveAt(index);
        if (index < SceneManager.sceneCount)
        {
            index++;
        }
        mSceneListData.guids.Insert(index, guid); //需要关注一下索引号是否越界
    }

    void MoveTop(string guid)
    {
        mSceneListData.guids.Remove(guid);
        mSceneListData.guids.Insert(0, guid);
    }
    void AddScene(Object[] objs)
    {
        foreach (var obj in objs)
        {
            SceneAsset sceneAsset = obj as SceneAsset;
            if (sceneAsset == null) continue;

            string path = AssetDatabase.GetAssetPath(obj);
            string guid = AssetDatabase.AssetPathToGUID(path);

            if (!mSceneListData.guids.Contains(guid))
                mSceneListData.guids.Add(guid);
        }
    }

    bool IsVaildDrag(Object[] objs)
    {
        foreach (Object o in objs)
        {
            if (o.GetType() == typeof(SceneAsset))
            {
                return true;
            }
            else
                return false;
        }

        return false;
    }
}