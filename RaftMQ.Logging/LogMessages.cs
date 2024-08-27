using Microsoft.Extensions.Logging;
using System;

namespace RaftMQ.Logging
{
    public static partial class LogMessages
    {
        [LoggerMessage(
            Message = "RMQ-I0001 Starting service: {serviceName}",
            Level = LogLevel.Information)]
        public static partial void LogServiceStart(
            this ILogger logger,
            string serviceName);

        [LoggerMessage(
            Message = "RMQ-I0001 Stopping service: {serviceName}",
            Level = LogLevel.Information)]
        public static partial void LogServiceStop(
            this ILogger logger,
            string serviceName);
    }
}
