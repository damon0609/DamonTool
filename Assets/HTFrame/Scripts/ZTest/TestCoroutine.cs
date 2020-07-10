using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class TestCoroutine : MonoBehaviour
{
    public override string ToString()
    {
        return ".....";
    }
    public Transform target;
    private string id;
    IEnumerator Test01(Action<string> action, string str)
    {
        yield return YieldInstrucioner.GetWaitForSeconds(1);
        if (action != null)
            action(str);
        // Main.coroutiner.Run(Test02);
    }
    IEnumerator Test011(Action<string> action, string str)
    {
        yield return YieldInstrucioner.GetWaitForSeconds(2);
        if (action != null)
            action(str);
        // Main.coroutiner.Run(Test022);
    }
    IEnumerator Test02(string s)
    {
        yield return YieldInstrucioner.GetWaitForSeconds(15);
        // Main.coroutiner.Stop(id);
        // Main.coroutiner.Return(id);
    }
    IEnumerator Test022(string s)
    {
        yield return YieldInstrucioner.GetWaitForSeconds(1);
        // Main.coroutiner.Stop(id);
        // Main.coroutiner.Return(id);
    }

    CoroutineAction<string> delegateTest02;
    CoroutineAction<Action<string>, string> delegateTest01;

    void Start()
    {
        delegateTest01 = Test01;
        delegateTest01 = Test011;
        Main.coroutiner.Run<Action<string>,string>(delegateTest01,s=>{},"damon1");
        Main.coroutiner.Run<Action<string>,string>(delegateTest01,s=>{} ,"damon2");

        delegateTest02 = Test02;
        delegateTest02 = Test022;
        Main.coroutiner.Run(delegateTest02, "damon1");
        Main.coroutiner.Run(delegateTest02, "damon2");

    }
    IEnumerator Test03()
    {
        yield return new WaitForSeconds(2);
        Main.coroutiner.Return(id);
    }

    void Update()
    {

    }
    private void OnApplicationQuit()
    {
        Main.coroutiner.ClearNotRunning();
    }
    private void OnDestroy()
    {
    }
}
