using System;
using DZzzz.Net.Core.Logging;

namespace DZzzz.Net.Core.Interfaces
{
    public interface ILogger
    {
        void Write<T>(string message, LogLevel logLevel = LogLevel.Info, Exception e = null);
    }
}