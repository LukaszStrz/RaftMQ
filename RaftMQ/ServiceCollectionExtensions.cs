using Microsoft.Extensions.DependencyInjection;
using RaftMQ.LeaderElection;
using RaftMQ.Service;
using RaftMQ.Transport;
using System;

namespace RaftMQ
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRabbitMQ<TTransport>(this IServiceCollection services) where TTransport : class, IRaftTransport
        {
            services.AddSingleton<ILeaderElectionService, LeaderElectionService>();
            services.AddSingleton<IRaftService, RaftService>();
            services.AddSingleton<IRaftTransport, TTransport>();
        }
    }
}
