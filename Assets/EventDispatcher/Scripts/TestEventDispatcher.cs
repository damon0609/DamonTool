using System.Collections;
using System.Collections.Generic;
using Damon.Tool.Events;
using UnityEngine;
public class TestEventDispatcher : MonoBehaviour {

    EventDispatcher eventDispatcher = new EventDispatcher ();
    void Start () {

        eventDispatcher.Register ("Idle", () => {
            Debug.Log ("进入idle状态");
        });

        eventDispatcher.Register<float> ("Attack", (float value) => {
            Debug.Log ("进入Attack状态=>" + value);
        });

        eventDispatcher.Register<float, float, float> ("Run", RunCallBack);
    }

    //触发时需要使用的逻辑
    bool RunCallBack (float v1, float v2, float v3) {
        if (v1 > v2) {
            Debug.Log ("true");
            return true;
        } else {
            Debug.Log ("false");
            return false;
        }
    }

    void Update () {

        if (eventDispatcher != null) {
            if (Input.GetKeyDown (KeyCode.A)) {
                eventDispatcher.Trigger ("Idle");
            }

            if (Input.GetKeyDown (KeyCode.D)) {
                eventDispatcher.Trigger ("Attack", 1.0f);
            }
            if (Input.GetKeyDown (KeyCode.W)) {
                eventDispatcher.Trigger ("Run", 5.0f, 2.0f, 3.0f);
            }
        }
    }
}