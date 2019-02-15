using Microsoft.Web.WebPages.OAuth;
using Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Art.Web
{
    // ** Facade Pattern
    // this is the Service 'layer'

    public static class Service
    {

        // add a user to user table, simplemembership table, and roles table

        public static void InsertUser(User user, string password, string role)
        {
            // ** Unit of Work Pattern

            using (var uow = new UnitOfWorkDistributed())
            {
                ArtContext.Users.Insert(user);
                WebSecurity.CreateAccount(user.Email, password);
                Roles.AddUserToRole(user.Email, role);

                uow.Complete();
            }
        }

        // add a user to user table, oauth membership table and role table

        public static void InsertOAuthUser(User user, string provider, string providerUserId, string role)
        {
            // ** Unit of Work Pattern

            using (var uow = new UnitOfWorkDistributed())
            {
                ArtContext.Users.Insert(user);
                OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, user.Email);
                Roles.AddUserToRole(user.Email, role);

                uow.Complete();
            }
        }

        // deletes user from user table and membership tables.

        public static void DeleteUser(User user)
        {
            var simpleRoles = (SimpleRoleProvider)Roles.Provider;
            var simpleMembership = (SimpleMembershipProvider)Membership.Provider;

            List<string> roles = new List<string>();
            if (simpleRoles.IsUserInRole(user.Email, "Admin")) roles.Add("Admin");
            if (simpleRoles.IsUserInRole(user.Email, "Member")) roles.Add("Member");

            // possible OAuth account

            var oAuths = OAuthWebSecurity.GetAccountsFromUserName(user.Email);
            var oAuth = oAuths.Count > 0 ? oAuths.ToList()[0] : null;

            // ** Unit of Work Pattern

            using (var uow = new UnitOfWorkDistributed())
            {
                simpleRoles.RemoveUsersFromRoles(new string[] { user.Email }, roles.ToArray());

                if (oAuth != null)
                    OAuthWebSecurity.DeleteAccount(oAuth.Provider, oAuth.ProviderUserId);
                else
                    simpleMembership.DeleteAccount(user.Email);

                simpleMembership.DeleteUser(user.Email, false);

                uow.Complete();
            }
        }

        // creates an order by adding records to order tables and deleting record from cart tables. 

        public static void CreateOrder(Order order, IEnumerable<OrderDetail> details, Cart cart, IEnumerable<CartItem> items)
        {
            // ** Unit of Work pattern

            using (var uow = new ArtUnitOfWork())
            {
                // insert order and order details
                uow.Insert(order);
                details.ForEach(d => { d.OrderId = order.Id; uow.Insert(d); });

                // delete cartitems and cart
                items.ForEach(i => uow.Delete(i));
                uow.Delete(cart);
            }
        }
    }
}