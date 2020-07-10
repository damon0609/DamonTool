using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Damon;
using Damon.Tool;
using HT;
using LitJson;
using UnityEngine;
public class ZTestHT : MonoBehaviour, ILog
{

    private Entity entity;
    public GameObject prefab;
    ReferencePool referencePool = new ReferencePool();
    private EntityNPC npc01;
    public class TankResInfo : BaseResourcesInfo
    {
        public TankResInfo(string assetBundleName, string assetPath, string resPath) : base(assetBundleName, assetPath, resPath)
        {
        }
    }

    public IEnumerator cor01()
    {
        yield return YieldInstrucioner.GetWaitForSeconds(1.0f);
        Debug.Log("cor01");
    }
    CoroutineAction<float> cor03;
    IEnumerator Print(float count)
    {
        yield return YieldInstrucioner.GetWaitForSeconds(3.0f);
        Debug.Log(count);
    }

    IEnumerator PrintAction(DAction action)
    {
        yield return YieldInstrucioner.GetWaitUntil(() => { return true; });
        if (action != null)
            action();
    }

    IEnumerator PrintAction(string arg1, string arg2)
    {
        yield return YieldInstrucioner.GetWaitUntil(() => { return true; });
    }

    IEnumerator PrintAction(float arg1, float arg2)
    {
        yield return YieldInstrucioner.GetWaitUntil(() =>
        {
            return arg1 > arg2;
        });
    }
     void Start()
    {

        cor03 = Print;

        string id1 = Main.coroutiner.Run(cor01);
        string id2 = Main.coroutiner.Run<float>(cor03, 1);


        string id3 = Main.coroutiner.Run<DAction>(PrintAction, () =>
        {
            //Debug.Log("ni hao");
        });
        Main.coroutiner.Run<DAction>(PrintAction, () =>
        {
            //Debug.Log("ni hao damon");
        });

        Main.coroutiner.Run<string, string>(PrintAction, "a", "b");
        Main.coroutiner.Run<float, float>(PrintAction, 1, 2);

        //TankResInfo resInfo = new TankResInfo("models", "Assets/Tanks/Assets/Models/Radar.fbx", "");
        //StartCoroutine(Main.resourceManager.LoadAssetAsync(resInfo, null,obj=> {

        //    //GameObject go = Instantiate<GameObject>((obj as UnityEngine.Object));
        //    this.d("damon","---");
        //}));

        //entity = new Entity();
        //Main.eventManager.Register(typeof(LoginEvent), (System.Object obj, BaseEvent e) =>
        //{
        //    LoginEvent login = e as LoginEvent;
        //});

        //Main.eventManager.Register(typeof(EntityCreateEvent), (object o, BaseEvent e) =>
        //{
        //    this.d("damon", o.GetType().ToString());
        //    this.d("damon", e.GetType().ToString());
        //});
    }


    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    LoginEvent loginEvent = new LoginEvent("damon", "11111");
        //    Main.eventManager.Trigger(this, loginEvent);
        //}

        //if (Input.GetKeyDown (KeyCode.A)) {
        //    npc01 = referencePool.OnSpawn (typeof (EntityNPC)) as EntityNPC;
        //    this.d ("entity npc:", npc01.ToString ());
        //}

        //if (Input.GetKeyDown (KeyCode.B)) {
        //    referencePool.OnDeSpawn (npc01);
        //    npc01 = null;
        //}
        //if(Main.inputManager.GetButtonDown("Mouseleft"))
        //{
        //    Debug.Log("---------------------");
        //}
    }
    void OnDestroy()
    {
        //Main.eventManager.UnRegister<LoginEvent> ();
    }
}