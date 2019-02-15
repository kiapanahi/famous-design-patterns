using Art.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Text;
using Art.Domain;

namespace Art.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/

        public ActionResult Dashboard()
        {
            var model = new DashboardModel();

            // lightweight query (only includes one column and data of first 4 months).
            var users = ArtContext.Users.Query("SELECT SignupDate FROM [User] WHERE SignupDate < '2013/5/1' ORDER BY SignupDate");

            // get date range to partition in weekly buckets
            var min = new DateTime(2013, 1, 1);
            var max = new DateTime(2013, 5, 1);  // (DateTime)users.Max(u => u.SignupDate);

            // create a bucket for each week
            int weeks = (((max - min).Days + 7) / 7) + 1;
            var buckets = Enumerable.Range(1, weeks).ToDictionary(w => w, w => 0);

            // number of new users each week
            foreach (var user in users)
                buckets[WeekOfYear(user.SignupDate.Value)]++;

            // create cumulative values
            int runningTotal = 0;
            for (int i = 1; i <= buckets.Count; i++)
            {
                runningTotal += buckets[i];
                buckets[i] = runningTotal;
            }

            // flot formatting
            var usersData = new StringBuilder(@"[{ ""label"": ""Users"", ""color"" : ""#b00"", ""data"": [");
            var usersTicks = new StringBuilder("[");
            int index = 1;
            foreach (var bucket in buckets)
            {
                usersData.Append("[" + index + ", " + bucket.Value + "],");
                usersTicks.Append("[" + index + @", """ + bucket.Key + @"""],");
                index++;
            }
            usersData = usersData.Remove(usersData.Length - 1, 1).Append("] }]");
            usersTicks = usersTicks.Remove(usersTicks.Length - 1, 1).Append("]");

            model.UsersData = usersData.ToString();
            model.UsersTicks = usersTicks.ToString();

            // sales data

            // lightweight query (only includes two columns and data of first 4 months).
            var orders = ArtContext.Orders.Query("SELECT OrderDate, TotalPrice FROM [Order] WHERE OrderDate < '2013/5/1' ORDER BY OrderDate");

            for (int i = 1; i <= buckets.Count; i++)
                buckets[i] = 0;  // clear buckets

            // sales by week
            foreach (var order in orders)
                buckets[WeekOfYear(order.OrderDate.Value)] += (int)order.TotalPrice;

            // create cumulative values
            runningTotal = 0;
            for (int i = 1; i <= buckets.Count; i++)
            {
                runningTotal += buckets[i];
                buckets[i] = runningTotal;
            }

            // flot formatting
            var salesData = new StringBuilder(@"[{ ""label"": ""Sales"", ""color"" : ""#090"", ""data"": [");
            var salesTicks = new StringBuilder("[");
            index = 1;
            foreach (var bucket in buckets)
            {
                salesData.Append("[" + index + ", " + bucket.Value + "],");
                salesTicks.Append("[" + index + @", """ + bucket.Key + @"""],");
                index++;
            }
            salesData = salesData.Remove(salesData.Length - 1, 1).Append("] }]");
            salesTicks = salesTicks.Remove(salesTicks.Length - 1, 1).Append("]");

            model.SalesData = salesData.ToString();
            model.SalesTicks = salesTicks.ToString();

            // Demographics of users

            // lightweight query (only includes two columns and data of first 4 months).
            string sql = "SELECT Country, COUNT(Id) AS Number FROM [User] GROUP BY Country ORDER BY Number DESC";
            var countries = ArtContext.Query(sql);

            // flot formatting
            var demographics = new StringBuilder("[");
            foreach (var country in countries)
            {
                demographics.Append(@"{ ""label"": """ + country.Country + @""" , ""data"": " + country.Number + " },");
            }

            demographics = demographics.Remove(demographics.Length - 1, 1).Append("]");
            model.Demographics = demographics.ToString();

            return View(model);
        }

        // helper

        int WeekOfYear(DateTime date)
        {
            var calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
            int val = calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return val;
        }
    }
}
