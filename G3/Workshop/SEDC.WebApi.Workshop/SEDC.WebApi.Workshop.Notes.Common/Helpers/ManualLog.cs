using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.WebApi.Workshop.Notes.Common.Helpers
{
    public static class ManualLog
    {
        public static void Information(string message)
        {
            var fullMessage = $"{DateTime.UtcNow.ToString()} [INF] {message} \n";
            WriteToFile(fullMessage);
        }

        public static void Error(string message)
        {
            var fullMessage = $"{DateTime.UtcNow.ToString()} [ERR] {message} \n";
            WriteToFile(fullMessage);

        }

        public static void Debug(string message)
        {
            var fullMessage = $"{DateTime.UtcNow.ToString()} [DBG] {message} \n";
            WriteToFile(fullMessage);

        }

        public static void Waring(string message)
        {
            var fullMessage = $"{DateTime.UtcNow.ToString()} [WRN] {message} \n";
            WriteToFile(fullMessage);

        }

        private static void WriteToFile(string message)
        {
            File.AppendAllText("logs.txt", message);
        }
    }
}
