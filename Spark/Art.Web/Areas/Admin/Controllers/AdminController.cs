using Art.Domain;
using Art.Web.Areas.Admin.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Art.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        static Dictionary<string, string> UserSortItems { get; set; }

        static AdminController()
        {
            UserSortItems = new Dictionary<string, string>();
            UserSortItems.Add("signupdate_desc", "Signup");
            UserSortItems.Add("lastname_asc", "Last Name");
            UserSortItems.Add("email_asc", "Email");
            UserSortItems.Add("city_asc", "City");
            UserSortItems.Add("country_asc", "Country");
            UserSortItems.Add("ordercount_desc", "Orders");

            Mapper.CreateMap<User, AdminUserModel>();

            Mapper.CreateMap<User, AdminNewUserModel>();
            Mapper.CreateMap<AdminNewUserModel, User>();
        }

        // display a paged set of users
        // note: pagesize argument is not defined on page

        [HttpGet]
        public ActionResult Users(string sort = "signupdate_desc", int page = 1, int pageSize = 10, bool layout = true)
        {
            ValidateUsersArgs(sort, page, pageSize);

            var model = new AdminUsersModel { Sort = sort, Page = page, PageSize = pageSize, SortItems = UserSortItems };

            // exclude current admin from list
            var users = ArtContext.Users.Paged(out model.TotalRows, where: "Id <> @0", orderBy: sort.Replace("_", " "), page: page, pageSize: pageSize, parms: CurrentUser.Id);
            model.Items = Mapper.Map<IEnumerable<User>, IEnumerable<AdminUserModel>>(users);

            // exclude layout when browser history is recalled
            if (!layout && Request.IsAjaxRequest())
            {
                ViewBag.Layout = "No";  // return page without layout. 
                return View(model);
            }

            if (Request.IsAjaxRequest())
                return PartialView("_Users", model);
            else
                return View(model);
        }

        // GET 

        [HttpGet]
        [ActionName("User")]
        public ActionResult GetUser(int? id = null)
        {
            var model = new AdminNewUserModel();

            if (id != null)
            {
                var user = ArtContext.Users.Single(id);
                model = Mapper.Map<User, AdminNewUserModel>(user);
            }

            model.HttpReferer = Request.UrlReferrer.PathAndQuery;
            return View("User", model);
        }

        // POST = Insert & Update

        [HttpPost]
        [ActionName("User")]
        public ActionResult PostUser(AdminNewUserModel model)
        {
            // ** Prototype pattern. the user object which has its default values set

            var user = Mapper.Map<AdminNewUserModel, User>(model, new User(true));

            if (user.Id != null)  // existing user
            {
                // remove email and password validation. not required for update

                ModelState.Remove("Email");
                ModelState.Remove("Password");

                if (ModelState.IsValid)
                {
                    if (!user.IsValid) // shows how custom validation would be used
                    {
                        // examine user.Errors list
                    }
                    ArtContext.Users.Update(user);
                    Success = "User " + user.FullName + " was successfully updated.";

                    return RedirectToAction("Users");
                }
            }
            else // new user
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // ** Facade pattern. Unit of Work pattern.
                        Service.InsertUser(user, model.Password, "Member");

                        Success = "User " + user.FullName + " was successfully added.";

                        return RedirectToAction("Users");
                    }
                    catch (MembershipCreateUserException e)
                    {
                        Failure = ErrorCodes.Find(e.StatusCode);
                    }
                }
            }

            return View(model);
        }

        // DELETE = delete

        [HttpDelete]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [ActionName("User")]
        public void DeleteUser(int? id)
        {
            // ** CQRS Pattern. App does not wait return values: it just assumes it works.
            var user = ArtContext.Users.Single(id);

            // ** Facade pattern and Unit of Work pattern.
            Service.DeleteUser(user);
        }

        #region Private Helpers

        void ValidateUsersArgs(string sort, int page, int pageSize)
        {
            if (!UserSortItems.ContainsKey(sort)) throw new ArgumentException("Invalid Sort");
            if (page < 1) throw new ArgumentException("Invalid Page");
            if (pageSize < 1) throw new ArgumentException("Invalid PageSize");
        }

        #endregion
    }
}
