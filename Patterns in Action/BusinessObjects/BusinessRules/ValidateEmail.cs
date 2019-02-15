using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.BusinessRules
{
    
    // email validation rule
    
    public class ValidateEmail : ValidateRegex
    {
        public ValidateEmail(string propertyName) :
            base(propertyName, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        {
            Error = propertyName + " is not a valid email address";
        }

        public ValidateEmail(string propertyName, string errorMessage) :
            this(propertyName)
        {
            Error = errorMessage;
        }
    }
}
