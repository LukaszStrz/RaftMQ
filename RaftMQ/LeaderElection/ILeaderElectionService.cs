using RaftMQ.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ.LeaderElection
{
    internal interface ILeaderElectionService
    {
        void Configure(IRaftServiceConfiguration config);
    }
}
