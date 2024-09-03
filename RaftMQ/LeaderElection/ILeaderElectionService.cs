using RaftMQ.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ.LeaderElection
{
    public interface ILeaderElectionService : IDisposable
    {
        void Start(IRaftServiceConfiguration config, Guid uid);

        void Stop();

        int Term { get; }

        int Votes { get; }
    }
}
