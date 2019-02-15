using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Code.Logging
{
    // singleton logger class through which all log events are processed.
    
    // ** Design Patterns: Singleton, Observer.
    
    public sealed class Logger
    {
        // delegate event handler that hooks up requests.

        public delegate void LogEventHandler(object sender, LogEventArgs e);

        // the Log event.

        public event LogEventHandler Log;

        #region The Singleton definition

        
        // the one and only Singleton Logger instance. 
        
        private static readonly Logger instance = new Logger();

        // private constructor. Initializes default severity to "Error".
        
        private Logger()
        {
            // default severity is Error level

            Severity = LogSeverity.Error;
        }

        
        // gets the instance of the singleton logger object
        
        public static Logger Instance
        {
            get { return instance; }
        }

        #endregion

        private LogSeverity severity;

        // these booleans are used to improve performance.

        private bool isDebug;
        private bool isInfo;
        private bool isWarning;
        private bool isError;
        private bool isFatal;

        
        // gets and sets the severity level of logging activity.
        
        public LogSeverity Severity
        {
            get { return severity; }
            set
            {
                severity = value;

                // set booleans to help improve performance

                int sev = (int)severity;

                isDebug = ((int)LogSeverity.Debug) >= sev ? true : false;
                isInfo = ((int)LogSeverity.Info) >= sev ? true : false;
                isWarning = ((int)LogSeverity.Warning) >= sev ? true : false;
                isError = ((int)LogSeverity.Error) >= sev ? true : false;
                isFatal = ((int)LogSeverity.Fatal) >= sev ? true : false;
            }
        }

       
        // log a message when severity level is "Debug" or higher.
        
        public void Debug(string message)
        {
            if (isDebug)
                Debug(message, null);
        }

        // log a message when severity level is "Debug" or higher.
        
        public void Debug(string message, Exception exception)
        {
            if (isDebug)
                OnLog(new LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now));
        }

        
        // log a message when severity level is "Info" or higher.

        public void Info(string message)
        {
            if (isInfo)
                Info(message, null);
        }

       
        // log a message when severity level is "Info" or higher.

        public void Info(string message, Exception exception)
        {
            if (isInfo)
                OnLog(new LogEventArgs(LogSeverity.Info, message, exception, DateTime.Now));
        }

        // log a message when severity level is "Warning" or higher.

        public void Warning(string message)
        {
            if (isWarning)
                Warning(message, null);
        }

        
        // log a message when severity level is "Warning" or higher.

        public void Warning(string message, Exception exception)
        {
            if (isWarning)
                OnLog(new LogEventArgs(LogSeverity.Warning, message, exception, DateTime.Now));
        }

        
        // log a message when severity level is "Error" or higher.
        
        public void Error(string message)
        {
            if (isError)
                Error(message, null);
        }

        // log a message when severity level is "Error" or higher.
        
        public void Error(string message, Exception exception)
        {
            if (isError)
                OnLog(new LogEventArgs(LogSeverity.Error, message, exception, DateTime.Now));
        }

        
        // log a message when severity level is "Fatal"
        
        public void Fatal(string message)
        {
            if (isFatal)
                Fatal(message, null);
        }

        // log a message when severity level is "Fatal"

        public void Fatal(string message, Exception exception)
        {
            if (isFatal)
                OnLog(new LogEventArgs(LogSeverity.Fatal, message, exception, DateTime.Now));
        }

        // invokes the Log event

        public void OnLog(LogEventArgs e)
        {
            if (Log != null)
            {
                Log(this, e);
            }
        }

        // attach a listening observer logging device to logger
        
        public void Attach(ILog observer)
        {
            Log += observer.Log;
        }

        
        // detach a listening observer logging device from logger

        public void Detach(ILog observer)
        {
            Log -= observer.Log;
        }
    }
}
