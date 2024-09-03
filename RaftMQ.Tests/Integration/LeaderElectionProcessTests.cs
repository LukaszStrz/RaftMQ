using Microsoft.Extensions.Logging;
using Moq;
using RaftMQ.LeaderElection;
using RaftMQ.Service;
using RaftMQ.Transport;
using RaftMQ.Transport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RaftMQ.Tests.Integration
{
    public class LeaderElectionProcessTests
    {
        [Fact]
        public async Task LeaderElectionProcess_CastVoteOnCurrentCandidateAsync()
        {
            var uid1 = Guid.NewGuid();
            var uid2 = Guid.NewGuid();

            // Arrange
            var loggerMock = new Mock<ILogger<LeaderElectionService>>();

            var transportMock1 = new Mock<IRaftTransport>();
            var transportMock2 = new Mock<IRaftTransport>();

            transportMock1
                .Setup(m => m.SendRequestVoteMessageAsync(It.IsAny<int>()))
                .Callback(
                    () => transportMock2.Raise(m => m.VoteMessageReceived += null, new object(), new VoteMessageData()
                    {
                        Sender = uid1,
                        Target = uid2,
                        Term = 1
                    }));

            var leaderElectionService1 = new LeaderElectionService(loggerMock.Object, transportMock1.Object);
            var leaderElectionService2 = new LeaderElectionService(loggerMock.Object, transportMock2.Object);

            var configurationMock1 = new Mock<IRaftServiceConfiguration>();
            configurationMock1.SetupGet(m => m.Transport).Returns(transportMock1.Object);
            configurationMock1.SetupGet(m => m.ElectionTimeout).Returns(TimeSpan.FromMilliseconds(10));

            var configurationMock2 = new Mock<IRaftServiceConfiguration>();
            configurationMock2.SetupGet(m => m.Transport).Returns(transportMock2.Object);
            configurationMock2.SetupGet(m => m.ElectionTimeout).Returns(TimeSpan.FromMilliseconds(1000));

            // Act
            var start = new List<Task>()
            {
                Task.Run(() => leaderElectionService1.Start(configurationMock1.Object, uid1)),
                Task.Run(() => leaderElectionService2.Start(configurationMock2.Object, uid2)),
            };

            var stop = new List<Task>()
            {
                Task.Run(() => leaderElectionService1.Stop()),
                Task.Run(() => leaderElectionService2.Stop()),
            };

            await Task.WhenAll(start);
            await Task.Delay(50);
            await Task.WhenAll(stop);

            // Assert
            Assert.Equal(1, leaderElectionService1.Term);
            Assert.Equal(1, leaderElectionService1.Votes);
        }
    }
}
