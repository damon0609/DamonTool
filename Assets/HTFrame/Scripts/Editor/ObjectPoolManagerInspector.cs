using HT;
using UnityEditor;
using UnityEngine;

namespace HT
{
    [GitHubURL("https://github.com/")]
    [CSDNURL("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
    [CustomEditor(typeof(ObjectPoolManager))]
    public class ObjectPoolManagerInspector : HTBaseEditor<ObjectPoolManager>
    {
        private ObjectPoolManager objectPool;

        protected override void OnDefaultEnable()
        {
            base.OnDefaultEnable();
            objectPool = e as ObjectPoolManager;
        }

        protected override void OnDefaultInspectorGUI()
        {
            base.OnDefaultInspectorGUI();
        }
    }
}