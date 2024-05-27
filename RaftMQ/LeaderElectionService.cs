using System;
using System.Timers;

namespace RaftMQ
{
    public sealed class LeaderElectionService : ConsensusService
    {
        private readonly TimeSpan electionTimeout;
        private readonly Timer electionTimer;
        private readonly Node node;

        private bool disposedValue;

        public LeaderElectionService(int electionTimeout, Node node)
        {
            this.node = node;
            this.electionTimeout = new TimeSpan(0, 0, 0, 0,
                                                milliseconds: electionTimeout);
            this.electionTimer = new Timer(100)
            {
                AutoReset = true
            };
            electionTimer.Elapsed += OnElectionTimerElapsed;
        }

        public void Start()
        {
            electionTimer.Start();
        }

        private void OnElectionTimerElapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    electionTimer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }

            base.Dispose(disposing);
        }
    }
}
