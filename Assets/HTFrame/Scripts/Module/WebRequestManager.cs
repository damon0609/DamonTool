using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Threading;

[InternalModule(HTFrameworkModuleType.WebRequest)]
public class WebRequestManager : InternalBaseModule
{
    private Dictionary<WebRequestType, List<BaseWebRequest>> webrequests = new Dictionary<WebRequestType, List<BaseWebRequest>>();
    public void SendRequest(WebRequestType type, string name)
    {
        Main.instance.StartCoroutine(SendWebRequest(type, name));
    }

    private IEnumerator SendWebRequest(WebRequestType type, string name)
    {
        BaseWebRequest webRequestInfo = GetWebRequest(type, name);
        using (UnityWebRequest unityWeb = UnityWebRequest.Get(webRequestInfo.uri))
        {
            webRequestInfo.OnPreWebRequest(unityWeb);//开始加载
            UnityWebRequestAsyncOperation async = unityWeb.SendWebRequest();
            webRequestInfo.OnUpdateWebRequest(async);
            yield return async;
            if (!unityWeb.isHttpError && !unityWeb.isNetworkError)//加载结束
                webRequestInfo.OnFinished(unityWeb.downloadHandler);
            else
                Debug.Log(unityWeb.error);
            unityWeb.Dispose();
        }
    }

    public void Register(BaseWebRequest webRequest)
    {
        if (!webrequests.ContainsKey(webRequest.webRequestType))
        {
            webrequests[webRequest.webRequestType] = new List<BaseWebRequest>();
            webrequests[webRequest.webRequestType].Add(webRequest);
        }
        else
        {
            if (!webrequests[webRequest.webRequestType].Contains(webRequest))
                webrequests[webRequest.webRequestType].Add(webRequest);
        }
    }
    public void UnRegister(WebRequestType type, string name)
    {

        List<BaseWebRequest> list;
        if (webrequests.TryGetValue(type, out list))
        {
            foreach (BaseWebRequest b in list)
            {
                if (b.name == name)
                {
                    list.Remove(b);
                }
            }
            if (list.Count <= 0)
            {
                list.Clear();
                webrequests.Remove(type);
            }
        }
    }

    public void Clear()
    {
        webrequests.Clear();
    }

    public BaseWebRequest GetWebRequest(WebRequestType type, string name)
    {
        BaseWebRequest webRequest = null;
        List<BaseWebRequest> list;
        if (webrequests.TryGetValue(type, out list))
        {
            foreach (BaseWebRequest b in list)
            {
                if (b.name == name)
                    webRequest = b;
            }
        }
        return webRequest;
    }


    public override void OnInitialization()
    {

    }
    public override void OnPause()
    {

    }
    public override void OnPreparatory()
    {

    }
    public override void OnRefresh()
    {

    }
    public override void OnResume()
    {

    }
    public override void OnTermination()
    {

    }
}
