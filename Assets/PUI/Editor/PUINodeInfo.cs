using System.Collections;
using System.Collections.Generic;
using Damon.Tool;
using UnityEditor;
using UnityEngine;
namespace PUI {
    [CustomPropertyDrawer (typeof (NodeInfo))]
    public class PUINodeInfo : PropertyDrawer, ILog {
        private SerializedProperty namePro;
        private SerializedProperty posPro;
        private SerializedProperty sizePro;

        private bool on = true;

        private void GetProperty (SerializedProperty property) {
            namePro = property.FindPropertyRelative ("name");
            posPro = property.FindPropertyRelative ("pos");
            sizePro = property.FindPropertyRelative ("size");

        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            GetProperty (property);
            return 2;
        }
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

            EditorGUILayout.BeginHorizontal ();
            GUIStyle style = EditorStyles.foldoutHeader;
            style.margin = new RectOffset (20, 5, 2, 2);
            style.fixedWidth = position.width;
            style.fontStyle = FontStyle.Bold;
            on = EditorGUILayout.Foldout (on, "NodeInfo", style);
            EditorGUILayout.EndHorizontal ();

            if (!on) return;
            EditorGUILayout.Space ();
            EditorGUILayout.BeginVertical ();
            namePro.stringValue = EditorGUILayout.TextField ("name", namePro.stringValue);
            string name = namePro.stringValue;
            if (string.IsNullOrEmpty (name)) {
                EditorGUILayout.HelpBox ("必须对节点名称进行赋值", MessageType.Error);
            } else if (name == "nodeName") {
                EditorGUILayout.HelpBox ("节点名称不能使用默认值", MessageType.Warning);
            }
            posPro.vector3Value = EditorGUILayout.Vector3Field ("pos", posPro.vector3Value);
            sizePro.vector2Value = EditorGUILayout.Vector2Field ("size", sizePro.vector2Value);
            EditorGUILayout.EndVertical ();
        }
    }
}