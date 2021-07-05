using System;
using System.IO;

namespace DataAccess.LogFile {
    public static class Logger {

        private static string GetLogFilePath() {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private static string GetLogFileName() {
            return "Log.txt";
        }

        private static string GetLogFileAbsolutePath() {
            var logFilePath = GetLogFilePath();
            var logFileName = GetLogFileName();
            return $"{logFilePath}\\{logFileName}";
        }

        private static void CreateLogFile(string logFileAbsolutePath) {

            var isLogFileExists = File.Exists(logFileAbsolutePath);
            if (isLogFileExists) {
                return;
            }

            File.Create(logFileAbsolutePath);

        }

        private static string GetLineHeading(LogType type) {

            switch (type) {
                case LogType.Error:
                    return "[Error] - ";
                case LogType.Warning:
                    return "[Warning] - ";
                default:
                    return "[Info] - ";
            }
        }

        public static void LogError(string msg, Exception ex = null) {
            Log(msg, LogType.Error, ex);
        }

        public static void LogWarning(string msg, Exception ex = null) {
            Log(msg, LogType.Warning, ex);
        }

        public static void LogInfo(string msg) {
            Log(msg, LogType.Info);
        }

        private static void Log(string msg, LogType type, Exception ex = null) {

            var logFileAbsPath = GetLogFileAbsolutePath();
            CreateLogFile(logFileAbsPath);

            using (StreamWriter writer = new StreamWriter(logFileAbsPath)) {

                var lineHeader = GetLineHeading(type);
                var exception = ex == null ? string.Empty : $" - [Exception] - {ex.Message}";
                var logMsg = $"{lineHeader}{msg}{exception}";
                writer.WriteLine(logMsg);

            }
        }
    }
}
