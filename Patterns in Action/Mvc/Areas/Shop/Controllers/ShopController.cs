using ActionService;
using AutoMapper;
using BusinessObjects;
using Mvc.Areas.Shop.Models;
using Mvc.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Areas.Shop.Controllers
{
    public class ShopController : Controller
    {
        IService service { get; set; }

        // static constructor. establishes Automapper object maps

        static ShopController()
        {
            Mapper.CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => string.Format("{0:C}", src.UnitPrice)));
            Mapper.CreateMap<ProductModel, Product>();
        }

        // default constructor

        public ShopController() : this(new Service()) { }

        // overloaded 'injectable' constructor
        // ** Constructor Dependency Injection (DI).

        public ShopController(IService service) { this.service = service; }

        // shopper page

        [HttpGet]
        public ActionResult Shopping()
        {
            ViewBag.Crumbs = new List<BreadCrumb>();
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "home", Url = "/" });
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "shopping" });

            ViewBag.Menu = "shopping";

            return View();
        }

        // products page

        public ActionResult Products(int categoryId = 1, string sort = "unitprice", string order = "asc")
        {
            ViewBag.Crumbs = new List<BreadCrumb>();
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "home", Url = "/" });
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "shopping", Url = "/shopping" });
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "products" });

            ViewBag.Menu = "products";

            var model = new ProductsModel();
            model.Categories = new SelectList(service.GetCategories(), "CategoryId", "CategoryName", categoryId);

            var products = service.GetProductsByCategory(categoryId, sort + " " + order);
            var productModels = Mapper.Map<List<Product>, List<ProductModel>>(products);
            model.Products = new SortedList<ProductModel>(productModels, sort, order);

            return View(model);
        }

        // product details page

        [HttpGet]
        public ActionResult Product(int id)
        {
            var product = service.GetProduct(id);
            var model = Mapper.Map<Product, ProductModel>(product);
            model.CategoryName = product.Category.CategoryName;

            ViewBag.Crumbs = new List<BreadCrumb>();
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "home", Url = "/" });
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "shopping", Url = "/shopping" });
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "products", Url = "/products" });
            ViewBag.Crumbs.Add(new BreadCrumb { Title = model.ProductName });

            ViewBag.Menu = "products";

            return View(model);
        }

        // searches for products given a name and/or price range.
        
        public ActionResult Search(string productName = "", string ranges = "0", string sort = "unitprice", string order = "asc")
        {
            ViewBag.Crumbs = new List<BreadCrumb>();
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "home", Url = "/" });
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "shopping", Url = "/shopping" });
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "search" });

            ViewBag.Menu = "search";

            var model = new SearchModel { ProductName = productName };
            model.PriceRanges = new SelectList(PriceRange.List, "RangeId", "RangeText", ranges);

            double priceFrom = PriceRange.List[int.Parse(ranges)].RangeFrom;
            double priceThru = PriceRange.List[int.Parse(ranges)].RangeThru;
            if (priceThru == 0) priceThru = 5000; // no range was selected
            var products = service.SearchProducts(productName, priceFrom, priceThru, sort + " " + order);

            var list = Mapper.Map<List<Product>, List<ProductModel>>(products);
            model.Products = new SortedList<ProductModel>(list, sort, order);

            return View(model);
        }

        #region private helper classes

        static class PriceRange
        {
            public static List<PriceRangeItem> List { get; private set; }

            // creates list of price ranges

            static PriceRange()
            {
                List = new List<PriceRangeItem>();

                List.Add(new PriceRangeItem(0, 0, 0, "select"));
                List.Add(new PriceRangeItem(1, 0, 50, "$0 - $50"));
                List.Add(new PriceRangeItem(2, 51, 100, "$51 - $100"));
                List.Add(new PriceRangeItem(3, 101, 250, "$101 - $250"));
                List.Add(new PriceRangeItem(4, 251, 1000, "$251 - $1,000"));
                List.Add(new PriceRangeItem(5, 1001, 2000, "$1,001 - $2,000"));
                List.Add(new PriceRangeItem(6, 2001, 10000, "$2,001 - $10,000"));
            }
        }

        // one item in a price range list

        class PriceRangeItem
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

        #endregion
    }
}
