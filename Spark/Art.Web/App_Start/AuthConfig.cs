using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;

namespace Art.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");


            // you will need to setup a Facebook app and enter 
            // the appId and appSecret here.

            OAuthWebSecurity.RegisterClient(
                new FacebookClient(
                    appId: "509049662487356",
                appSecret: "1b64294d0d95b1e202f8ef71fa9ab51d"),
                "Facebook", null
            );

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
