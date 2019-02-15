using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Code
{
    
    // Sortable list. supports sorting of lists on a page

    public class SortedList<T> : ISortable<T>
    {
        // the sorted list.
        
        public List<T> List { get; private set; }

        public string Sort { get; private set; }
        public string Order { get; private set; } 

        public SortedList(List<T> list, string sort = null, string order = null)
        {
            List = list;
            Sort = sort;
            Order = order;
        }

        #region IEnumerable Members

        public System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}