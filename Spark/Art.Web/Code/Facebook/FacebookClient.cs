using DotNetOpenAuth.AspNet.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;

namespace Art.Web
{
    // custom client of Facebook. 
    // DotNetAuth does not natively support requesting additional permissions (such as email access). 
    // this class gives more control over the permissions (called 'scope') requested from Facebook
    // here we ask for email and user_location, but could have included more (such as friends).

    public class FacebookClient : OAuth2Client
    {
        const string AuthorizationUrl = "https://www.facebook.com/dialog/oauth";
        const string TokenUrl = "https://graph.facebook.com/oauth/access_token";
        const string MeUrl = "https://graph.facebook.com/me?access_token=";

        readonly string _appId;
        readonly string _appSecret;

        public FacebookClient(string appId, string appSecret)
            : base("Facebook")
        {
            this._appId = appId;
            this._appSecret = appSecret;
        }

        protected override Uri GetServiceLoginUrl(Uri returnUrl)
        {
            return new Uri(
                        AuthorizationUrl
                        + "?client_id=" + this._appId
                        + "&redirect_uri=" + HttpUtility.UrlEncode(returnUrl.ToString())
                        + "&scope=email,user_location"
                        + "&display=page"
                    );
        }

        protected override IDictionary<string, string> GetUserData(string accessToken)
        {
            var client = new WebClient();
            string content = client.DownloadString(MeUrl + accessToken);

            // replace all unicode characters
            content = Regex.Replace(content, @"\\u([\dA-Fa-f]{4})", v => ((char)Convert.ToInt32(v.Groups[1].Value, 16)).ToString());
            dynamic data = Json.Decode(content);

            var result = new Dictionary<string, string>();
            result.Add("id", data.id);
            result.Add("name", data.name);
            result.Add("username", data.username);

            result.Add("first_name", null);
            result.Add("last_name", null);
            result.Add("city", null);
            result.Add("country", null);
            result.Add("email", data.email);

            if (data.first_name != null && data.last_name != null)
            {
                result["first_name"] = data.first_name;
                result["last_name"] = data.last_name;
            }
            else if (data.name != null)
            {
                string[] tokens = data.name.Split(' ');
                if (tokens.Length > 0) result["first_name"] = tokens[0];
                if (tokens.Length > 1) result["last_name"] = tokens[1];
            }
            else if (data.username != null)
            {
                result["first_name"] = "";
                result["last_name"] = data.username;
            }

            if (data.location != null)
            {
                if (data.location.name != null)
                {
                    string[] tokens = data.location.name.Split(',');
                    if (tokens.Length > 0) result["city"] = tokens[0].Trim();
                    if (tokens.Length > 1) result["country"] = tokens[1].Trim(); // this is province/state, but alas
                }
            }
            else if (data.hometown != null)
            {
                if (data.hometown.name != null)
                {
                    string[] tokens = data.hometown.name.Split(',');
                    if (tokens.Length > 0) result["city"] = tokens[0].Trim();
                    if (tokens.Length > 1) result["country"] = tokens[1].Trim(); // this is province/state, but alas
                }
            }

            // this works for USA locales only. 
            // to make it work for others would require a locale/country lookup facility.
            if (data.locale != null && data.locale.ToLower().Contains("us"))
                result["country"] = "USA";

            return result;
        }

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
        {
            var client = new WebClient();
            string content = client.DownloadString(
                TokenUrl
                + "?client_id=" + this._appId
                + "&client_secret=" + this._appSecret
                + "&redirect_uri=" + HttpUtility.UrlEncode(returnUrl.ToString())
                + "&code=" + authorizationCode
            );

            var nameValueCollection = HttpUtility.ParseQueryString(content);
            if (nameValueCollection != null)
            {
                return nameValueCollection["access_token"];
            }
            return null;
        }
    }
}