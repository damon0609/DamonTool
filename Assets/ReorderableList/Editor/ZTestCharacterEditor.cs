using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEditorInternal;
[CustomEditor(typeof(ZTestCharacter))]
public class ZTestCharacterEditor : Editor
{
    private ReorderableList reorderableList;
    private SerializedProperty uriPro;
    private void OnEnable()
    {
        uriPro = serializedObject.FindProperty("url");
        SerializedProperty pro = serializedObject.FindProperty("characters");
        reorderableList = new ReorderableList(serializedObject, pro, true, true, true, true);
        reorderableList.headerHeight = EditorGUIUtility.singleLineHeight;
        reorderableList.elementHeight = 65;
        reorderableList.drawHeaderCallback += (r) =>
        {
            EditorGUI.LabelField(r, pro.displayName);
        };
        reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
        {
            SerializedProperty element = pro.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUILayout.Space(5);
        if (reorderableList != null)
            reorderableList.DoLayoutList();

        EditorGUILayout.PropertyField(uriPro);
        serializedObject.ApplyModifiedProperties();
    }
}
