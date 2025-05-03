using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointFileEditorAddin.Helpers
{
    public static class Logger
    {
        private static readonly string LogFile = "log.txt";

        public static void Log(string message)
        {
            System.IO.File.AppendAllText(LogFile, $"{DateTime.Now}: {message}\n");
        }
    }
}
