using System;

using DZzzz.Net.Logging.Model;

namespace DZzzz.Net.Logging.Interfaces
{
    public interface ILogger
    {
        void Write<T>(string message, LogLevel logLevel = LogLevel.Info, Exception e = null);
    }
}