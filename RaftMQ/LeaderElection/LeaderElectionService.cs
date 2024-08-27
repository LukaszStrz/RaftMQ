using Microsoft.Extensions.Logging;
using RaftMQ.Service;
using System;

namespace RaftMQ.LeaderElection
{
    internal class LeaderElectionService(ILogger<LeaderElectionService> logger) : ILeaderElectionService
    {
        private readonly ILogger<LeaderElectionService> logger = logger;

        public void Configure(IRaftServiceConfiguration config)
        {
            throw new NotImplementedException();
        }
    }
}
