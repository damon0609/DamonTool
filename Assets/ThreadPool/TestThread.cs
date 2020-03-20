using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damon.Tool.ThreadPool;
public class TestThread : MonoBehaviour {

    // Use this for initialization
    ThreadManager threadManager;

    ThreadInfo threadInfo;
    void Start () {

        threadManager = new ThreadManager();
        threadInfo = threadManager.Allocate();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            threadManager.Recycle(threadInfo);
        }
	}
}
