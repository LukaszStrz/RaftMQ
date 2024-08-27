using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using RaftMQ.LeaderElection;
using RaftMQ.Service;
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
            var leaderElectionService = new LeaderElectionService(loggerMock.Object);

            var configurationMock = new Mock<IRaftServiceConfiguration>();
            configurationMock.SetupGet(m => m.ElectionTimeout).Returns(TimeSpan.FromMilliseconds(10));

            // Act
            leaderElectionService.Start(configurationMock.Object);
            await Task.Delay(50);

            // Assert
            Assert.Equal(1, leaderElectionService.Term);
        }
    }
}
