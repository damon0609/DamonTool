using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public abstract class BaseDateSet : ScriptableObject
{
    public abstract void Fill(JsonData data);
    public abstract JsonData Pack();
}
