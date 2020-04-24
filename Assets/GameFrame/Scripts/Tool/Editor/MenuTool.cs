using System.Collections;
using System.Collections.Generic;
using System.IO;
using Damon.EditorTool;
using UnityEditor;
using UnityEngine;
public class MenuTool {

    [MenuItem("Tool/Remove Empty Folder")]
    public static void RemoveEmptyFolder()
    {
        EditorTool.RemoveEmptyFolder();
    }

    [MenuItem ("Tool/BuildAssetBundle/Clear Unused Label")]
    public static void ClearAssetBundleLabel () {
        string[] strs = AssetDatabase.GetUnusedAssetBundleNames ();
        foreach (string s in strs) {
            AssetDatabase.RemoveAssetBundleName (s, true);
            Debug.Log (s);
        }
    }

    [MenuItem ("Tool/BuildAssetBundle/BuildAssetBundle Folder")]
    static void BuildAssetFolder () {
        string outPathAssetBundle = Application.streamingAssetsPath;
        Object[] objs = Selection.GetFiltered<Object> (SelectionMode.DeepAssets);
        if (objs.Length > 0 && null != objs) {
            List<string> paths = new List<string> ();
            foreach (Object obj in objs) {
                string path = AssetDatabase.GetAssetPath (obj);
                string extension = path.Substring (path.LastIndexOf ('.') + 1);
                if (extension == "cs" || extension == "unity")
                    continue;
                paths.Add (path);
            }
            BuildAssetBundle (outPathAssetBundle, paths.ToArray (), false);
        }
    }

    private static void BuildAssetBundle (string outPath, string[] objPaths, bool separate = true) {
        if (!Directory.Exists (outPath))
            Directory.CreateDirectory (outPath);

        if (separate) {
            foreach (string path in objPaths) {
                if (path.IndexOf ('.') != -1) {
                    string name = path.Substring (path.LastIndexOf ('/') + 1);
                    name = name.Remove (name.LastIndexOf ('.'));
                    AssetImporter assetImporter = AssetImporter.GetAtPath (path);
                    assetImporter.assetBundleName = name;
                    assetImporter.assetBundleVariant = ".assetbundle";
                }
            }
            AssetBundleManifest bundleManifest = BuildPipeline.BuildAssetBundles (outPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
        } else {
            List<AssetBundleBuild> builds = new List<AssetBundleBuild> ();
            AssetBundleBuild assetBundleBuild = new AssetBundleBuild ();
            List<string> list = new List<string> ();
            foreach (string path in objPaths) {
                if (path.IndexOf ('.') != -1)
                    list.Add (path);
            }
            assetBundleBuild.assetNames = list.ToArray ();
            assetBundleBuild.assetBundleName = "GameRes";
            assetBundleBuild.assetBundleVariant = ".assetbundle";
            builds.Add (assetBundleBuild);
            BuildPipeline.BuildAssetBundles (outPath, builds.ToArray (), BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
        }
        AssetDatabase.Refresh ();

    }
}