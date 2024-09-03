using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaftMQ.Rabbit;
using RaftMQ.Service;
using RaftMQ.Transport;

namespace RaftMQ.ExampleConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("STARTING\n");
            var rand = new Random();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddRaftMQ<RabbitTransport>();
                }).Build();

            var raft = host.Services.GetRequiredService<IRaftService>();
            var transport = host.Services.GetRequiredService<IRaftTransport>();

            var config = new RaftServiceConfiguration(transport, TimeSpan.FromSeconds(rand.Next(3, 6)));
            raft.Start(config);

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
