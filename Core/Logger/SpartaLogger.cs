﻿using Microsoft.Extensions.DependencyInjection;
using Sparta.Core.Models;
using System.Diagnostics;

namespace Sparta.Core.Logger
{
    public class SpartaLogger(IServiceProvider serviceProvider)
    {
        public void LogCritical(Exception ex) => Log($"{ex.Message}\n{ex.StackTrace}", ex.Message, LogSeverity.Critical);

        public void LogException(Exception ex) => Log($"{ex.Message}\n{ex.StackTrace}", ex.Message, LogSeverity.Exception);

        public void LogWarning(string message) => Log(message, message, Logger.LogSeverity.Warning);

        public void LogInfo(string message) => Log(message, message, Logger.LogSeverity.Info);

        public void LogVerbose(string message) => Log(message, message, Logger.LogSeverity.Verbose);

        public void LogDebug(string message) => Log(message, message, Logger.LogSeverity.Debug);

        public void LogMessage(string message, LogSeverity severity) => Log(message, message, severity);

        private void Log(string message, string shortMessage, LogSeverity severity)
        {
            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<SpartaDbContext>();

            var trace = new StackTrace();
            var frame = trace.GetFrame(2);

            var shortSource = $"{frame?.GetMethod()?.DeclaringType?.Name}.{frame?.GetMethod()?.Name}";

            var truncatedMessage = shortMessage.Length > 30 ? $"{new string(shortMessage.Take(27).ToArray())}..." : shortMessage;
            var truncatedSource = shortSource.Length > 30 ? $"{new string(shortSource.Take(27).ToArray())}..." : shortSource;

            context.LgLogMessages.Add(new LgLogMessage
            {
                Message = message,
                ShortMessage = truncatedMessage,
                Source = $"{frame?.GetMethod()?.DeclaringType?.FullName}.{frame?.GetMethod()?.Name}",
                ShortSource = truncatedSource,
                Time = DateTimeOffset.Now,
                Severity = (int)severity
            });
            context.SaveChanges();
        }
    }
}
