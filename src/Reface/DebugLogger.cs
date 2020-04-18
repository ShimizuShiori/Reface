using System;
using DD = System.Diagnostics.Debug;

namespace Reface
{
    public class DebugLogger
    {
        private static LoggerLevels enableLevels = LoggerLevels.All;

        public static void SetEnableLevels(LoggerLevels levels)
        {
            DebugLogger.enableLevels = levels;
        }

        private static void WriteLine(LoggerLevels level, string message)
        {
            DD.WriteLineIf((enableLevels & level) == level, $"{DateTime.Now.ToString("HH:mm:ss.fff")} [{level.ToString()}] - {message}");
        }

        public static void Debug(string message)
        {
            DebugLogger.WriteLine(LoggerLevels.Debug, message);
        }

        public static void Info(string message)
        {
            DebugLogger.WriteLine(LoggerLevels.Info, message);
        }

        public static void Warning(string message)
        {
            DebugLogger.WriteLine(LoggerLevels.Warning, message);
        }

        public static void Error(string message)
        {
            DebugLogger.WriteLine(LoggerLevels.Error, message);
        }
    }
}
