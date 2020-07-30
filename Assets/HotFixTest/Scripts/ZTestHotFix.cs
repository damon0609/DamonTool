using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.IO;
using SystemObject = System.Object;
using System;

public class ZTestHotFix : MonoBehaviour
{
    void Start()
    {

        string path = Application.dataPath + @"\Resources\HotFix.dll";
        path = path.Replace("/", "\\");
        if (File.Exists(path))
        {
            Assembly assembly = Assembly.LoadFile(path);
            if (assembly == null)
            {
                Debug.LogError("load assembly failed");
                return;
            }
            else
            {
                var types = assembly.GetTypes();
                foreach (var t in types)
                {
                    if (t.Name == "HotFixEnvironment")
                    {
                        SystemObject obj = Activator.CreateInstance(t,false);
                        if (obj == null)
                            Debug.LogError("Hot Fix inintize failed");
                        else
                        {
                            MethodInfo method = t.GetMethod("Init", BindingFlags.Instance | BindingFlags.Public);
                            if (method != null)
                                method.Invoke(obj,null);
                        }
                    }
                }
            }
        }
    }

    void Update()
    {

    }
}
