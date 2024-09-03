using RaftMQ.Transport;
using System;

namespace RaftMQ.Service
{
    public class RaftServiceConfiguration(IRaftTransport transport, TimeSpan electionTimeout) : IRaftServiceConfiguration
    {
        public IRaftTransport Transport { get; set; } = transport;

        public TimeSpan ElectionTimeout { get; set; } = electionTimeout;
    }
}
