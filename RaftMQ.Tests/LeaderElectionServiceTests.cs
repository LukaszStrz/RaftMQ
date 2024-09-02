using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using RaftMQ.LeaderElection;
using RaftMQ.Service;
using RaftMQ.Transport;
using RaftMQ.Transport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaftMQ.Tests
{
    public class LeaderElectionServiceTests
    {
        [Fact]
        public async Task LeaderElectionService_BecomesCandidateAfterElectionTimeout()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LeaderElectionService>>();
            var transportMock = new Mock<IRaftTransport>();
            var leaderElectionService = new LeaderElectionService(loggerMock.Object, transportMock.Object);

            var configurationMock = new Mock<IRaftServiceConfiguration>();
            configurationMock.SetupGet(m => m.ElectionTimeout).Returns(TimeSpan.FromMilliseconds(10));

            // Act
            leaderElectionService.Start(configurationMock.Object, Guid.NewGuid());
            await Task.Delay(50);

            // Assert
            Assert.Equal(1, leaderElectionService.Term);
            Assert.Equal(1, leaderElectionService.Votes);
            Assert.True(leaderElectionService.HasVotedInThisTerm);
            transportMock.Verify(m => m.SendRequestVoteMessage(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task LeaderElectionService_ReceivesVote()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LeaderElectionService>>();
            var transportMock = new Mock<IRaftTransport>();
            var leaderElectionService = new LeaderElectionService(loggerMock.Object, transportMock.Object);

            var configurationMock = new Mock<IRaftServiceConfiguration>();
            configurationMock.SetupGet(m => m.ElectionTimeout).Returns(TimeSpan.FromMilliseconds(10));

            var uid = Guid.NewGuid();
            leaderElectionService.Start(configurationMock.Object, uid);
            await Task.Delay(50);

            // Act
            transportMock.Raise(m => m.VoteMessageReceived += null, new object(), new VoteMessageData()
            {
                Sender = Guid.NewGuid(),
                Target = uid,
                Term = 1
            });

            // Assert
            Assert.Equal(2, leaderElectionService.Votes);
        }

        [Fact]
        public async Task LeaderElectionService_ReceivesVoteToOtherTarget()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LeaderElectionService>>();
            var transportMock = new Mock<IRaftTransport>();
            var leaderElectionService = new LeaderElectionService(loggerMock.Object, transportMock.Object);

            var configurationMock = new Mock<IRaftServiceConfiguration>();
            configurationMock.SetupGet(m => m.ElectionTimeout).Returns(TimeSpan.FromMilliseconds(10));

            var uid = Guid.NewGuid();
            leaderElectionService.Start(configurationMock.Object, uid);
            await Task.Delay(50);

            // Act
            transportMock.Raise(m => m.VoteMessageReceived += null, new object(), new VoteMessageData()
            {
                Sender = Guid.NewGuid(),
                Target = Guid.NewGuid(),
                Term = 1
            });

            // Assert
            Assert.Equal(1, leaderElectionService.Votes);
        }

        [Fact]
        public async Task LeaderElectionService_ReceivesVoteBeforeBecomingCandidate()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<LeaderElectionService>>();
            var transportMock = new Mock<IRaftTransport>();
            var leaderElectionService = new LeaderElectionService(loggerMock.Object, transportMock.Object);

            var configurationMock = new Mock<IRaftServiceConfiguration>();
            configurationMock.SetupGet(m => m.ElectionTimeout).Returns(TimeSpan.FromMinutes(10));

            var uid = Guid.NewGuid();
            leaderElectionService.Start(configurationMock.Object, uid);
            await Task.Delay(50);

            // Act
            transportMock.Raise(m => m.VoteMessageReceived += null, new object(), new VoteMessageData()
            {
                Sender = Guid.NewGuid(),
                Target = Guid.NewGuid(),
                Term = 1
            });

            // Assert
            Assert.Equal(0, leaderElectionService.Votes);
        }
    }
}
