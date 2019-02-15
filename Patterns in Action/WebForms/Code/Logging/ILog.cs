using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Code.Logging
{
    
    // defines a single method to write requested log events to an output device
    
    public interface ILog
    {
        // write a log request to a given output device

        void Log(object sender, LogEventArgs e);
    }
}
