using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Code
{
    public class PriceRange
    {
        
        // List of price ranges.
        
        static List<PriceRangeItem> list { get; set; }

        
        // Static constructor initializeing price range list.
        
        static PriceRange()
        {
            list = new List<PriceRangeItem>();

            list.Add(new PriceRangeItem(0, 0, 0, "select"));
            list.Add(new PriceRangeItem(1, 0, 50, "$0 - $50"));
            list.Add(new PriceRangeItem(2, 51, 100, "$51 - $100"));
            list.Add(new PriceRangeItem(3, 101, 250, "$101 - $250"));
            list.Add(new PriceRangeItem(4, 251, 1000, "$251 - $1,000"));
            list.Add(new PriceRangeItem(5, 1001, 2000, "$1,001 - $2,000"));
            list.Add(new PriceRangeItem(6, 2001, 10000, "$2,001 - $10,000"));
        }

        public List<PriceRangeItem> GetList()
        {
            return list;
        }
    }

    public class PriceRangeItem
    {
        public PriceRangeItem(int rangeId, double rangeFrom, double rangeThru, string rangeText)
        {
            RangeId = rangeId;
            RangeFrom = rangeFrom;
            RangeThru = rangeThru;
            RangeText = rangeText;
        }

        public int RangeId { get; private set; }
        public double RangeFrom { get; private set; }
        public double RangeThru { get; private set; }
        public string RangeText { get; private set; }
    }
}