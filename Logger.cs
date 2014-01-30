using System;
using System.Diagnostics;
using System.IO;

namespace msgen
{
    public static class Logger
    {
        private const string LOG_NAME = "Application";

        public enum LogType
        {
            Information,
            Warning,
            Error,
            CriticalError
        }

        private static string getSource()
        {
            return Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        public static void logEvent(Exception ex)
        {
            logEvent(ex.ToString(), LogType.Error, getSource() + ".log");
        }

        public static void logFile(Exception ex)
        {
            logFile(ex.ToString(), LogType.Error, getSource() + ".log");
        }

        public static void logEvent(string message, LogType type, string source)
        {
            EventLogEntryType entryType = EventLogEntryType.Information;
            switch(type)
            {
                case LogType.Information:
                    entryType = EventLogEntryType.Information;
                    break;
                case LogType.Warning:
                    entryType = EventLogEntryType.Warning;
                    break;
                case LogType.Error:
                case LogType.CriticalError:
                    entryType = EventLogEntryType.Error;
                    break;
            }
            try
            {
                if(!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, LOG_NAME);
                EventLog elog = new EventLog(LOG_NAME, Environment.MachineName, source);
                elog.WriteEntry(message, entryType);
            }
            catch(Exception)
            {
                logFile(message, type, source + ".log");
            }
        }

        public static void logFile(string message, LogType type, string file)
        {
            StreamWriter sw = new StreamWriter(file, true);
            try
            {
                sw.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss [") + type.ToString() + "]");
                sw.WriteLine(message);
                sw.WriteLine();
            }
            finally
            {
                if(sw != null)
                    sw.Close();
            }
        }

    }
}
