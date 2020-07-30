using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class Character
{

    [SerializeField]
    public Texture2D icon;

    [SerializeField]
    public GameObject perfab;

    [SerializeField]
    public GameObject weapon;

    [SerializeField]
    public string name;
}

public class URLInfoAttribute : PropertyAttribute
{
    public string uri;
    public string iconPath;
    public URLInfoAttribute(string mUri, string path)
    {
        this.uri = mUri;
        this.iconPath = path;
    }
}

