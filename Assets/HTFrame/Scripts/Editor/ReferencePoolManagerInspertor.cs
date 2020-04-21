using HT;
using UnityEditor;
using UnityEngine;

[GitHubURL ("https://github.com/")]
[CSDNURL ("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
[CustomEditor (typeof (ReferencePoolManager))]
public class ReferencePoolManagerEditor : HTBaseEditor<ReferencePoolManager> {
    private ReferencePoolManager referencePool;

    protected override void OnDefaultEnable () {
        base.OnDefaultEnable ();
        referencePool = e as ReferencePoolManager;
    }

    protected override void OnDefaultInspectorGUI () {
        base.OnDefaultInspectorGUI ();
    }

}