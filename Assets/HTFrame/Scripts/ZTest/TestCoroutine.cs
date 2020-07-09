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
        yield return YieldInstrucioner.GetWaitForSeconds(1);
        Debug.Log(s);
        // Main.coroutiner.Stop(id);
        // Main.coroutiner.Return(id);
    }
    IEnumerator Test022(string s)
    {
        yield return YieldInstrucioner.GetWaitForSeconds(2);
        Debug.Log(s);
        // Main.coroutiner.Stop(id);
        // Main.coroutiner.Return(id);
    }

    CoroutineItem coroutineItem;

    delegate IEnumerator IEnumeratorDele(string s);

    private IEnumeratorDele ieTest01;
    private IEnumeratorDele ieTest02;
    void Start()
    {
        ieTest01 = Test02;
        ieTest02 = Test022;
        coroutineItem = new CoroutineItem();
        coroutineItem.Fill(ieTest01, new object[] { "ssss" });
        coroutineItem.Run();

        CoroutineItem coroutineItem01 = new CoroutineItem();
        coroutineItem01.Fill(ieTest01, new object[] { "aaaa" });
        coroutineItem01.Run();
        // coroutineItem.Fill(ieTest02, null);
        // coroutineItem.Run();
        // Debug.Log(Main.coroutiner.Run<Action<string>, string>(Test01, str => { Debug.Log(str); }, "11111"));
        // Debug.Log(Main.coroutiner.Run<Action<string>, string>(Test011, str => { Debug.Log(str); }, "22222"));
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
