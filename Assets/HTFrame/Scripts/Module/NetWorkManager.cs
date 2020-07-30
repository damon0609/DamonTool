using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

[InternalModule(HTFrameworkModuleType.NetWork)]
public class NetWorkManager : InternalBaseModule
{
    [SerializeField]
    private string mServerIp;
    public string ip { get { return mServerIp; } set { mServerIp = value; } }

    [SerializeField]
    private int mPort;
    public int serverPort { get { return mPort; } set { mPort = value; } }

    [SerializeField]
    private string mClientIp;
    public string clientIP { get { return mClientIp; } set { mClientIp = value; } }

    [SerializeField]
    private int mClientPort;
    public int clentPort { get { return mClientPort; } set { mClientPort = value; } }

    [SerializeField]
    public List<string> mProtocolTypes = new List<string>();

    [SerializeField]
    public Dictionary<string, BaseProtocolChannel> protocols = new Dictionary<string, BaseProtocolChannel>();//这里通过反射也可以获取该字段值
    private Dictionary<int, Action<INetworkMessage>> messageHandler = new Dictionary<int, Action<INetworkMessage>>();
    private Queue<INetworkMessage> messageQueue;
    public IPEndPoint ipe
    {
        get { return new IPEndPoint(IPAddress.Parse(ip), serverPort); }
    }

    public void Conncet(string typeName)
    {
        StartCoroutine(ConnectServer(typeName));
    }
    private IEnumerator ConnectServer(string typeName)
    {
        yield return null;
        BaseProtocolChannel channel = protocols[typeName];
        channel.Connect(ipe);
    }

    public void DisConnectServer(string typeName)
    {
        BaseProtocolChannel channel = protocols[typeName];
        if (channel != null)
        {
            channel.DisConnect();
        }
    }

    public void ResgisterMessage(int cmd,Action<INetworkMessage> action)
    {
        if(!messageHandler.ContainsKey(cmd))
        {
            messageHandler[cmd]=action;
        }
    }
    public void SendMessage(string typeName, INetworkMessage message)
    {
        BaseProtocolChannel channel = protocols[typeName];
        if (channel != null)
        {
            channel.Send(message);
        }
    }
    public void ReceiveMessage(BaseProtocolChannel channel, INetworkMessage message)
    {
        messageQueue.Enqueue(message);
    }
    public override void OnInitialization()
    {
        base.OnInitialization();
        messageQueue = new Queue<INetworkMessage>();
        foreach (BaseProtocolChannel c in protocols.Values)
        {
            c.Init();
            c.sendMessage += channel => { };
            c.receiveMessage += (channel, message) => { };
            c.connectSuccessed += (channel) => { };
            c.connectFailed += (channel) => { };
            c.disConnected += (channel) => { };
        }
    }
    public override void OnPause()
    {

    }
    public override void OnPreparatory()
    {

    }
    public override void OnRefresh()
    {
        while (messageQueue.Count > 0)
        {
            INetworkMessage message = messageQueue.Dequeue();//网络通道获取的消息上传到manager中进行处理
            int mainCmd = (message as TCPNetworkMessage).mainCmd;
            if(messageHandler.ContainsKey(mainCmd))
            {
                Action<INetworkMessage> action = messageHandler[mainCmd];
                action.Invoke(message);
            }
        }
    }
    public override void OnResume()
    {

    }
    public override void OnTermination()
    {
        messageQueue.Clear();
        foreach (BaseProtocolChannel c in protocols.Values)
        {
            c.OnTermination();
        }
    }
}
