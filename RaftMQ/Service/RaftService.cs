using Microsoft.Extensions.Logging;
using RaftMQ.LeaderElection;
using RaftMQ.Logging;
using System;

namespace RaftMQ.Service
{
    public class RaftService(ILeaderElectionService leaderElectionService, ILogger<RaftService> logger) : IRaftService
    {
        private bool disposedValue;

        private IRaftServiceConfiguration config;
        private readonly ILeaderElectionService leaderElectionService = leaderElectionService ?? throw new ArgumentNullException(nameof(leaderElectionService));
        private readonly ILogger<RaftService> logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public ServiceState State {  get; private set; } = ServiceState.UNREGISTERED;

        public void Start(IRaftServiceConfiguration config)
        {
            logger.LogServiceStart(nameof(RaftService));

            this.config = config;

            leaderElectionService.Configure(config);
        }

        public void Stop()
        {
            logger.LogServiceStop(nameof(RaftService));
        }

        protected virtual void Dispose(bool disposing)
        {
            Stop();

            if (!disposedValue)
            {
                if (disposing)
                {
                    config.Transport.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RaftService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
