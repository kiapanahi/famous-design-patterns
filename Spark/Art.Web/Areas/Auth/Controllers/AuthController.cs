using Art.Domain;
using Art.Web.Areas.Auth.Models;
using AutoMapper;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;


namespace Art.Web.Areas.Auth.Controllers
{
    public class AuthController : BaseController
    {
        #region Internal Authentication

        static AuthController()
        {
            // ** Prototype pattern (a prototype User object will be created)
            Mapper.CreateMap<SignupModel, User>().ConstructUsing((Func<SignupModel, User>) (u => new User(true)));
            Mapper.CreateMap<User, AccountModel>();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.Email, model.Password, model.RememberMe))
                {
                    SetCustomAuthenticationCookie(model.Email, model.RememberMe);

                    return RedirectToLocal(returnUrl);
                }
            }

            Failure = "The credentials provided are incorrect.";
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            WebSecurity.Logout();

            // instead of displaying logout page directly we redirect to confirmation page.
            // this will ensure auth cookie is cleared, which, in turn, ensures correct menu items are displayed

            return RedirectToAction("LogoutConfirm");
        }

        [HttpGet]
        public ActionResult LogoutConfirm()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            var model = new SignupModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = Mapper.Map<SignupModel, User>(model);

                    // ** Facade pattern. Unit of Work pattern.
                    Service.InsertUser(user, model.Password, "Member");

                    WebSecurity.Login(model.Email, model.Password);
                    SetCustomAuthenticationCookie(model.Email, rememberMe: false);

                    return RedirectToLocal();
                }
                catch (MembershipCreateUserException e)
                {
                    Failure = ErrorCodes.Find(e.StatusCode);
                }
            }
            else
                Failure = "Signup was unsuccessful. Please check all data entries";

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Account()
        {
            var user = ArtContext.Users.Single(CurrentUser.Id);
            var model = Mapper.Map<User, AccountModel>(user);

            // for oauth members we don't maintain a password
            model.IsAuthenticatedWithOAuth = IsAuthenticatedWithOAuth();

            return View(model);
        }



        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(AccountModel model)
        {
            // these validations are not relevant here
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("City");
            ModelState.Remove("Country");

            bool success = false;
            if (ModelState.IsValid)
            {
                // note: ChangePassword may throw an exception
                try
                {
                    success = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    if (success) Success = "Password has been updated";
                }
                catch { }
            }

            if (!success) Failure = "Current password is incorrect or new password is invalid.";

            return RedirectToAction("Account");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAccount(AccountModel model)
        {
            // these validations are not relevant here
            ModelState.Remove("OldPassword");
            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                // update properties and save

                var user = ArtContext.Users.Single(CurrentUser.Id);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.City = model.City;
                user.Country = model.Country;

                ArtContext.Users.Update(user);

                // update authentication cookie with potentially changed first name or last name 
                SetCustomAuthenticationCookie(user.Email, rememberMe: true);

                Success = "User Profile has been updated";
            }
            else
            {
                Failure = "Invalid data. Please check each data entry.";
            }

            model.IsAuthenticatedWithOAuth = IsAuthenticatedWithOAuth();

            return View("Account", model);
        }

        #endregion

        #region External Authentication (OAuth)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl) // Oauth
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        public ActionResult ExternalLoginCallback(string returnUrl) // Oauth/callback
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            // existing account

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                SetCustomAuthenticationCookie(email: result.ExtraData["email"], rememberMe: false);

                return RedirectToLocal(returnUrl);
            }

            // create new account with default values set (notice the 'true' argument).
            // ** Prototype patterns

            var user = new User(true)
            {
                FirstName = result.ExtraData["first_name"],
                LastName = result.ExtraData["last_name"],
                Email = result.ExtraData["email"],
                City = result.ExtraData["city"],
                Country = result.ExtraData["country"]
            };

            // ** Facade pattern. Unit of Work pattern.

            Service.InsertOAuthUser(user, result.Provider, result.ProviderUserId, "Member");

            OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false);
            SetCustomAuthenticationCookie(user.Email, rememberMe: false);

            return RedirectToLocal(returnUrl);
        }

        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #endregion

        #region Private Helpers

        // add custom user data to authentication cookie

        void SetCustomAuthenticationCookie(string email, bool rememberMe)
        {
            var user = ArtContext.Users.ByEmail(email); 

            var principalModel = new CustomPrincipalModel();
            principalModel.UserId = (int)user.Id;
            principalModel.FirstName = user.FirstName;
            principalModel.LastName = user.LastName;

            var serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(principalModel);

            var authCookie = FormsAuthentication.GetAuthCookie(email, rememberMe);
            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userData);
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            Response.Cookies.Add(authCookie);
        }

        // workaround for an issue with OAuthWebSecurity.IsAuthenticatedWithOAuth

        bool IsAuthenticatedWithOAuth()
        {
            // next line is commented out because IsAuthenticatedWithOAuth seems to always return false
            // return OAuthWebSecurity.IsAuthenticatedWithOAuth;

            // quick and easy workaround
            var count = (int)ArtContext.Scalar("SELECT COUNT(UserId) FROM [webpages_OAuthMembership] WHERE UserId = @0", CurrentUser.Id);
            return count > 0;
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                // starts provider authentication and provider will redirect back to returnurl. 
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        // redirects to valid local page

        ActionResult RedirectToLocal(string returnUrl = null)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "Home" });
            }
        }

        #endregion
    }
}
