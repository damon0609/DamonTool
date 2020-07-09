using System;
using System.Collections;
using System.Collections.Generic;
using Damon.Tool;
using UnityEngine;

[DisallowMultipleComponent]
[DefaultExecutionOrder (-1000)]
public sealed partial class Main : MonoBehaviour {
    public static Main instance;

    public bool log;

    private void Awake () {
        Log.on = log;
        DontDestroyOnLoad (gameObject);
        instance = this;
        InitModule ();

    }
    void Start () {
        PreparatoryModule ();
    }

    void Update () {
        if (Log.on != log) {
            Log.on = log;
        }
        ModuleUpdate ();
    }

   
    private void OnDestroy () {
        OnTermination ();
    }
}