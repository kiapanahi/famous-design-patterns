using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace WebForms.Code
{
    
    // GlobalViewStateSingleton maintains a list of viewstates in a 
    // globally accessible hashtable. this is a Singleton helper class to the 
    // ViewStateProviderGlobal class.
    
    // Gof Design Pattern: Singleton.

    public class GlobalViewStateSingleton
    {
        #region The Singleton definition

        // this is the single instance of this class
        private static readonly GlobalViewStateSingleton _instance = new GlobalViewStateSingleton();

        // private constructor for GlobalViewStateSingleton.
        // prevents others from instantiating additional instances.
        
        private GlobalViewStateSingleton()
        {
            ViewStates = new Dictionary<string, object>();
        }

        
        // gets the one instance of the GlobalViewStateSingleton class
        
        public static GlobalViewStateSingleton Instance
        {
            get { return _instance; }
        }

        #endregion

        
        // gets a list of ViewStates.
        
        public Dictionary<string, object> ViewStates { get; private set; }
    }
}
