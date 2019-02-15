using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.BusinessRules
{
    
    // enum of operators that can be used in validation rules
    
    public enum ValidationOperator
    {
        Equal, 
        NotEqual, 
        GreaterThan, 
        GreaterThanEqual, 
        LessThan, 
        LessThanEqual
    }
}
