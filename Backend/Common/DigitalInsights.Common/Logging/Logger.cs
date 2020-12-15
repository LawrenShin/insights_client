using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DigitalInsights.Common.Logging
{
    public static class Logger
    {
        const string LOG_FORMAT = "{0:dd.MM.yyyy HH:mm:ss} {1}: {2}";

        static bool initialized = false;
        static string moduleName = string.Empty;
        static string logFormat = LOG_FORMAT;
        public static void Init(string name)
        {
            Init(name, LOG_FORMAT);
        }

        public static void Init(string name, string format)
        {
            moduleName = name;
            logFormat = format;
            initialized = true;
        }

        public  static void Log(string message)
        {
            if (!initialized)
            {
                throw new InvalidOperationException("Logger is not initialized with module name!");
            }
            var line = string.Format(logFormat, DateTime.Now, moduleName, message);
            Trace.WriteLine(line);
            LambdaLogger.Log(line);
        }
    }
}
