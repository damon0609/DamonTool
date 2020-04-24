using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SettingItem {
    public string nameValue;

    public Text nameLabel;
    public Button button01;
    public Button button02;
}

public class TestPropertyDrawer : MonoBehaviour {

    public SettingItem settings;
    void Start () {

    }

    void Update () {

    }
}