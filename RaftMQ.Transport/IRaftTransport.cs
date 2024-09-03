using RaftMQ.Transport.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RaftMQ.Transport
{
    public interface IRaftTransport : IDisposable
    {
        delegate Task RequestVoteMessageReceivedHandlerAsync(object sender, GenericMessageData data);
        delegate void VoteMessageReceivedHandler(object sender, VoteMessageData data);

        event RequestVoteMessageReceivedHandlerAsync RequestVoteMessageReceivedAsync;
        event VoteMessageReceivedHandler VoteMessageReceived;

        Task SendRequestVoteMessageAsync(int term);
        Task SendVoteMessageAsync(int term, Guid target);
    }
}
