using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Mvc.Code
{
    // Sortable interface. Defines column and order.
    
    public interface ISortable : IEnumerable
    {
        string Sort { get; }
        string Order { get; }
    }

    // generic form of ISortable interface.

    public interface ISortable<T> : ISortable, IEnumerable<T>
    {
        // No members..
    }
}