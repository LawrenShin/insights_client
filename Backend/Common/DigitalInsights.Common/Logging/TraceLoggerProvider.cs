using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace DigitalInsights.Common.Logging
{
    public class TraceLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger();
        }

        public void Dispose()
        {

        }

        private class DbLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                Trace.WriteLine(formatter(state, exception));
                Logger.Log(formatter(state, exception));
            }

            IDisposable ILogger.BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}