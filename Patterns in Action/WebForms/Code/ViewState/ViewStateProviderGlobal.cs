using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WebForms.Code
{
    
    // Viewstate provider that is implemented using a global singleton hashtable.
    
    // Gof Design Pattern: Strategy.

    public class ViewStateProviderGlobal : ViewStateProviderBase
    {
        
        // saves view state information for the web page in a global variable
        
        public override void SavePageState(string name, object viewState)
        {
            GlobalViewStateSingleton.Instance.ViewStates.Add(name,viewState);
        }

        // retrieves viewstate information for the web page from global variable
        
        public override object LoadPageState(string name)
        {
            return GlobalViewStateSingleton.Instance.ViewStates[name];
        }
    }
}
