
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Code.Logging
{
    
    // writes log events to the diagnostic trace
    // GoF Design Pattern: Observer

    public class ObserverLogToConsole : ILog
    {
        public void Log(object sender, LogEventArgs e)
        {
            // example code of entering a log event to output console

            string message = "[" + e.Date.ToString() + "] " +
                e.SeverityString + ": " + e.Message;

            // writes message to debug output window

            System.Diagnostics.Debugger.Log(0, null,  message + "\r\n\r\n"); 
        }
    }
}
