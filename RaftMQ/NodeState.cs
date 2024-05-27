using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ
{
    public enum NodeState
    {
        FOLLOWER,
        CANDIDATE,
        LEADER
    }
}
