using Microsoft.Extensions.Logging;
using RaftMQ.Logging;
using RaftMQ.Service;
using System;
using System.Threading;

namespace RaftMQ.LeaderElection
{
    public class LeaderElectionService(ILogger<LeaderElectionService> logger) : ILeaderElectionService
    {
        private readonly ILogger<LeaderElectionService> logger = logger;
        private TimeSpan leaderElectionTimeout;

        public int Term { get; private set; } = 0;

        public void Start(IRaftServiceConfiguration config)
        {
            logger.LogServiceStart(nameof(LeaderElectionService));

            leaderElectionTimeout = config.ElectionTimeout;

            var timer = new Timer(StartElection, null, leaderElectionTimeout, Timeout.InfiniteTimeSpan);
        }

        private void StartElection(Object state)
        {
            Term++;
            logger.LogElectionStart(Term, (int)leaderElectionTimeout.TotalMilliseconds);
        }
    }
}
