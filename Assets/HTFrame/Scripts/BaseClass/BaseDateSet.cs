using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
public abstract class BaseDateSet : ScriptableObject {
    public abstract void Fill (JsonData data);
    public abstract JsonData Pack ();
}