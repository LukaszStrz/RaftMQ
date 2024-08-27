using Microsoft.Extensions.DependencyInjection;
using RaftMQ.LeaderElection;
using RaftMQ.Service;

namespace RaftMQ
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<IRaftService, RaftService>();
            services.AddScoped<ILeaderElectionService, LeaderElectionService>();
        }
    }
}
