using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[DefaultExecutionOrder (-1000)]
public sealed partial class Main : MonoBehaviour {

    public static Main instance;

    private void Awake () {
        DontDestroyOnLoad (gameObject);
        instance = this;
        InitModule ();
    }

    void Start () {
        PreparatoryModule ();
    }

    void Update () {
        ModuleUpdate ();
    }
}