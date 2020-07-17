using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Threading;

public class WebRequestStream : BaseWebRequest
{
    private Action<byte[]> action;
    public WebRequestStream(WebRequestType type, string uri, string name, Action action) : base(type, uri, name)
    {

    }
    public WebRequestStream(WebRequestType type, string uri, string name) : base(type, uri, name)
    {
    }
    public override void OnFinished(DownloadHandler downloadHandler)
    {
        base.OnFinished(downloadHandler);

        if (action != null)
        {
            action.Invoke(downloadHandler.data);
        }
    }
    public override void OnPreWebRequest(UnityWebRequest request)
    {
        base.OnPreWebRequest(request);
    }

    public override void OnUpdateWebRequest(UnityWebRequestAsyncOperation aync)
    {
        base.OnUpdateWebRequest(aync);
    }
}

public class WebRequestText : BaseWebRequest
{
    private Action<string> action;

    public WebRequestText(WebRequestType type, string uri, string name, Action<string> action) : base(type, uri, name)
    {

    }
    public WebRequestText(WebRequestType type, string uri, string name) : base(type, uri, name)
    {
    }

    public override void OnFinished(DownloadHandler downloadHandler)
    {
        base.OnFinished(downloadHandler);

        if (action != null)
        {
            action.Invoke(downloadHandler.text);
        }
    }
    public override void OnPreWebRequest(UnityWebRequest request)
    {
        base.OnPreWebRequest(request);

    }

    public override void OnUpdateWebRequest(UnityWebRequestAsyncOperation aync)
    {
        base.OnUpdateWebRequest(aync);

    }
}

public class WebRequestTexture : BaseWebRequest
{
    private Action<Texture2D> action;
    public WebRequestTexture(WebRequestType type, string uri, string name, Action<Texture2D> action) : base(type, uri, name)
    {
        this.action = action;
    }
    public WebRequestTexture(WebRequestType type, string uri, string name) : base(type, uri, name)
    {
    }

    public override void OnFinished(DownloadHandler downloadHandler)
    {
        base.OnFinished(downloadHandler);
        if (action != null)
        {
            DownloadHandlerTexture download = downloadHandler as DownloadHandlerTexture;
            action.Invoke(download.texture);
        }
    }

    public override void OnPreWebRequest(UnityWebRequest request)
    {
        base.OnPreWebRequest(request);
        request.downloadHandler = new DownloadHandlerTexture(true);
    }

    public override void OnUpdateWebRequest(UnityWebRequestAsyncOperation aync)
    {
        base.OnUpdateWebRequest(aync);
        Debug.Log(Thread.CurrentThread.Name);
    }
}

public enum WebRequestType
{
    Unknow,
    Texture,
    Audio,
    Text,
    Stream,
}

public abstract class BaseWebRequest
{
    protected WebRequestType mWebRequestType;
    public WebRequestType webRequestType
    {
        get { return mWebRequestType; }
    }

    protected string mName;
    public string name
    {
        get { return mName; }
    }

    protected string mUrl;
    public string uri { get { return mUrl; } }
    public bool loading = false;

    public BaseWebRequest(WebRequestType type, string uri, string name)
    {
        mWebRequestType = type;
        mUrl = uri;
        mName = name;
    }

    public virtual void OnFinished(DownloadHandler downloadHandler)
    {
        loading = false;
    }
    public virtual void OnPreWebRequest(UnityWebRequest request)
    {
        loading = true;
    }
    //加载的进度还有问题
    public virtual void OnUpdateWebRequest(UnityWebRequestAsyncOperation aync)
    {
        Debug.Log("加载进度:" + aync.progress);
    }

    public void Reset()
    {
        mUrl = string.Empty;
        mName = string.Empty;
        loading = false;
    }

}
