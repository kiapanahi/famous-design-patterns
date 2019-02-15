using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;
using System.Security.Principal;
using System.Web.Script.Serialization;
using System.Threading;
using Microsoft.Web.WebPages.OAuth;
using Art.Domain;

namespace Art.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // initializes the SimpleMembership system and creates the 4 membership tables if they do not already exist
            // Art is the connectionstring name. User is user table name. Id is user Identity. Email is used as UserName.

            WebSecurity.InitializeDatabaseConnection("Art", "User", "Id", "Email", autoCreateTables: true);

            // create two roles in the app if they to not already exist

            if (!Roles.RoleExists("Admin")) Roles.CreateRole("Admin");
            if (!Roles.RoleExists("Member")) Roles.CreateRole("Member");

            // override table and key names (just for demo purposes, it has no effect)

            Art.Domain.User.Init(table: "User", key: "Id");

            // create membership accounts for admin Debbie and user Art Lover (only the very first time). 
            // you will not need this in your own projects. 

            string email = "debbie@company.com";
            string password = "secret123";
            if (WebSecurity.GetCreateDate(email) == DateTime.MinValue)
            {
                WebSecurity.CreateAccount(email, password);
                Roles.AddUserToRole(email, "Admin");
                Roles.AddUserToRole(email, "Member");
            }

            email = "artlover@gmail.com";
            if (WebSecurity.GetCreateDate(email) == DateTime.MinValue)
            {
                WebSecurity.CreateAccount(email, password);
                Roles.AddUserToRole(email, "Member");
            }

            // remove the XML formatter which allows viewing JSON data in Chrome and Firefox (their default is XML).
            // this is not recommended in real production systems.

            GlobalConfiguration.Configuration.Formatters.RemoveAt(1);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        // parses authentication cookie and updates thread's principal with custom user data

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    string encodedTicket = authCookie.Value;
                    if (!string.IsNullOrEmpty(encodedTicket))
                    {
                        var ticket = FormsAuthentication.Decrypt(encodedTicket);
                        var serializer = new JavaScriptSerializer();
                        var principalModel = serializer.Deserialize<CustomPrincipalModel>(ticket.UserData);

                        // setup proper roles

                        string[] roles = Thread.CurrentPrincipal.IsInRole("Admin") ? new string[] { "Admin", "Member" } : new string[] { "Member" };

                        var principal = new CustomPrincipal(ticket.Name, roles);
                        principal.UserId = principalModel.UserId;
                        principal.FirstName = principalModel.FirstName;
                        principal.LastName = principalModel.LastName;

                        // assign to current thread

                        Thread.CurrentPrincipal = principal;
                    }
                }
            }
            catch { WebSecurity.Logout(); }
        }

        // last resort error logging
        // note: customErrors in web config specifies error page

        protected void Application_Error(object sender, EventArgs e)
        {
            // get last exception
            var exception = HttpContext.Current.Server.GetLastError();

            if (exception != null)
                LogException(exception);
        }

        // logs an exception with relevant context data to the error table

        void LogException(Exception exception)
        {
            // try-catch because database itself could be down or Request context is unknown.

            try
            {
                int? userId = null;
                try { userId = CurrentUser.Id; }
                catch { /* do nothing */ }

                // ** Prototype pattern. the Error object has it default values initialized

                var error = new Error(true)
                {
                    UserId = userId,
                    Exception = exception.GetType().FullName,
                    Message = exception.Message,
                    Everything = exception.ToString(),
                    IpAddress = HttpContext.Current.Request.UserHostAddress,
                    UserAgent = HttpContext.Current.Request.UserAgent,
                    PathAndQuery = HttpContext.Current.Request.Url == null ? "" : HttpContext.Current.Request.Url.PathAndQuery,
                    HttpReferer = HttpContext.Current.Request.UrlReferrer == null ? "" : HttpContext.Current.Request.UrlReferrer.PathAndQuery
                };

                ArtContext.Errors.Insert(error);
            }
            catch { /* do nothing, or send email to webmaster*/}
        }
    }
}