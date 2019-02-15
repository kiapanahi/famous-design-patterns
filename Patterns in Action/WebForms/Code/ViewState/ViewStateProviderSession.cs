using System.Web;
using System.Web.SessionState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebForms.Code
{
    
    // Viewstate provider that is implemented using session objects.
  
    // Gof Design Pattern: Strategy.
  
    public class ViewStateProviderSession : ViewStateProviderBase
    {
        
        // saves view state information for the web page in a session object
        
        public override void SavePageState(string name, object viewState)
        {
            var session = HttpContext.Current.Session;
            session[name] = viewState;
        }
        
        // retrieves viewstate information for the web page from session
        
        public override object LoadPageState(string name)
        {
            var session = HttpContext.Current.Session;
            return session[name];
        }
    }
}
