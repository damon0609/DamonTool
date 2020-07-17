
using System.Net.Sockets;
using System;

public class TCPProtocolChannel : BaseProtocolChannel
{
    public TCPProtocolChannel(NetWorkManager m) : base(m)
    {
    }
    public override ProtocolType protocolType
    {
        get
        {
            mProtocolType = ProtocolType.Tcp;
            return mProtocolType;
        }
    }

    protected override byte[] ParseMessage(INetworkMessage message)
    {
        TCPNetworkMessage temp = message as TCPNetworkMessage;
        byte[] totalBytes = new byte[temp.bodyLenght+32];

        byte[] mdBytes= BitConverter.GetBytes(temp.md5);
        Array.Copy(mdBytes,0,totalBytes,0,4);

        Array.Copy(BitConverter.GetBytes(temp.bodyLenght),0,totalBytes,4,4);
        Array.Copy(BitConverter.GetBytes(temp.sessionId),0,totalBytes,8,8);
        Array.Copy(BitConverter.GetBytes(temp.mainCmd),0,totalBytes,16,4);
        Array.Copy(BitConverter.GetBytes(temp.cmd),0,totalBytes,20,4);
        Array.Copy(BitConverter.GetBytes(temp.secuity),0,totalBytes,24,4);
        Array.Copy(BitConverter.GetBytes(temp.returnCode),0,totalBytes,28,4);
        return totalBytes;
    }
    public override void Send(INetworkMessage message)
    {
        if (mState)
        {
            byte[] bytes = ParseMessage(message);
            mBufferMessage.Enqueue(bytes);
        }
    }
    protected override INetworkMessage ReceiveMessage()
    {
        # region 接受消息头
        int receHeaderLength = 32;
        byte[] receHeader = new byte[receHeaderLength];//规定接受固定长度的消息头
        while (receHeader.Length > 0)
        {
            byte[] tempReceHeader = new byte[32];//接受的临时消息头
            int tempLength = 0;
            if (tempLength <= receHeaderLength)//消息长度不足
            {
                tempLength = mClient.Receive(tempReceHeader, tempReceHeader.Length, 0);
            }
            else
            {
                tempLength = mClient.Receive(tempReceHeader, receHeaderLength, 0);
            }
            tempReceHeader.CopyTo(receHeader, receHeader.Length - receHeaderLength);
            receHeaderLength -= tempLength;//减去已经接受的长度
        }
        #endregion


        #region 接受消息体
        byte[] bodyLengthBytes = new byte[4];
        Array.Copy(receHeader, 4, bodyLengthBytes, 0, 4);
        int bodyLength = BitConverter.ToInt32(bodyLengthBytes, 0);

        byte[] bodyBytes = new byte[bodyLength];
        while (bodyLength > 0)
        {
            byte[] tempBytes = new byte[bodyLength > 1024 ? 1024 : bodyLength];//最大长度不超过1024
            int alreadlyLen = 0;
            if (alreadlyLen <= bodyLength)
            {
                alreadlyLen = mClient.Receive(tempBytes, tempBytes.Length, 0);
            }
            else
            {
                alreadlyLen = mClient.Receive(tempBytes, bodyLength, 0);
            }
            tempBytes.CopyTo(bodyBytes, bodyBytes.Length - bodyLength);
            bodyLength -= alreadlyLen;
        }
        #endregion

        int md5 = BitConverter.ToInt32(receHeader, 0);//消息校验
        int length = BitConverter.ToInt32(receHeader, 4);//消息体长度
        int sessionId = BitConverter.ToInt32(receHeader, 8);//身份id;
        int mainCmd = BitConverter.ToInt32(receHeader, 16);//主命令
        int cmd = BitConverter.ToInt32(receHeader, 20);//子命令
        int security = BitConverter.ToInt32(receHeader, 24);//加密方式
        int returnCode = BitConverter.ToInt32(receHeader, 28);//返回码

        TCPNetworkMessage message = new TCPNetworkMessage(md5, length, sessionId, mainCmd, cmd, security, returnCode);
        return message;
    }
}
