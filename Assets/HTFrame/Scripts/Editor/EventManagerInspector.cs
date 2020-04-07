using UnityEditor;
using UnityEngine;
namespace HT {

    [GitHubURL ("https://github.com/")]
    [CSDNURL ("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.jpg")]
    [CustomEditor (typeof (EventManager))]
    public class EventManagerInspector : HTBaseEditor<EventManager> {
        private EventManager eventManager;

        protected override void OnDefaultEnable () {
            base.OnDefaultEnable ();
            eventManager = e as EventManager;
        }

        protected override void OnDefaultInspectorGUI () {
            base.OnDefaultInspectorGUI ();
        }
    }
}