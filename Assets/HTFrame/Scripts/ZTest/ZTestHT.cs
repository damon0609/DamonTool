using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class ZTestHT : MonoBehaviour
{
    void Start()
    {
        JsonData npc = new JsonData();
        npc["name"]="npc1";


        BaseDateSet set = Main.dateManager.CreateDateSet(typeof(NPCDate),npc);
        JsonData data= set.Pack();

    }

    void Update()
    {
        
    }
}
