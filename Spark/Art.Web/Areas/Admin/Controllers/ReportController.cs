using Art.Domain;
using Art.Web.Areas.Admin.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : BaseController
    {
        static Dictionary<string, string> ProductSortItems { get; set; }
        static Dictionary<string, string> UserSortItems { get; set; }
        static Dictionary<string, string> OrderSortItems { get; set; }

        static ReportController()
        {
            ProductSortItems = new Dictionary<string, string>();
            ProductSortItems.Add("quantitysold_desc", "# Sold");
            ProductSortItems.Add("price_desc", "Price");
            ProductSortItems.Add("title_asc", "Title");

            UserSortItems = new Dictionary<string, string>();
            UserSortItems.Add("lastname_asc", "Last Name");
            UserSortItems.Add("email_asc", "Email");
            UserSortItems.Add("city_asc", "City");
            UserSortItems.Add("country_asc", "Country");
            UserSortItems.Add("ordercount_desc", "Orders");

            OrderSortItems = new Dictionary<string, string>();
            OrderSortItems.Add("orderdate_desc", "Date");
            OrderSortItems.Add("totalprice_desc", "Total");
            OrderSortItems.Add("itemcount_desc", "Line Items");

            Mapper.CreateMap<Order, ReportOrderModel>();
            Mapper.CreateMap<Product, ReportProductModel>();
            Mapper.CreateMap<User, ReportUserModel>();

            Mapper.CreateMap<Order, ReportOrderDetailsModel>();
            Mapper.CreateMap<OrderDetail, ReportOrderDetailModel>();
        }


        [HttpGet]
        public ActionResult Reports()
        {
            var model = new ReportsModel();

            var startDate = new DateTime(2013, 4, 30).AddDays(-7);   // In real app change to DateTime.Now.AddDays(-7);

            model.TotalRevenues = (double?)ArtContext.Orders.Sum("TotalPrice");
            model.LastWeekRevenues = (double?)ArtContext.Orders.Sum("TotalPrice", where: "OrderDate > @0", parms: startDate);

            model.ProductCount = ArtContext.Products.Count();
            model.UserCount = ArtContext.Users.Count();
            model.OrderCount = ArtContext.Orders.Count();

            return View(model);
        }

        [HttpGet]
        public ActionResult Users(string sort = "lastname_asc", bool ordersOnly = false, int page = 1, int pageSize = 10)
        {
            ValidateUsersArgs(sort, page, pageSize);

            var model = new ReportUsersModel { Sort = sort, OrdersOnly = ordersOnly, Page = page, PageSize = pageSize, SortItems = UserSortItems };

            string where = ordersOnly ? "OrderCount > 0" : null;
            var users = ArtContext.Users.Paged(out model.TotalRows, where: where, orderBy: sort.Replace("_", " "), page: page, pageSize: pageSize);

            model.Items = Mapper.Map<IEnumerable<User>, IEnumerable<ReportUserModel>>(users);

            if (Request.IsAjaxRequest())
                return PartialView("_Users", model);
            else
                return View(model);
        }

        [HttpGet]
        public ActionResult Orders(string sort = "orderdate_desc", int page = 1, int pageSize = 10, bool layout = true)
        {
            ValidateOrdersArgs(sort, page, pageSize);

            var model = new ReportOrdersModel { Sort = sort, Page = page, PageSize = pageSize, SortItems = OrderSortItems };
            var orders = ArtContext.Orders.Paged(out model.TotalRows, orderBy: sort.Replace("_", " "), page: page, pageSize: pageSize);
            var users = ArtContext.Users.All(orders.CommaSeparate(o => o.UserId)).ToDictionary(u => u.Id);

            model.Items = Mapper.Map<IEnumerable<Order>, IEnumerable<ReportOrderModel>>(orders);
            foreach (var order in model.Items)
                order.UserName = users[order.UserId].FullName;

            // exclude layout when browser history is recalled
            if (!layout && Request.IsAjaxRequest())
            {
                ViewBag.Layout = "No";  // return page without layout. 
                return View(model);
            }


            if (Request.IsAjaxRequest())
                return PartialView("_Orders", model);
            else
                return View(model);
        }

        [HttpGet]
        public ActionResult OrderDetails(int? id)
        {
            var order = ArtContext.Orders.Single(id);
            var orderDetails = ArtContext.OrderDetails.All(where: "OrderId = @0", parms: id);
            var user = ArtContext.Users.Single(order.UserId);
            var products = ArtContext.Products.All(orderDetails.CommaSeparate(d => d.ProductId)).ToDictionary(p => p.Id);

            var model = Mapper.Map<Order, ReportOrderDetailsModel>(order);
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;

            foreach (var detail in orderDetails)
            {
                var m = Mapper.Map<OrderDetail, ReportOrderDetailModel>(detail);
                m.SubTotal = m.Quantity * m.Price;

                var product = products[detail.ProductId];
                m.ProductArtist = ArtCache.Artists[product.ArtistId].FullName;
                m.ProductImage = product.Image;
                m.ProductTitle = product.Title;

                model.OrderDetails.Add(m);
            }

            model.HttpReferer = Request.UrlReferrer.PathAndQuery;
            return View(model);
        }

        [HttpGet]
        public ActionResult Products(string sort = "quantitysold_desc", int page = 1, int pageSize = 10)
        {
            ValidateProductsArgs(sort, page, pageSize);

            var model = new ReportProductsModel { Sort = sort, Page = page, PageSize = pageSize, SortItems = ProductSortItems };
            var products = ArtContext.Products.Paged(out model.TotalRows, orderBy: sort.Replace("_", " "), page: page, pageSize: pageSize);

            model.Items = Mapper.Map<IEnumerable<Product>, IEnumerable<ReportProductModel>>(products);
            foreach (var product in model.Items)
            {
                product.ArtistName = ArtCache.Artists[product.ArtistId].FullName;
                product.Revenue = product.Price * product.QuantitySold;
            }

            if (Request.IsAjaxRequest())
                return PartialView("_Products", model);
            else
                return View(model);
        }

        void ValidateProductsArgs(string sort, int page, int pageSize)
        {
            if (!ProductSortItems.ContainsKey(sort)) throw new ArgumentException("Invalid Sort");
            if (page < 1) throw new ArgumentException("Invalid Page");
            if (pageSize < 1) throw new ArgumentException("Invalid PageSize");
        }

        void ValidateUsersArgs(string sort, int page, int pageSize)
        {
            if (!UserSortItems.ContainsKey(sort)) throw new ArgumentException("Invalid Sort");
            if (page < 1) throw new ArgumentException("Invalid Page");
            if (pageSize < 1) throw new ArgumentException("Invalid PageSize");
        }

        void ValidateOrdersArgs(string sort, int page, int pageSize)
        {
            if (!OrderSortItems.ContainsKey(sort)) throw new ArgumentException("Invalid Sort");
            if (page < 1) throw new ArgumentException("Invalid Page");
            if (pageSize < 1) throw new ArgumentException("Invalid PageSize");
        }
    }
}
