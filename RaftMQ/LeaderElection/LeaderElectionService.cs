using Microsoft.Extensions.Logging;
using RaftMQ.Logging;
using RaftMQ.Service;
using RaftMQ.Transport;
using RaftMQ.Transport.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RaftMQ.LeaderElection
{
    public class LeaderElectionService : ILeaderElectionService
    {
        private readonly ILogger<LeaderElectionService> logger;
        private readonly IRaftTransport transport;
        private TimeSpan leaderElectionTimeout;
        private Guid uid;
        private Timer timer;

        public int Term { get; private set; }

        public int Votes { get; private set; }

        public LeaderElectionService(ILogger<LeaderElectionService> logger, IRaftTransport transport)
        {
            this.logger = logger;
            this.transport = transport;

            transport.VoteMessageReceived += Transport_VoteMessageReceived;
            transport.RequestVoteMessageReceivedAsync += Transport_RequestVoteMessageReceivedAsync;
        }

        public void Start(IRaftServiceConfiguration config, Guid uid)
        {
            logger.LogServiceStart(nameof(LeaderElectionService));

            this.uid = uid;

            leaderElectionTimeout = config.ElectionTimeout;

            timer = new Timer(async (o) => await StartElectionAsync());
            UpdateTimer();
        }

        public void Stop()
        {
            timer?.Dispose();
            timer = null;
        }

        private void UpdateTimer()
        {
            timer.Change(leaderElectionTimeout, Timeout.InfiniteTimeSpan);
        }

        private async Task Transport_RequestVoteMessageReceivedAsync(object sender, GenericMessageData data)
        {
            if (data.Term > Term)
            {
                Term = data.Term;
                await transport.SendVoteMessageAsync(Term, data.Sender);
                UpdateTimer();
            }
        }

        private void Transport_VoteMessageReceived(object sender, VoteMessageData data)
        {
            if (data.Target == uid)
            {
                if (data.Term != Term)
                {
                    throw new InvalidOperationException("Received vote for a wrong term");
                }

                Votes++;
            }
        }

        private async Task StartElectionAsync()
        {
            logger.LogElectionStart(Term, (int)leaderElectionTimeout.TotalMilliseconds);

            Term++;
            Votes = 1;

            await transport.SendRequestVoteMessageAsync(Term);
        }

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    timer?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LeaderElectionService()
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

        #endregion IDisposable
    }
}
