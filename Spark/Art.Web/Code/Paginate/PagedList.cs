using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web
{
    // base class to view models that support pagination

    public abstract class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRows;  // not a property because it is used as an 'out' parameter

        public PagedList()
        {
            Items = Enumerable.Empty<T>();
            TotalRows = 0;
            Page = 1;
            PageSize = 20;
        }

        public PagedList(IEnumerable<T> items, int totalRows = 0, int page = 1, int pageSize = 20)
        {
            Items = items;
            TotalRows = totalRows;
            Page = page;
            PageSize = pageSize;
        }

        // helpers and computed items

        public int FirstRow { get { return Math.Min(Math.Max(1, ((Page - 1) * PageSize) + 1), TotalRows); } }
        public int LastRow { get { return Math.Min(FirstRow + PageSize - 1, TotalRows); } }
        public string Range { get { return FirstRow.ToString() + " - " + LastRow.ToString(); } }
        public int TotalPages { get { return (int)Math.Ceiling(((double)TotalRows) / PageSize); } }
        public bool HasPreviousPage { get { return Page > 1; } }
        public bool HasNextPage { get { return Page < TotalPages; } }
    }
}