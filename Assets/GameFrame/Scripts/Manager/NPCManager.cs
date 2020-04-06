using System;
using System.Collections.Generic;
using Damon.Tool;
using UnityEngine;
public class NPCManager : Singleton<NPCManager> {
    protected NPCManager () { }

    private Dictionary<string, Dictionary<string, string>> mDic;

    private List<BaseNPC> npcs = new List<BaseNPC> ();

    private int index = 0;

    public BaseNPC GetNPC (int id) {
        return npcs.Find ((BaseNPC n) => { return n.id == id; });
    }

    public BaseNPC NextNPC () {
        if (index < npcs.Count) {
            return npcs[index++];
        }
        return null;
    }

    public override void OnSingletonInit () {
        base.OnSingletonInit ();
        TextAsset text = ResourceManager.Instance.LoadRes<TextAsset> (ResourceManager.ResourceType.Data, "npc");
        if (text == null) {
            Debug.LogError ("load npc data failed");
            return;
        }
        mDic = CSVTool.Parse (text.text);
        foreach (string id in mDic.Keys) {
            BaseNPC npc = new BaseNPC ();
            npc.id = int.Parse (id);
            npc.name = mDic[id]["Name"];
            npc.position = mDic[id]["Position"];
            npc.task = mDic[id]["Task"];
            npc.message = mDic[id]["Message"];
            npc.resPath = mDic[id]["ResPath"];
            npcs.Add (npc);
        }
    }

    public override void Dispose () {
        base.Dispose ();
    }
}