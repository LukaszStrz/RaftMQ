using Microsoft.Extensions.Logging;
using System;

namespace RaftMQ.Logging
{
    public static partial class LogMessages
    {
        [LoggerMessage(
            Message = "Starting service: {serviceName}",
            Level = LogLevel.Information)]
        public static partial void LogServiceStart(
            this ILogger logger,
            string serviceName);

        [LoggerMessage(
            Message = "Stopping service: {serviceName}",
            Level = LogLevel.Information)]
        public static partial void LogServiceStop(
            this ILogger logger,
            string serviceName);

        [LoggerMessage(
            Message = "Starting election: Term={term}; Timeout={timeout}",
            Level = LogLevel.Debug)]
        public static partial void LogElectionStart(
            this ILogger logger,
            int term,
            int timeout);
    }
}
