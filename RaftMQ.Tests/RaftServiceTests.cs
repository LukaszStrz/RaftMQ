using Microsoft.Extensions.Logging;
using Moq;
using RaftMQ.LeaderElection;
using RaftMQ.Service;

namespace RaftMQ.Tests
{
    public class RaftServiceTests
    {
        //[Fact]
        //public void Start_LogsMessage()
        //{
        //    // Arrange
        //    var loggerMock = new Mock<ILogger<RaftService>>();
        //    var leaderElectionMock = new Mock<ILeaderElectionService>();
        //    var raftServiceConfigurationMock = new Mock<IRaftServiceConfiguration>();

        //    IRaftService svc = new RaftService(leaderElectionMock.Object, loggerMock.Object);

        //    // Act
        //    svc.Start(raftServiceConfigurationMock.Object);

        //    // Assert
        //    loggerMock.VerifyLog(logger => logger.LogInformation($"RMQ-I0001 Starting service: {nameof(RaftService)}"));
        //}
    }
}