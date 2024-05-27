using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ
{
    public class Node
    {
        public NodeState State { get; set; } = NodeState.FOLLOWER;

        public int Term { get; set; }

        public Node()
        {

        }
    }
}
