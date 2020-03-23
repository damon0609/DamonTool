using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSingleton : MonoBehaviour
{

    void Start()
    {
        string path = Application.dataPath + "/DFrame/Res/Data/";
        XMLHelpler xmlHelper = new XMLHelpler(path + "npc.xml", "npcs");
        //xmlHelper.AddRootClidNode("npc");

        //for (int i = 0; i < 9; i++)
        //{
        //    BaseNPC npc = NPCManager.Instance.GetNPC(i);
        //    xmlHelper.AddChildNodeByAttribute("npcs/npc", npc.id.ToString(), "name", npc.name);
        //    xmlHelper.AddChildNodeByAttribute("npcs/npc", npc.id.ToString(), "position", npc.position);
        //    xmlHelper.AddChildNodeByAttribute("npcs/npc", npc.id.ToString(), "task", npc.task);
        //    xmlHelper.AddChildNodeByAttribute("npcs/npc", npc.id.ToString(), "message", npc.message);
        //    xmlHelper.AddChildNodeByAttribute("npcs/npc", npc.id.ToString(), "resPath", npc.resPath);
        //}
        xmlHelper.Save();
    }

    void Update()
    {

    }
}
