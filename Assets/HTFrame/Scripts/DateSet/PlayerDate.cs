using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class PlayerDate : BaseDateSet
{
    public string name;
    public int id;

    public float speed;

    public override void Fill(JsonData data)
    {
        throw new System.NotImplementedException();
    }

    public override JsonData Pack()
    {
        throw new System.NotImplementedException();
    }
}
