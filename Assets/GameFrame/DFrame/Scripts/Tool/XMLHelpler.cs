using System;
using System.Xml;
using System.IO;
using UnityEngine;
public class XMLHelpler
{
    private XmlDocument xmlDocument;
    private string mPath;
    public XMLHelpler(string path, string root)
    {
        if (!Directory.Exists(path))
            File.Create(path).Dispose();

        this.mPath = path;

        xmlDocument = new XmlDocument();
        XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
        xmlDocument.AppendChild(declaration);

        XmlElement rootNode = xmlDocument.CreateElement(root);
        xmlDocument.AppendChild(rootNode);
        xmlDocument.Save(path);
    }


    public XmlNode SelectedNodeByAttribute(string nodeName, string attributeName)
    {
        XmlNode temp = null;
        if (xmlDocument != null)
        {
            XmlNodeList list = xmlDocument.SelectNodes(nodeName);
            foreach (XmlNode item in list)
            {
                XmlAttributeCollection attributes = item.Attributes;
                foreach (XmlAttribute xml in attributes)
                {
                    if (xml.Value == attributeName)
                    {
                        temp = item;
                        break;
                    }
                }
            }
        }
        return temp;
    }

    public XmlNode SelectedSingleNode(string node)
    {
        if (xmlDocument != null)
        {
            return xmlDocument.SelectSingleNode(node);
        }
        return null;
    }

    public XmlElement AddRootClidNode(string name)
    {
        XmlNode root = xmlDocument.DocumentElement.FirstChild;
        Debug.Log(xmlDocument.DocumentElement.FirstChild.Name);
        //XmlElement en = xmlDocument.CreateElement(name);
        //root.AppendChild(en);
        return null;
    }

    public void AddChildNodeByAttribute(string nodeName, string attributeName, string name, string value)
    {
        if (xmlDocument != null)
        {
            XmlNode temp = SelectedNodeByAttribute(nodeName, attributeName);
            if (temp != null)
            {
                XmlElement child = xmlDocument.CreateElement(name);
                child.InnerText = value;
                temp.AppendChild(child);
            }
        }
    }

    public void Save()
    {
        xmlDocument.Save(mPath);
    }
}
