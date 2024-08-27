using RaftMQ.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaftMQ.Service
{
    public interface IRaftServiceConfiguration
    {
        IRaftTransport Transport { get; }
    }
}
