using ActionService;
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
using BusinessObjects;

namespace Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // initializes the SimpleMembership system and creates the 4 membership tables if they do not already exist
            // Action is the connectionstring name. Member is user table name. MemberId is user Identity. Email is used as UserName.

            WebSecurity.InitializeDatabaseConnection("Action", "Member", "MemberId", "Email", autoCreateTables: true);

            // create the two roles in the app if they to not already exist

            if (!Roles.RoleExists("Admin")) Roles.CreateRole("Admin");
            if (!Roles.RoleExists("Member")) Roles.CreateRole("Member");
            
            // get member and create potentially first admin

            var member = new Service().GetMemberByEmail("debbie@company.com");
            if (WebSecurity.GetCreateDate(member.Email) == DateTime.MinValue) 
            {
                var email = "debbie@company.com";
                WebSecurity.CreateAccount(email, "secret123");
                Roles.AddUserToRole(email, "Admin");
            }

            // standard MVC 4.5 registrations

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}