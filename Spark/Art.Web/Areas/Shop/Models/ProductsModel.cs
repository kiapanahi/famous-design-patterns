using Art.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Shop.Models
{
    public class ProductsModel : PagedList<ProductModel>
    {
        // search string
        public string Q { get; set; }

        public string Sort { get; set; }
        public double PriceFrom { get; set; }
        public double PriceThru { get; set; }
        public int[] ArtistIds { get; set; }
        public bool Reset { get; set; }

        public IDictionary<string, string> SortItems { get; set; }

        public IEnumerable<ArtistModel> Artists { get; set; }
    }
}