using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
[AttributeUsage (AttributeTargets.Method, AllowMultiple = false)]
public class LeapPreferenceAttribute : Attribute {
    public string header;
    public int order;

    public LeapPreferenceAttribute (string header, int order) {
        this.header = header;
        this.order = order;
    }
    private struct LeapPreferenceItem {
        public Action onDrawPreferenceItem;
        public LeapPreferenceAttribute attribute;
    }
    private static List<LeapPreferenceItem> mLeapPreferencs;

#if UNITY_EDITOR
    internal static void GetPrefercenItem () {
        if (mLeapPreferencs != null)
            return;

        mLeapPreferencs = new List<LeapPreferenceItem> ();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies ();
        foreach (Type t in assemblies.SelectMany (t => t.GetTypes ())) {
            foreach (MethodInfo m in t.GetMethods (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)) {
                var tt = m.GetCustomAttributes (typeof (LeapPreferenceAttribute), true);
                if (tt.Length == 0) {
                    continue;
                }
                var attribute = tt[0] as LeapPreferenceAttribute;
                LeapPreferenceItem item = new LeapPreferenceItem ();
                item.attribute = attribute;
                item.onDrawPreferenceItem = () => {
                    EditorGUILayout.LabelField (attribute.header, EditorStyles.boldLabel);
                    using (new EditorGUILayout.VerticalScope (EditorStyles.helpBox)) {
                        m.Invoke (null, null);
                    }
                };
                mLeapPreferencs.Add (item);
            }
        }
        mLeapPreferencs.Sort ((a, b) => { return a.attribute.order.CompareTo (b.attribute.order); });
    }
#endif

    internal class LeapPreferenceSetting : SettingsProvider {
        public LeapPreferenceSetting (string path, SettingsScope scope) : base (path, scope) { }
        public override void OnGUI (string searchContext) {
            GetPrefercenItem ();
            foreach (var item in mLeapPreferencs) {
                item.onDrawPreferenceItem ();
            }
        }
    }

    [SettingsProvider]
    internal static SettingsProvider GetLeapPreferenceSetting () {
        return new LeapPreferenceSetting ("Preferences/Leap Motion", SettingsScope.User);
    }
}