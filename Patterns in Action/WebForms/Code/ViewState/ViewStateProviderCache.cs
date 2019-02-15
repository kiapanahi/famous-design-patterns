using System;
using System.Collections.Generic;
using System.Text;

using System.Web.Caching;
using System.Web;
using System.Web.SessionState;
using System.Linq;

namespace WebForms.Code
{
    
    // Viewstate provider that is implemented using a cache.
    
    // Gof Design Pattern: Strategy.

    public class ViewStateProviderCache : ViewStateProviderBase
    {
        
        // saves view state information for the web page in cache
        
        public override void SavePageState(string name, object viewState)
        {
            // access cache and session state

            var cache = HttpContext.Current.Cache;
            var session = HttpContext.Current.Session;

            // add to cache

            cache.Add(name, viewState, null, DateTime.Now.AddMinutes(session.Timeout), TimeSpan.Zero, CacheItemPriority.Default, null);
        }

        
        // retrieves viewstate information for the web page from cache.
        
        public override object LoadPageState(string name)
        {
            // returns cached entry

            return HttpContext.Current.Cache[name];
        }
    }
}
