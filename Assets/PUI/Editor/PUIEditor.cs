using System.Collections;
using System.Collections.Generic;
using Damon.EditorTool;
using Damon.Tool;
using UnityEditor;
using UnityEngine;
namespace PUI {
    public abstract class PUIEditor<T> : Editor where T : PUINode {

        protected T t;

        protected RectTransform mRectTransform;
        private SerializedProperty nodeInfoPro;
        private void OnEnable () {
            t = target as T;
            mRectTransform = t.gameObject.GetComponent<RectTransform> ();
            nodeInfoPro = serializedObject.FindProperty ("nodeInfo");
            OnDefaultEnable ();
        }

        protected virtual void OnDefaultEnable () {

        }
        public override void OnInspectorGUI () {
            //EditorGUILayout.BeginHorizontal();
            // t.id = EditorGUILayout.TextField ("NodeName", t.id);
            // if (string.IsNullOrEmpty (t.id)) {
            //     EditorGUILayout.HelpBox ("请对节点进行赋值", MessageType.Warning);
            // } else if (t.id == "node") {
            //     EditorGUILayout.HelpBox ("请不要使用默认名称做节点名称", MessageType.Warning);
            // }
            EditorGUILayout.PropertyField(nodeInfoPro);
            //EditorGUILayout.EndHorizontal();
            OnDefaultOnGUI ();
            if (GUI.changed) {
                EditorTool.HasChanged (this);
            }

            serializedObject.ApplyModifiedProperties ();
        }
        protected virtual void OnDefaultOnGUI () {

        }
    }
}