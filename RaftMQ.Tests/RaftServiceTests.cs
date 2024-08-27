using Microsoft.Extensions.Logging;
using Moq;
using RaftMQ.LeaderElection;
using RaftMQ.Service;

namespace RaftMQ.Tests
{
    public class RaftServiceTests
    {
        [Fact]
        public void RaftService_Start_ConfiguresLeaderElection()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RaftService>>();
            var leaderElectionServiceMock = new Mock<ILeaderElectionService>();
            var raftServiceConfigurationMock = new Mock<IRaftServiceConfiguration>();
            var raftService = new RaftService(leaderElectionServiceMock.Object, loggerMock.Object);

            // Act
            raftService.Start(raftServiceConfigurationMock.Object);

            // Assert
            leaderElectionServiceMock.Verify(m => m.Start(It.IsAny<IRaftServiceConfiguration>()), Times.Once());
        }
    }
}