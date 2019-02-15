using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace WebForms.Code
{
    
    // defines the methods that viewstate providers should implement.
    
    // Design Pattern: Microsoft's Provider Design Pattern. This pattern is
    // really is a blending of 3 GoF Design Patterns: Strategy, Factory, Singleton.
    // 
    // Strategy Pattern because the client has the option which viewstate provider Strategy to choose.
    // Singleton Pattern because GlobalViewStateSingleton is a Singleton that holds a list of all available viewstate providers.
    // Factory Pattern because viewstate providers are manufactured according to settings in a configuration file (web config).

    public abstract class ViewStateProviderBase : ProviderBase
    {
        // saves any view or control state information for the web page.

        public abstract void SavePageState(string name, object viewState);
        
        // loads any view or control state information for the web page.

        public abstract object LoadPageState(string name);
    }
}
