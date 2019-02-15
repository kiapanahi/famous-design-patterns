using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace WebForms.Code
{
    
    // represents a collection of viewstate providers.
    
    public class ViewStateProviderCollection : ProviderCollection
    {
        // gets a viewState provider from a list given its name.

        public new ViewStateProviderBase this[string name]
        {
            get { return base[name] as ViewStateProviderBase; }
        }
        
        // adds a viewstate provider to a collection of providers.
        
        public override void Add(ProviderBase provider)
        {
            if (provider != null && provider is ViewStateProviderBase)
            {
                base.Add(provider);
            }
        }
    }
}
