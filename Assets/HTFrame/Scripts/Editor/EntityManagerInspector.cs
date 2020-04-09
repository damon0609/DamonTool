using HT;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (EntityManager))]
[GitHubURL ("https://github.com/")]
[CSDNURL ("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
public class EntityManagerInspector : HTBaseEditor<EntityManager> {
    private EntityManager dateSetManager;

    protected override void OnDefaultEnable () {
        base.OnDefaultEnable ();
        dateSetManager = e as EntityManager;
    }

    protected override void OnDefaultInspectorGUI () {
        base.OnDefaultInspectorGUI ();
    }
}