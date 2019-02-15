using Art.Domain;
using Art.Web.Areas.Shop.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Art.Web.Areas.Shop.Controllers
{
    public class ProductController : BaseController
    {
        static Dictionary<string, string> SortItems { get; set; }
        static int[] AllArtistIds { get; set; }

        static ProductController()
        {
            SortItems = new Dictionary<string, string>();

            SortItems.Add("quantitysold_desc", "Popularity");
            SortItems.Add("avgstars_desc", "Rating");
            SortItems.Add("price_asc", "Price");

            AllArtistIds = ArtCache.Artists.Keys.Cast<int>().ToArray();

            // the last item maps star ratings to a star image (10, 15, 20, 25, etc)
            Mapper.CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => string.Format("{0:C0}", src.Price)))
                .ForMember(dest => dest.AvgStars, opt => opt.MapFrom(src => ((Math.Round(src.AvgStars * 2) / 2) * 10).ToString()));

            Mapper.CreateMap<Artist, ArtistModel>();
        }


        [HttpGet]
        public ActionResult Products(string sort = "quantitysold_desc", int[] artistids = null, double priceFrom = 0, double priceThru = 500, int page = 1, int pageSize = 9, string q = null, bool layout = true)
        {
            // very first time the page renders we have all artists selected
            if (artistids == null && !Request.IsAjaxRequest())
                artistids = AllArtistIds;

            // no layout and no artists: activate all artists
            if (artistids == null && layout == false)
                artistids = AllArtistIds;

            var model = new ProductsModel { Sort = sort, ArtistIds = artistids, PriceFrom = priceFrom, PriceThru = priceThru, Page = page, PageSize = pageSize, Q = q, SortItems = SortItems };
            CheckForResetEnabled(model);

            // at least one artist needs to be selected
            if (artistids != null)
            {
                Validate(sort, artistids, priceFrom, priceThru, page, pageSize);

                string where = "Price BETWEEN @0 AND @1 AND ArtistId IN (" + artistids.CommaSeparate(a => a) + ")";
                object[] parms = new object[] { priceFrom, priceThru };
                string orderBy = sort.Replace("_", " ");

                if (!string.IsNullOrEmpty(q))
                {
                    where += " AND Title LIKE @2";
                    parms = new object[] { priceFrom, priceThru, Server.UrlDecode(q.Replace("...", "")) + "%" };
                }

                var products = ArtContext.Products.Paged(out model.TotalRows, where: where, orderBy: orderBy, page: page, pageSize: pageSize, parms: parms);

                model.Items = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
                model.Artists = Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistModel>>(ArtCache.Artists.Values);
                model.Items.ForEach(p => p.ArtistLastName = ArtCache.Artists[p.ArtistId].LastName);
            }

            // this is only used when user moves through browser history (using History.js)
            // in that situation the entire page (but without layout) needs to be refreshed. 
            if (!layout && Request.IsAjaxRequest())
            {
                ViewBag.Layout = "No";  // flag to render page without layout
                return View(model);
            }

            // ajax requests return partial view only
            if (Request.IsAjaxRequest())
                return PartialView("_Products", model);

            return View(model);
        }

        // helper
        void Validate(string sort, int[] artist, double priceFrom, double priceThru, int page, int pageSize)
        {
            // validate incoming entries. 
            if (!SortItems.ContainsKey(sort)) throw new ArgumentException("Invalid sort");
            foreach (var a in artist) if (a <= 0) throw new ArgumentException("Invalid artist");
            if (priceFrom < 0 || priceThru > 500) throw new ArgumentException("Invalid price range");
            if (page < 0) throw new ArgumentException("Invalid page");
            if (pageSize < 0) throw new ArgumentException("Invalid page size");
        }

        // if these are non-default values then reset button needs to be turned on
        void CheckForResetEnabled(ProductsModel model)
        {
            if (model.Page != 1 ||
                model.PriceFrom != 0 ||
                model.PriceThru != 500 ||
                (model.ArtistIds != null && model.ArtistIds.Count() != 6) ||
                model.Sort != "quantitysold_desc" ||
                !string.IsNullOrEmpty(model.Q))
            {
                model.Reset = true;
            }
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Search(string term)
        {
            // NOTE: jQuery control requires that parameter be named 'term' and returned values be named 'label'
            var label = new List<string>();
            var items = ArtContext.Query("SELECT TOP 5 Title FROM [Product] WHERE Title Like @0 ORDER BY Title", term + "%");
            foreach (var item in items)
            {
                string l = item.Title.ToString();
                label.Add(l.Ellipsify(20));
            }

            return Json(label, JsonRequestBehavior.AllowGet);
        }

        // Get /product/3

        [HttpGet]
        public ActionResult Product(int id)
        {
            var product = ArtContext.Products.Single(id);

            var model = Mapper.Map<Product, ProductModel>(product);
            model.Artist = Mapper.Map<Artist, ArtistModel>(ArtCache.Artists[product.ArtistId]);

            return View(model);
        }


    }
}
