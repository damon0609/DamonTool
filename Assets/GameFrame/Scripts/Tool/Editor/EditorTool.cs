using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
namespace Damon.EditorTool {
    public class EditorTool {

        public static void HasChanged (Editor e) {

            if (!Application.isPlaying) {
                EditorUtility.SetDirty (e.target);
                Component com = e.target as Component;
                if(com!=null&&com.gameObject.scene!=null)
                {
                    EditorSceneManager.MarkSceneDirty(com.gameObject.scene);
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