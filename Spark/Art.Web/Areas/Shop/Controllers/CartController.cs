using Art.Domain;
using Art.Web.Areas.Shop.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Art.Web.Areas.Shop.Controllers
{
    public class CartController : BaseController
    {
        static CartController()
        {
            Mapper.CreateMap<Cart, CartModel>();
            Mapper.CreateMap<CartItem, CartItemModel>();
        }

        // show shopping cart

        [HttpGet]
        public ActionResult Cart()
        {
            var model = new CartModel();

            var cookie = Request.Cookies[".cart"];
            if (cookie == null)
            {
                CurrentCart.ItemCount = 0;
                return View(model);
            }

            var cart = ArtContext.Carts.Single(where: "Cookie = @0", parms: cookie.Value);
            if (cart == null)
            {
                // cookie and db are out of sync

                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
                return View(model);
            }

            var items = ArtContext.CartItems.All(where: "CartId = @0", parms: cart.Id);

            if (items.Count() > 0)
            {
                model.Items = Mapper.Map<IEnumerable<CartItem>, IEnumerable<CartItemModel>>(items);

                var products = ArtContext.Products.All(where: "Id IN (" + items.CommaSeparate(i => i.ProductId) + ")").ToDictionary(p => p.Id);
                foreach (var item in model.Items)
                {
                    item.SubTotal = item.Quantity * item.Price;
                    item.ProductImage = products[item.ProductId].Image;
                    item.ProductTitle = products[item.ProductId].Title;
                    item.ProductAvgStars = ((Math.Round(products[item.ProductId].AvgStars * 2) / 2) * 10).ToString();
                    item.ProductArtistName = ArtCache.Artists[products[item.ProductId].ArtistId].FullName;
                }

                model.GrandTotal = items.Aggregate(0D, (runningTotal, next) => runningTotal + (next.Quantity * next.Price));
            }

            // this is a good time to refresh cart item count in our cookie
            CurrentCart.ItemCount = items.Count();


            return View(model);
        }

        // add item to cart

        [HttpPost]
        [AjaxOnly]
        public ActionResult Cart(int count, int? id)
        {
            bool newCart = false;
            var cookie = Request.Cookies[".cart"];

            if (cookie == null)
            {
                cookie = new HttpCookie(".cart", Guid.NewGuid().ToString());
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);
                newCart = true;
            }
            else
            {
                // double check that associated cart exists
                var cart = ArtContext.Carts.Single(where: "Cookie = @0", parms: cookie.Value);
                if (cart == null) newCart = true;
            }

            // optimistic increment, that is, it assumes that the subsequent work will succeed. 
            CurrentCart.ItemCount++;

            // ** CQRS Pattern (fire and forget)

            new Thread(() =>
            {
                if (newCart)
                    AddToNewCart(cookie, count, id);
                else
                    AddToExistingCart(cookie, count, id);

            }).Start();

            // ** Null Object Pattern
            return new EmptyResult();
        }

        // update quantity of cart line item

        [HttpPut]
        [AjaxOnly]
        public ActionResult Cart(int id, int quantity)
        {

            // ** CQRS Pattern (fire and forget)

            new Thread(() =>
            {
                var cartItem = ArtContext.CartItems.Single(id);
                cartItem.Quantity = quantity;
                ArtContext.CartItems.Update(cartItem);

            }).Start();

            return new EmptyResult();
        }

        // delete cart item

        [HttpDelete]
        [AjaxOnly]
        public ActionResult Cart(int id)
        {
            // optimistic, that is, it is assumed that the subsequent delete will succeed
            CurrentCart.ItemCount--;

            // ** CQRS Pattern (fire and forget)

            new Thread(() =>
            {
                var cartItem = ArtContext.CartItems.Single(id);
                ArtContext.CartItems.Delete(cartItem);

            }).Start();

            return new EmptyResult();
        }

        // checkout and pay for cart

        [HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            var cookie = Request.Cookies[".cart"];
            if (cookie == null) return RedirectToAction("Cart");

            var cart = ArtContext.Carts.Single(where: "Cookie = @0", parms: cookie.Value);
            var items = ArtContext.CartItems.All(where: "CartId = @0", parms: cart.Id);

            var model = new CheckoutModel();
            model.GrandTotal = items.Aggregate(0D, (runningTotal, next) => runningTotal + (next.Quantity * next.Price));

            return View(model);
        }

        // checkout. here is where payment is validated (although not implemented).
        // it also is the place where the cart-to-order transaction is completed.

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(CheckoutModel model)
        {
            // note: payment information is not validated nor captured.
            // we simply move on and create the order

            var cookie = Request.Cookies[".cart"];
            if (cookie == null) return RedirectToAction("Cart");

            var cart = ArtContext.Carts.Single(where: "Cookie = @0", parms: cookie.Value);
            var items = ArtContext.CartItems.All(where: "CartId = @0", parms: cart.Id);

            var grandTotal = items.Aggregate(0D, (runningTotal, next) => runningTotal + (next.Quantity * next.Price));

            // ** Prototype pattern

            var order = new Order(true) { OrderDate = DateTime.Now, TotalPrice = grandTotal, UserId = CurrentUser.Id };
            var details = new List<OrderDetail>();
            foreach (var item in items)
            {
                // ** Prototype pattern

                details.Add(new OrderDetail(true) { Price = item.Price, ProductId = item.ProductId, Quantity = item.Quantity });
            }

            order.OrderNumber = ArtContext.OrderNumbers.Next();

            // ** Facade Pattern. Unit of Work Pattern.

            Service.CreateOrder(order, details, cart, items);

            // Expire Cart cookie
            cookie.Expires = DateTime.Now.AddDays(-30);
            Response.Cookies.Add(cookie);

            // Update cart item count
            CurrentCart.ItemCount = 0;

            return View("Confirm", new ConfirmModel { OrderNumber = order.OrderNumber });
        }

        // confirm page

        [HttpGet]
        [Authorize]
        public ActionResult Confirm()
        {
            var model = new ConfirmModel();
            return View(model);
        }

        #region Private Helpers

        void AddToNewCart(HttpCookie cookie, int count, int? id)
        {
            var product = ArtContext.Products.Single(id);

            // ** Unit of Work Pattern

            using (var uow = new ArtUnitOfWork())
            {
                // ** Prototype Pattern

                var cart = new Cart(true) { Cookie = cookie.Value };
                uow.Insert(cart);

                // ** Prototype Pattern

                var item = new CartItem(true) { CartId = cart.Id, Quantity = count, ProductId = id, Price = product.Price };
                uow.Insert(item);
            }
        }

        void AddToExistingCart(HttpCookie cookie, int count, int? id)
        {
            var cart = ArtContext.Carts.Single(where: "Cookie = @0", parms: cookie.Value);
            var product = ArtContext.Products.Single(id);

            // check if product already exists in cart
            var item = ArtContext.CartItems.Single(where: "CartId = @0 AND ProductId = @1", parms: new object[] { cart.Id, id });
            if (item != null)
            {
                item.Quantity += count;
                ArtContext.CartItems.Update(item);
            }
            else  // cart item has default values set
            {

                // ** Prototype Pattern

                item = new CartItem(true) { CartId = cart.Id, Quantity = count, ProductId = id, Price = product.Price };
                ArtContext.CartItems.Insert(item);
            }
        }

        #endregion
    }
}
