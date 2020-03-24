using System;
using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class XMLHelpler
{
    private XmlDocument xmlDocument;
    private string mPath;
    private XmlElement mRootNode;
    public XmlElement rootNode
    {
        get { return mRootNode; }
        set { mRootNode = value; }
    }

    private string mRootName;
    public string rootName
    {
        get { return mRootName; }
        set { mRootName = value; }
    }

    public XMLHelpler(string path, string root)
    {
        this.mPath = path;
        mRootName = root;
        Init();
    }
    public XMLHelpler()
    {

    }
    public void CreateXML(string path)
    {
        this.mPath = path;
        Init();
    }

    void Init()
    {
        xmlDocument = new XmlDocument();
        if (!File.Exists(mPath))
        {
            File.Create(mPath).Dispose();

            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.AppendChild(declaration);
            if (mRootName == string.Empty || mRootName == null)
            {
                Debug.Log("根节点未赋值");
                return;
            }
            mRootNode = xmlDocument.CreateElement(mRootName);
            xmlDocument.AppendChild(rootNode);
        }
        else
        {
            xmlDocument.Load(mPath);
            mRootName = xmlDocument.DocumentElement.Name;
            mRootNode = (XmlElement)xmlDocument.SelectSingleNode(mRootName);
        }
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
        XmlElement en = xmlDocument.CreateElement(name);
        rootNode.AppendChild(en);
        return en;
    }

    public XmlElement CreateElement(string name, string value)
    {
        XmlElement en = xmlDocument.CreateElement(name);
        en.InnerText = value;
        return en;
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
