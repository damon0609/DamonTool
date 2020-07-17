using System.Threading;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Net;

public interface INetworkMessage
{

}
public sealed class TCPNetworkMessage : INetworkMessage //不能被继承
{
    public int md5;
    public int bodyLenght;
    public int sessionId;
    public int mainCmd;
    public int cmd;
    public int secuity;
    public int returnCode;
    public TCPNetworkMessage(int md5, int length, int id, int cmd, int subCmd, int s, int retrunCode)
    {
        this.md5 = md5;
        this.bodyLenght = length;
        this.sessionId = id;
        this.mainCmd = cmd;
        this.cmd = subCmd;
        this.secuity = s;
        this.returnCode = retrunCode;
    }
}

public abstract class BaseProtocolChannel
{
    protected bool mState;
    public virtual bool state
    {
        get
        {
            return mState;
        }
        private set { }
    }

    protected ProtocolType mProtocolType;
    public virtual ProtocolType protocolType
    {
        get { return mProtocolType; }
    }

    protected SocketType mSocketType;
    public SocketType socketType
    {
        get { return mSocketType; }
    }
    protected Socket mClient;

    protected bool isActive = false;
    protected bool isNeedConnect = true;

    protected Thread sendThread;
    protected Thread receiveThread;

    protected NetWorkManager mNetWorkManager;

    protected Queue<byte[]> mBufferMessage;

    public event Action<BaseProtocolChannel> connectSuccessed;
    public event Action<BaseProtocolChannel> connectFailed;
    public event Action<BaseProtocolChannel> disConnected;
    public event Action<BaseProtocolChannel> sendMessage;
    public event Action<BaseProtocolChannel, INetworkMessage> receiveMessage;

    protected abstract INetworkMessage ReceiveMessage();
    public abstract void Send(INetworkMessage message);
    protected abstract byte[] ParseMessage(INetworkMessage message);

    protected BaseProtocolChannel(NetWorkManager m)
    {
        this.mNetWorkManager = m;
    }

    public virtual void Init()
    {
        isActive = true;
        mBufferMessage = new Queue<byte[]>();
        if (isNeedConnect)
        {
            sendThread = new Thread(SendNeedConnect);
            sendThread.Start();

            receiveThread = new Thread(ReceiveNeedConnect);
            sendThread.Start();
        }
        else
        {
            sendThread = new Thread(SendNoConnect);
            sendThread.Start();

            receiveThread = new Thread(ReceiveNoConnect);
            sendThread.Start();
        }
    }
    protected virtual void SendNeedConnect()
    {
        if (mState)
        {
            while (mBufferMessage.Count > 0)
            {
                byte[] bytes = mBufferMessage.Dequeue();
                int len = mClient.Send(bytes);
                if (len > 0)
                {
                    if (sendMessage != null)
                        sendMessage.Invoke(this);
                }
            }
        }
    }

    protected virtual void ReceiveNeedConnect()
    {
        while (isActive)
        {
            if (state)
            {
                INetworkMessage message = ReceiveMessage();
                if (mNetWorkManager != null)
                    mNetWorkManager.ReceiveMessage(this, message);
                if (receiveMessage != null)
                {
                    receiveMessage.Invoke(this, message);
                }
            }
        }
    }
    protected virtual void SendNoConnect()
    {
        while (mBufferMessage.Count > 0)
        {
            byte[] bytes = mBufferMessage.Dequeue();
            int len = mClient.SendTo(bytes, mNetWorkManager.ipe);
            if (len > 0)
            {
                if (sendMessage != null)
                    sendMessage.Invoke(this);
            }
        }
    }
    protected virtual void ReceiveNoConnect()
    {
        while (isActive)
        {
            INetworkMessage message = ReceiveMessage();
            if (mNetWorkManager != null)
                mNetWorkManager.ReceiveMessage(this, message);
            if (receiveMessage != null)
            {
                receiveMessage.Invoke(this, message);
            }
        }
    }
    public virtual void Connect(IPEndPoint ipe)
    {
        mClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, protocolType);
        try
        {
            mClient.BeginConnect(ipe, async =>
                   {
                       Socket obj = async.AsyncState as Socket;
                       obj.EndConnect(async);
                       mState = true;
                       if (connectSuccessed != null)
                           connectSuccessed.Invoke(this);
                   }, mClient);
        }
        catch (SocketException ex)
        {
            if (connectFailed != null)
                connectFailed.Invoke(this);
        }
    }
    public virtual void DisConnect()
    {
        if (mClient != null)
        {
            if (mState && mClient.Connected && isNeedConnect)
            {
                mClient.Shutdown(SocketShutdown.Both);
                mClient.Disconnect(false);
                mClient.Close();
                mState = false;
                if (disConnected != null)
                {
                    disConnected.Invoke(this);
                }
            }
            mBufferMessage.Clear();
            mClient = null;
        }
    }
    public virtual void OnTermination()
    {
        DisConnect();
        isActive = false;
        mState = false;
        sendThread.Abort();
        receiveThread.Abort();
    }
}
