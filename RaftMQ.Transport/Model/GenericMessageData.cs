using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ.Transport.Model
{
    public class GenericMessageData
    {
        public Guid Sender { get; set; }

        public int Term { get; set; }
    }
}
