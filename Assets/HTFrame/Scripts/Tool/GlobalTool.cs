using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GlobalTool : MonoBehaviour {
    private static readonly HashSet<string> RunTimeAssemblies = new HashSet<string> () {
        "Assembly-CSharp",
        "HTFramework.RunTime",
        "HTFramework.AI.RunTime",
        "HTFramework.Auxiliary.RunTime",
        "UnityEngine",
        "UnityEngine.CoreModule",
        "UnityEngine.UI",
        "UnityEngine.PhysicsModule"
    };

    public static Dictionary<string, Type[]> types = new Dictionary<string, Type[]> ();
    public static void GetRuntimeType () {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies ();
        foreach (Assembly assembly in assemblies) {
            string name = assembly.GetName ().Name;
            if (RunTimeAssemblies.Contains (name)) {
                Type[] ts = assembly.GetTypes ();
                if (!types.ContainsKey (name))
                    types[name] = ts;
            }
        }
    }

    public static List<Type> GetRuntimeTypes () {
        GetRuntimeType ();
        List<Type> list = new List<Type> ();
        foreach (Type[] type in types.Values) {
            list.AddRange (type);
        }
        return list;
    }

    public static Type GetRuntimeType (string typeName) {
        Type type = null;
        foreach (string name in types.Keys) {
            foreach (Type t in types[name]) {
                if (t.Name == typeName) {
                    type = t;
                    break;
                }
            }
        }
        return type;
    }
}