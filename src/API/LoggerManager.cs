using Application.Interfaces;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace API
{
    public class LoggerManager : ILoggerManager
    {
        private static Serilog.ILogger logger = Log.Logger;

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogInfo(string message)
        {
            logger.Information(message);
        }
        public void LogWarn(string message)
        {
            logger.Warning(message);
        }
    }
}
