using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
public class CorePreference {
    private const string mKeyIp = "ip";
    private const string mKeyShowIp = "on";
    private const string mkeyUnityVersion = "unityVersion";
    private static string mUnityVersion = string.Empty;
    public static string unityVersion {
        get { return EditorPrefs.GetString (mkeyUnityVersion, mUnityVersion); }
        set {
            if (mUnityVersion != value) {
                mUnityVersion = value;
                EditorPrefs.SetString (mkeyUnityVersion, unityVersion);
            }
        }
    }
    private static string mIp = "";
    public static string ip {
        get {
            if (mIp == string.Empty) {
                IPAddress[] entry = Dns.GetHostAddresses (Dns.GetHostName ());
                foreach (var i in entry) {
                    if (i.AddressFamily == AddressFamily.InterNetwork)
                        mIp = i.ToString ();
                }
            }
            return EditorPrefs.GetString (mKeyIp, mIp);
        }
        set {
            if (mIp != value) {
                mIp = value;
                EditorPrefs.SetString (mKeyIp, mIp);
            };
        }
    }
    private static bool mIsShow = true;
    public static bool isShow {
        get { return EditorPrefs.GetBool (mKeyShowIp, mIsShow); }
        set {
            if (mIsShow != value) {
                mIsShow = value;
                EditorPrefs.SetBool (mKeyShowIp, mIsShow);
            }
        }
    }

    private static string mCacheDataPath = "Asstes/";
    public static string cachePath {
        get { return EditorPrefs.GetString ("cacheDataPath", mCacheDataPath); }
        set {
            if (mCacheDataPath != value) {
                mCacheDataPath = value;
            }
            EditorPrefs.SetString ("cacheDataPath", mCacheDataPath);
        }
    }

#if UNITY_EDITOR
    [LeapPreference ("UserPreference", 0)]
    private static void OnDrawPreference () {
        EditorGUILayout.BeginVertical ();
        GUIContent c2 = new GUIContent ();
        c2.text = "Unity Version";
        c2.tooltip = "unity版本号";
        EditorGUILayout.LabelField (c2, new GUIContent { text = Application.unityVersion }, EditorStyles.boldLabel);

        isShow = EditorGUILayout.Toggle (new GUIContent { text = "IsShow", tooltip = "是否显示本机ip地址" }, isShow);
        if (isShow) {
            GUIContent c1 = new GUIContent ();
            c1.text = "IP";
            c1.tooltip = "本机ip地址";

            EditorGUILayout.LabelField (c1, new GUIContent { text = ip }, EditorStyles.boldLabel);
        }

        EditorGUILayout.EndVertical ();
    }

    [LeapPreference ("DataPath", 1)]
    private static void OnDrawDataSetting () {
        EditorGUILayout.BeginVertical ();
        GUIContent c2 = new GUIContent ();
        c2.text = "Data Path";
        c2.tooltip = "数据存放路径";

        EditorGUILayout.BeginHorizontal ();
        cachePath = EditorGUILayout.TextField (c2, cachePath);
        if (GUILayout.Button ("路径设置",GUILayout.Width(80))) {
            cachePath = EditorUtility.OpenFolderPanel ("Cacha Data Path", "", "");
        }
        EditorGUILayout.EndHorizontal ();
        EditorGUILayout.EndVertical ();
    }
#endif
}