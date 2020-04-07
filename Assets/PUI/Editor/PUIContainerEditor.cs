using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace PUI {
    [CustomEditor (typeof (PUIContainer))]
    public class PUIContainerEditor : PUIEditor<PUIContainer> {

        private ReorderableList nodes;
        private SerializedProperty listPro;
        GUIStyle style;
        private bool on = true;
        protected override void OnDefaultEnable () {
            style = EditorStyles.foldout;
            listPro = serializedObject.FindProperty ("nodes");
            nodes = new ReorderableList (serializedObject, listPro);
        }
        protected override void OnDefaultOnGUI () {
            if (nodes != null) {
                style = EditorStyles.foldout;
                nodes.drawHeaderCallback += (Rect r) => {
                    style.fixedWidth = r.width;
                    style.fontStyle = FontStyle.Bold;
                    on = EditorGUI.Foldout (r, on, "Nodes List", style);
                };
                nodes.drawElementCallback += delegate(Rect rect, int index, bool isActive, bool isFocused)
                {

                };
                nodes.DoLayoutList ();
            }
        }
    }
}