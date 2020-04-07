using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;


public class NPCDate : BaseDateSet {
    public string name;
    public int id;
    private JsonData jsonData;
    public override void Fill (JsonData data) {
        name = data["name"].ToString ();
        id = int.Parse (data["id"].ToString ());
        this.jsonData = data;
    }
    public override JsonData Pack () {
        return jsonData;
    }

    public override string ToString () {
        return base.ToString ();
    }
}