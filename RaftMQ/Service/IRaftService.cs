using System;

namespace RaftMQ.Service
{
    public interface IRaftService : IDisposable
    {
        void Start(IRaftServiceConfiguration config);

        ServiceState State { get; }
    }
}
