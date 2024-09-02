using Microsoft.Extensions.Logging;
using RaftMQ.Logging;
using RaftMQ.Service;
using RaftMQ.Transport;
using RaftMQ.Transport.Model;
using System;
using System.Threading;

namespace RaftMQ.LeaderElection
{
    public class LeaderElectionService(ILogger<LeaderElectionService> logger, IRaftTransport transport) : ILeaderElectionService
    {
        private readonly ILogger<LeaderElectionService> logger = logger;
        private readonly IRaftTransport transport = transport;
        private TimeSpan leaderElectionTimeout;
        private Guid uid;
        private Timer timer;

        public int Term { get; private set; }

        public bool HasVotedInThisTerm { get; private set; }

        public int Votes { get; private set; }

        public void Start(IRaftServiceConfiguration config, Guid uid)
        {
            logger.LogServiceStart(nameof(LeaderElectionService));

            this.uid = uid;

            transport.VoteMessageReceived += Transport_VoteMessageReceived;
            transport.RequestVoteMessageReceived += Transport_RequestVoteMessageReceived;

            leaderElectionTimeout = config.ElectionTimeout;

            timer = new Timer(StartElection, null, leaderElectionTimeout, Timeout.InfiniteTimeSpan);
        }

        private void Transport_RequestVoteMessageReceived(object sender, GenericMessageData data)
        {
            if (data.Term > Term)
            {
                Term = data.Term;
                transport.SendVoteMessage(Term, data.Sender);
            }
            if (!HasVotedInThisTerm)
            {
                
            }
        }

        private void Transport_VoteMessageReceived(object sender, VoteMessageData data)
        {
            if (data.Target == uid)
            {
                Votes++;
            }
        }

        private void StartElection(Object state)
        {
            logger.LogElectionStart(Term, (int)leaderElectionTimeout.TotalMilliseconds);

            Term++;
            HasVotedInThisTerm = true;
            Votes++;

            transport.SendRequestVoteMessage(Term);
        }
    }
}
