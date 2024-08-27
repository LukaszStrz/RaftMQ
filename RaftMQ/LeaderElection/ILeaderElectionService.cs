﻿using RaftMQ.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ.LeaderElection
{
    public interface ILeaderElectionService
    {
        void Start(IRaftServiceConfiguration config);

        int Term { get; }
    }
}
