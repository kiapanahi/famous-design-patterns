using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.BusinessRules
{
    
    // validates a range (min and max) for a given data type
    
    public class ValidateRange : BusinessRule
    {
        ValidationDataType DataType { get; set; }
        ValidationOperator Operator { get; set; }

        object Min { get; set; }
        object Max { get; set; }

        public ValidateRange(string propertyName, object min, object max,
            ValidationOperator @operator, ValidationDataType dataType)
            : base(propertyName)
        {
            Min = min;
            Max = max;

            Operator = @operator;
            DataType = dataType;

            Error = propertyName + " must be between " + Min + " and " + Max;
        }

        public ValidateRange(string propertyName, string errorMessage, object min, object max,
            ValidationOperator @operator, ValidationDataType dataType)
            : this (propertyName,  min,  max,@operator, dataType)
        {
            Error = errorMessage;
        }

        public override bool Validate(BusinessObject businessObject)
        {
            try
            {
                string value = GetPropertyValue(businessObject).ToString();

                switch (DataType)
                {
                    case ValidationDataType.Integer:

                        int imin = int.Parse(Min.ToString());
                        int imax = int.Parse(Max.ToString());
                        int ival = int.Parse(value);

                        return (ival >= imin && ival <= imax);

                    case ValidationDataType.Double:
                        double dmin = double.Parse(Min.ToString());
                        double dmax = double.Parse(Max.ToString());
                        double dval = double.Parse(value);

                        return (dval >= dmin && dval <= dmax);

                    case ValidationDataType.Decimal:
                        decimal cmin = decimal.Parse(Min.ToString());
                        decimal cmax = decimal.Parse(Max.ToString());
                        decimal cval = decimal.Parse(value);

                        return (cval >= cmin && cval <= cmax);

                    case ValidationDataType.Date:
                        DateTime tmin = DateTime.Parse(Min.ToString());
                        DateTime tmax = DateTime.Parse(Max.ToString());
                        DateTime tval = DateTime.Parse(value);

                        return (tval >= tmin && tval <= tmax);

                    case ValidationDataType.String:

                        string smin = Min.ToString();
                        string smax = Max.ToString();

                        int result1 = string.Compare(smin, value);
                        int result2 = string.Compare(value, smax);

                        return result1 >= 0 && result2 <= 0;
                }
                return false;
            }
            catch { return false; }
        }
    }
}
