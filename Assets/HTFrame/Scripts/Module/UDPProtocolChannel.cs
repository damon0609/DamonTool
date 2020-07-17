using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPProtocolChannel : BaseProtocolChannel
{
    public UDPProtocolChannel(NetWorkManager m) : base(m)
    {
    }
    protected override byte[] ParseMessage(INetworkMessage message)
    {
        return null;
    }
    public override void Send(INetworkMessage message)
    {

    }
    protected override INetworkMessage ReceiveMessage()
    {
        return null;
    }
}
