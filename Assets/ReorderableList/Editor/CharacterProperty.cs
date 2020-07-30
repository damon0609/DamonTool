using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CustomPropertyDrawer(typeof(Character))]
public class CharacterProperty : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //创建属性包装器
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            position.height = EditorGUIUtility.singleLineHeight;
            Rect iconRect = new Rect(position)
            {
                width = 60,
                height = 60,
            };
            SerializedProperty icon = property.FindPropertyRelative("icon");
            icon.objectReferenceValue = EditorGUI.ObjectField(iconRect, icon.objectReferenceValue, typeof(Texture2D), false);


            Rect nameLabelRect = new Rect(position)
            {
                x = iconRect.x + iconRect.width + 5,
                width = 100,
                height = EditorGUIUtility.singleLineHeight,
            };
            SerializedProperty name = property.FindPropertyRelative("name");
            EditorGUI.LabelField(nameLabelRect, name.displayName);

            Rect nameRect = new Rect(position)
            {
                x = iconRect.width + nameLabelRect.width,
                width = position.width - (iconRect.width + nameLabelRect.width),
                height = EditorGUIUtility.singleLineHeight,
            };
            name.stringValue = EditorGUI.TextField(nameRect, name.stringValue);

            Rect prefaLabelRect = new Rect(position)
            {
                x = iconRect.x + iconRect.width + 5,
                y = nameRect.y + nameRect.height + 5,
                width = 100,
            };
            SerializedProperty prefab = property.FindPropertyRelative("perfab");
            EditorGUI.LabelField(prefaLabelRect, prefab.displayName);

            Rect prefabRect = new Rect(position)
            {
                x = iconRect.width + prefaLabelRect.width,
                y = nameRect.y + nameRect.height + 5,
                width = position.width - iconRect.width - prefaLabelRect.width,
                height = EditorGUIUtility.singleLineHeight,
            };
            prefab.objectReferenceValue = EditorGUI.ObjectField(prefabRect, prefab.objectReferenceValue, typeof(GameObject), false);


            Rect weaponLabelRect = new Rect(position)
            {
                x = iconRect.x + iconRect.width + 5,
                y = prefabRect.y + prefabRect.height + 5,
                width = 100,
                height = EditorGUIUtility.singleLineHeight,
            };
            SerializedProperty weapon = property.FindPropertyRelative("weapon");
            EditorGUI.LabelField(weaponLabelRect, weapon.displayName);

            Rect weaponRect = new Rect(position)
            {
                x = iconRect.width + weaponLabelRect.width,
                y = prefabRect.y + prefabRect.height + 5,
                width = position.width - (prefaLabelRect.width + iconRect.width),
                height = EditorGUIUtility.singleLineHeight,
            };
            weapon.objectReferenceValue = EditorGUI.ObjectField(weaponRect, weapon.objectReferenceValue, typeof(GameObject), false);
        }
    }
}


[CustomPropertyDrawer(typeof(URLInfoAttribute))]
public class URLInfoPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        URLInfoAttribute att = attribute as URLInfoAttribute;
        if(!string.IsNullOrEmpty(att.uri)&&!string.IsNullOrEmpty(att.iconPath))
        {
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(att.iconPath);
            GUIContent g1 = new GUIContent();
            g1.image = tex;
            g1.tooltip = att.uri;

            if(GUILayout.Button(g1,GUILayout.Width(24),GUILayout.Height(24)))
                Application.OpenURL(att.uri);
        }
    }
}



//绘制被修饰的属性或者字段的特性类的显示效果

[CustomPropertyDrawer(typeof(RangeAttribute))]
public class RangePropertyDrawer : PropertyDrawer
{
    //property 这里是只被修饰的属性或者字段
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RangeAttribute rangeAttribute = attribute as RangeAttribute;
        if (property.propertyType == SerializedPropertyType.Float)
        {

        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {

        }
    }
}

//修饰属性或者字段的特性类
public class RangeAttribute : PropertyAttribute
{
    public float max;
    public float min;

    public RangeAttribute(float min, float max)
    {
        this.max = max;
        this.min = min;
    }
}