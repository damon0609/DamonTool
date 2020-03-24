using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TestSingleton : MonoBehaviour
{
    Dictionary<string, Dictionary<string, string>> GetTableData()
    {
        TextAsset text = ResourceManager.Instance.LoadRes<TextAsset>(ResourceManager.ResourceType.Data, "npc");
        return CSVTool.Parse(text.text);

    }
    void Start()
    {
        //Dictionary<string, Dictionary<string, string>> dic = GetTableData();
        //string path = Application.dataPath + "/GameFrame/Res/Data/";
        //XMLHelpler xmlHelper = new XMLHelpler(path + "npc.xml", "npcs");
        //BaseNPC npc = NPCManager.Instance.NextNPC();
        //while (npc != null)
        //{
        //    XmlElement en = xmlHelper.AddRootClidNode("npc");
        //    en.SetAttribute("id", npc.id + "");
        //    en.AppendChild(xmlHelper.CreateElement("name", npc.name));
        //    en.AppendChild(xmlHelper.CreateElement("position", npc.position));
        //    en.AppendChild(xmlHelper.CreateElement("task", npc.task));
        //    en.AppendChild(xmlHelper.CreateElement("message", npc.message));
        //    en.AppendChild(xmlHelper.CreateElement("resPath", npc.resPath));
        //    npc = NPCManager.Instance.NextNPC();
        //}
        //xmlHelper.Save();

        string path = Application.dataPath + "/GameFrame/Res/Data/";
        XMLHelpler xmlHelper = new XMLHelpler();
        xmlHelper.CreateXML(path + "npc.xml");
        XmlNodeList list = xmlHelper.rootNode.ChildNodes;
        foreach (XmlNode node in list)
        {
            Debug.Log(node.Name);
        }
        xmlHelper.Save();
    }

    void Update()
    {

    }
}
