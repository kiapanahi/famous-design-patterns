using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Code.Logging
{
    
    // sends log events via email.
    // ** Design Pattern: Observer

    public class ObserverLogToEmail : ILog
    {
        private string from;
        private string to;
        private string subject;
        private string body;
        private SmtpClient smtpClient;

        public ObserverLogToEmail(string from, string to, string subject, string body, SmtpClient smtpClient)
		{
            this.from = from;
            this.to = to;
            this.subject = subject;
            this.body = body;
            
            this.smtpClient = smtpClient;
		}

        #region ILog Members

        
        // sends a log request via email.
        // actual email 'Send' calls are commented out.
        // uncomment if you have the proper email privileges.
        public void Log(object sender, LogEventArgs e)
        {
            string message = "[" + e.Date.ToString() + "] " +
               e.SeverityString + ": " + e.Message;

            // commented out for now. you need privileges to send email.
            // _smtpClient.Send(from, to, subject, body);
        }

        #endregion
    }
}
