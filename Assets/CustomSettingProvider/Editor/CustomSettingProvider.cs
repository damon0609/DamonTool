using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomSettingProvider : ScriptableObject {

    [SerializeField]
    private int mNum;
    [SerializeField]
    private string mStr;

    internal static CustomSettingProvider GetOrCreateProvider () {

        string path = "Assets/CustomSettingProvider/CustomSettingProvider.asset";
        CustomSettingProvider mCustomerSettingProvider = (CustomSettingProvider) AssetDatabase.LoadAssetAtPath (path, typeof (CustomSettingProvider));
        if (mCustomerSettingProvider == null) {
            mCustomerSettingProvider = ScriptableObject.CreateInstance<CustomSettingProvider> ();
            mCustomerSettingProvider.mNum = 40;
            mCustomerSettingProvider.mStr = "test";

            AssetDatabase.CreateAsset (mCustomerSettingProvider, path);
            AssetDatabase.SaveAssets ();
        }
        return mCustomerSettingProvider;
    }

    internal static SerializedObject GetSerializedSetting () {
        return new SerializedObject (GetOrCreateProvider ());
    }
}

internal static class RegisterSettingProvider {

    [SettingsProvider]
    public static SettingsProvider CreateCumstomSettingProvider () {

        SettingsProvider settingsProvider = new SettingsProvider("Project/CustomSettingProvider",SettingsScope.Project)
        {
            label = "Custom Setting",
            guiHandler=(string str)=>{
                SerializedObject custom = CustomSettingProvider.GetSerializedSetting();
                if(custom!=null)
                {
                    EditorGUILayout.PropertyField(custom.FindProperty("mNum"),new GUIContent("Num"));
                    EditorGUILayout.PropertyField(custom.FindProperty("mStr"),new GUIContent("Str"));
                }
            },
            keywords = new HashSet<string>(new[] { "Number", "Some String" })
        };
        return settingsProvider;

    }
}