using System.Linq;
using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace Damon.EditorTool {

    public class EditorTool {
        public static void RemoveEmptyFolder () {
            string[] directores = Directory.GetDirectories ("Assets", "*", SearchOption.AllDirectories); //*是通配符
            foreach (string d in directores) {
                if (!Directory.Exists (d))
                    continue;
                if(Directory.GetFiles(d,"*",SearchOption.AllDirectories).Count(p=>Path.GetExtension(p)!="meta")>0)
                    continue;

                try
                {
                    Directory.Delete(d,true);
                }catch(Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
            AssetDatabase.Refresh();
        }
        public static void HasChanged (Editor e) {

            if (!Application.isPlaying) {
                EditorUtility.SetDirty (e.target);
                Component com = e.target as Component;
                if (com != null && com.gameObject.scene != null) {
                    EditorSceneManager.MarkSceneDirty (com.gameObject.scene);
                }
            }
        }
        public class EditorGUILayoutTool {
            public float FloatField (string label, float value) {
                return EditorGUILayout.FloatField (label, value);
            }
        }
    }

}