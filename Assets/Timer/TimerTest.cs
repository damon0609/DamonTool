using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour {
    void Start () {
        Timer timer = new Timer (this, 1.0f);
        timer.Start (v => { Debug.Log (v); },()=>{
            Debug.Log("计时器结束");
        });
    }
    void Update () {

    }
}