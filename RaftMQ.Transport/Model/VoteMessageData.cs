using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ.Transport.Model
{
    public class VoteMessageData : GenericMessageData
    {
        public Guid Target { get; set; }
    }
}
