﻿using RaftMQ.Transport;
using System;
using System.Threading.Tasks;

namespace RaftMQ.Rabbit
{
    public class RabbitTransport : IRaftTransport
    {
        public event IRaftTransport.RequestVoteMessageReceivedHandlerAsync RequestVoteMessageReceivedAsync;
        public event IRaftTransport.VoteMessageReceivedHandler VoteMessageReceived;

        public void SendRequestVoteMessage(int term)
        {
            throw new NotImplementedException();
        }

        public void SendVoteMessage(Guid target)
        {
            throw new NotImplementedException();
        }

        public void SendVoteMessage(int term, Guid target)
        {
            throw new NotImplementedException();
        }

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Rabbit()
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

        public Task SendRequestVoteMessageAsync(int term)
        {
            throw new NotImplementedException();
        }

        public Task SendVoteMessageAsync(int term, Guid target)
        {
            throw new NotImplementedException();
        }



        #endregion IDisposable

    }
}
