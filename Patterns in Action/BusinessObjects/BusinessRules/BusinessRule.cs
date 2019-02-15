using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.BusinessRules
{
    
    // abstract base class for business rules. 
    // maintains property name to which rule applies and validation error message
    
    public abstract class BusinessRule
    {
        public string Property { get; set; }
        public string Error { get; set; }

        public BusinessRule(string property)
        {
            Property = property;
            Error = property + " is not valid";
        }

        public BusinessRule(string property, string error)
            : this(property)
        {
            Error = error;
        }

        // validation method. To be implemented in derived classes

        public abstract bool Validate(BusinessObject businessObject);

        // gets value for given business object's property using reflection

        protected object GetPropertyValue(BusinessObject businessObject)
        {
            // note: reflection is relatively slow
            return businessObject.GetType().GetProperty(Property).GetValue(businessObject, null);
        }
    }
}

