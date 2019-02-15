using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace WebForms.Code
{
    
    // represents the custom viewstate provider section in web.config.
   
    // GoF Design Patterns: Factory.

    public class ViewStateProviderServiceSection : ConfigurationSection
    {
        
        // gets a collection of viewstate providers from web.config.
        
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        
        // gets or sets the default viewstate provider.
        
        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider", DefaultValue = "ViewStateProviderCache")]
        public string DefaultProvider
        {
            get { return (string)base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }
    }
}
