using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.BusinessRules
{
    
    // IP Address validation rule
    
    public class ValidateIPAddress : ValidateRegex
    {
        // Match IP Address
        public ValidateIPAddress(string propertyName) :
            base(propertyName, @"^([0-2]?[0-5]?[0-5]\.){3}[0-2]?[0-5]?[0-5]$")
        {
            Error = propertyName + " is not a valid IP Address";
        }

        public ValidateIPAddress(string propertyName, string errorMessage) :
            this(propertyName)
        {
            Error = errorMessage;
        }
    }
}
