using System;
using System.Xml;
using UnityEngine;
namespace Damon.Table
{
    public class NPC : BaseTable
    {
        public NPC(string name) : base(name)
        {

        }

        protected override void Clear()
        {

        }

        protected override void InitData()
        {
            base.InitData();
            string path = Application.dataPath + "/GameFrame/Res/Data/";
            XMLHelpler xmlHelper = new XMLHelpler();
            xmlHelper.CreateXML(path + "npc.xml");
            XmlNodeList list = xmlHelper.rootNode.ChildNodes;
            foreach (XmlNode node in list)
            {
                string id = (node as XmlElement).GetAttribute("id");
                NPCItem item = new NPCItem();
                string name = node.SelectSingleNode("name").InnerText;
                string position = node.SelectSingleNode("position").InnerText;
                string task = node.SelectSingleNode("task").InnerText;
                string message = node.SelectSingleNode("message").InnerText;
                string resPath = node.SelectSingleNode("resPath").InnerText;

                item.id = int.Parse(id);
                item.name = name;
                item.pos = position;
                item.task = task;
                item.message = message;
                item.resPath = resPath;
                items.Add(item);

                Debug.Log(item.ToString());
            }
            xmlHelper.Save();
        }

        protected override void InitView()
        {

        }

        public override void ItemChange()
        {

        }

        public override void Close()
        {
            base.Close();
        }
    }
}
