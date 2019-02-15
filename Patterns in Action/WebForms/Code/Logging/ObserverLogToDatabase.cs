
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Code.Logging
{
    
    // Writes log events to a database
    // GoF Design Pattern: Observer
    
    public class ObserverLogToDatabase : ILog
    {
        // actual database insert statements are commented out.
        // you can activate this if you have the proper database 
        // configuration and access privileges.

        public void Log(object sender, LogEventArgs e)
        {
            // example code of entering a log event to database

            string message = "[" + e.Date.ToString() + "] " +
               e.SeverityString + ": " + e.Message;

            // something like
            string sql = "INSERT INTO LOG (message) VALUES('" + message + "')";

            // commented out for now. You need database to store log values. 
            //Db.Update(sql);
        }
    }
}
