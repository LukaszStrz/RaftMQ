﻿using Microsoft.Extensions.Logging;
using RaftMQ.LeaderElection;
using System;

namespace RaftMQ.Service
{
    internal class RaftService : IRaftService
    {
        private bool disposedValue;

        private IRaftServiceConfiguration config;
        private readonly ILeaderElectionService leaderElectionService;
        private readonly ILogger<RaftService> logger;

        public RaftService(ILeaderElectionService leaderElectionService, ILogger<RaftService> logger)
        {
            this.leaderElectionService = leaderElectionService ?? throw new ArgumentNullException(nameof(leaderElectionService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Start(IRaftServiceConfiguration config)
        {
            this.config = config;

            leaderElectionService.Configure(config);

            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
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
