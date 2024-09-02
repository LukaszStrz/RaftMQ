using RaftMQ.Transport.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ.Transport
{
    public interface IRaftTransport : IDisposable
    {
        delegate void RequestVoteMessageReceivedHandler(object sender, GenericMessageData data);
        delegate void VoteMessageReceivedHandler(object sender, VoteMessageData data);

        event RequestVoteMessageReceivedHandler RequestVoteMessageReceived;
        event VoteMessageReceivedHandler VoteMessageReceived;

        void SendRequestVoteMessage(int term);
        void SendVoteMessage(int term, Guid target);
    }
}
