using System;
using System.Collections.Generic;
using UnityEngine;
public class YieldInstrucioner
{
    private static Dictionary<string, WaitForSeconds> waitforseconds = new Dictionary<string, WaitForSeconds>();
    private static Dictionary<string, WaitForSecondsRealtime> waitforsecondsrealtime = new Dictionary<string, WaitForSecondsRealtime>();
    private static WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private static WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    public static WaitForSecondsRealtime GetWaitForSecondsRealTime(float seconds)
    {
        string label = seconds.ToString("f2");
        if (!waitforsecondsrealtime.ContainsKey(label))
            waitforsecondsrealtime.Add(label, new WaitForSecondsRealtime(seconds));
        return waitforsecondsrealtime[label];
    }

    public static WaitForSeconds GetWaitForSeconds(float seconds)
    {
        string label = seconds.ToString("f2");
        if (!waitforseconds.ContainsKey(label))
            waitforseconds.Add(label, new WaitForSeconds(seconds));
        return waitforseconds[label];
    }

    public static WaitForFixedUpdate GetWaitForFixedUpdate()
    {
        return waitForFixedUpdate;
    }

    public static WaitForEndOfFrame GetWaitForEndOfFrame()
    {
        return waitForEndOfFrame;
    }

    public static WaitUntil GetWaitUntil(Func<bool> predicate)
    {
        return new WaitUntil(predicate);
    }

    public static WaitWhile GetWaitWhile(Func<bool> predicate)
    {
        return new WaitWhile(predicate);
    }
}