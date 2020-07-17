
using System;
using UnityEditor;
using UnityEngine;
public class EditorUtils
{
    public static class Styles
    {
        public const string Box = "Box";

    }
    [MenuItem("HT/Procedure")]
    static void CreateProcedure()
    {
        ProcedureWin win = EditorWindow.GetWindow<ProcedureWin>();
        win.titleContent = new GUIContent("Procedure"); 
        win.Show();
    }
}
