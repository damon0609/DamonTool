using System.Collections;
using System.Collections.Generic;
using Damon;
using Damon.Tool;
using HT;
using LitJson;
using UnityEngine;
public class ZTestHT : MonoBehaviour, ILog {

    private Entity entity;

    public GameObject prefab;
    ReferencePool referencePool = new ReferencePool ();

    private EntityNPC npc01;

    void Start () {

        entity = new Entity ();
        Main.eventManager.Register (typeof (LoginEvent), (System.Object obj, BaseEvent e) => {
            LoginEvent login = e as LoginEvent;
            Debug.Log (login.ToString ());
        });

        Main.eventManager.Register(typeof (EntityCreateEvent),(object o,BaseEvent e) =>{

            this.d("damon",o.GetType().ToString());
            this.d("damon",e.GetType().ToString());

        });
    }


    void Update () {

        if (Input.GetKeyDown (KeyCode.Space)) {
            LoginEvent loginEvent = new LoginEvent ("damon", "11111");
            Main.eventManager.Trigger (this, loginEvent);
        }

        if (Input.GetKeyDown (KeyCode.A)) {
            npc01 = referencePool.OnSpawn (typeof (EntityNPC)) as EntityNPC;
            this.d ("entity npc:", npc01.ToString ());
        }

        if (Input.GetKeyDown (KeyCode.B)) {
            referencePool.OnDeSpawn (npc01);
            npc01 = null;
        }

    }
    void OnDestroy () {
        Main.eventManager.UnRegister<LoginEvent> ();
    }
}