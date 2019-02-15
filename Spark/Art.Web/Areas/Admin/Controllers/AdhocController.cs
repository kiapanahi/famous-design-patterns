using Art.Domain;
using Art.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdhocController : BaseController
    {
        static Dictionary<string, List<AdhocColumn>> schema { get; set; }
        static List<Builtin> builtins { get; set; }

        static AdhocController()
        {
            // build schema cache (only once)

            schema = new Dictionary<string, List<AdhocColumn>>();

            var sql = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
			             WHERE TABLE_NAME NOT LIKE 'aspnet_%'  AND TABLE_NAME NOT LIKE 'webpages_%'
                           AND TABLE_TYPE = 'BASE TABLE'
			             ORDER BY TABLE_NAME";

            var tables = ArtContext.Query(sql);

            foreach (var table in tables)
            {
                var adhocColumns = new List<AdhocColumn>();

                sql = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
			              FROM INFORMATION_SCHEMA.COLUMNS 
			             WHERE TABLE_NAME = '" + table.TABLE_NAME + @"'";

                var columns = ArtContext.Query(sql);

                foreach (var column in columns)
                    adhocColumns.Add(new AdhocColumn { Name = column.COLUMN_NAME, DataType = column.DATA_TYPE });

                schema.Add(table.TABLE_NAME, adhocColumns);
            }

            builtins = new List<Builtin>();
            builtins.Add(new Builtin
            {
                Id = "builtin1",
                Text = "Record Counts",
                Description = "Record counts for all tables in database",
                Sql =
                    @"SELECT Artists = (SELECT COUNT(Id) FROM [Artist]), 
                             Carts =  (SELECT COUNT(Id) FROM [Cart]),
                             CartItems =  (SELECT COUNT(Id) FROM [CartItem]),
                             Errors =  (SELECT COUNT(Id) FROM [Error]),
                             Orders=  (SELECT COUNT(Id) FROM [Order]),
                             OrderDetails=  (SELECT COUNT(Id) FROM [OrderDetail]),
                             Products=  (SELECT COUNT(Id) FROM [Product]),
                             Ratings=  (SELECT COUNT(Id) FROM [Rating]),
                             Users =  (SELECT COUNT(Id) FROM [User])"
            });
            builtins.Add(new Builtin
            {
                Id = "builtin2",
                Text = "Users with Orders",
                Description = "Display Users and all their orders",
                Sql =
                        @"SELECT U.FirstName, U.LastName,  U.Email, O.OrderDate, 
                                      '$' + CONVERT(varchar, CAST(O.TotalPrice AS Money), 1) AS Total, 
                                      O.OrderNumber
                            FROM [Order] O 
                            JOIN [User] U ON O.UserId = U.Id
                        ORDER BY O.OrderDate DESC"
            });

            builtins.Add(new Builtin
            {
                Id = "builtin3",
                Text = "Artists with Products",
                Description = "Display Artists and all their arts works (i.e. products)",
                Sql =
                        @"SELECT A.FirstName, A.LastName, 
                                    '$' + CONVERT(VARCHAR, CAST(P.Price AS Money), 1) AS Total, 
                                     P.Title
                            FROM [Artist] A JOIN [Product] P ON A.Id = P.ArtistId
                        ORDER BY A.LastName"
            });

            builtins.Add(new Builtin
            {
                Id = "builtin4",
                Text = "Update Statistics",
                Description = "Recompute totals and summary statistics in all relevant tables",
                Sql =

                        @"UPDATE A SET TotalProducts = ISNULL(X.Total,0)
                          FROM [Artist] A
                          LEFT OUTER JOIN (SELECT ArtistId, COUNT(Id) AS Total
                                  FROM [Product] 
                              GROUP BY ArtistId) AS X
                            ON A.Id = X.ArtistId;

                        UPDATE C SET ItemCount = ISNULL(X.Total,0)
                          FROM [Cart] C
                          LEFT OUTER JOIN (SELECT CartId, COUNT(Id) AS Total
                                  FROM [CartItem] 
                              GROUP BY CartId) AS X
                            ON C.Id = X.CartId;

                        UPDATE O SET ItemCount = ISNULL(X.Total,0)
                          FROM [Order] O
                          LEFT OUTER JOIN (SELECT OrderId, COUNT(Id) AS Total
                                  FROM [OrderDetail] 
                              GROUP BY OrderId) AS X
                            ON O.Id = X.OrderId;

                        UPDATE P SET QuantitySold = ISNULL(X.QSold,0)
                          FROM [Product] P
                          LEFT OUTER JOIN (SELECT ProductId, SUM(Quantity) AS QSold
                                  FROM [OrderDetail] 
                              GROUP BY ProductId) AS X
                            ON P.Id = X.ProductId;

                        UPDATE U SET OrderCount = ISNULL(X.Total,0)
                          FROM [User] U
                          LEFT OUTER JOIN (SELECT UserId, COUNT(Id) AS Total
                                  FROM [Order] 
                              GROUP BY UserId) AS X
                            ON U.Id = X.UserId;"
            });

        }

        [HttpGet]
        public ActionResult Adhoc()
        {

            var model = new AdhocModel { Schema = schema, Builtins = builtins };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adhoc(AdhocModel model)
        {
            string sql = model.Sql;
            string currentBuiltin = model.CurrentBuiltin;
            model = new AdhocModel { Sql = sql, Schema = schema, Builtins = builtins, CurrentBuiltin = currentBuiltin };

            try
            {
                if (sql.Trim().ToLower().StartsWith("select"))
                {
                    var results = new List<List<string>>();

                    var rows = ArtContext.Query(sql);

                    bool first = true;
                    foreach (var row in rows)
                    {
                        if (first)
                        {
                            List<string> headers = new List<string>();
                            foreach (var column in (IDictionary<string, object>)row)  // get column names
                            {
                                headers.Add(column.Key);
                            }

                            results.Add(headers);
                            first = false;
                        }

                        var values = new List<string>();
                        foreach (var column in (IDictionary<string, object>)row)  // get column values
                        {
                            string value = column.Value == null ? "" : column.Value.ToString();
                            values.Add(value);
                        }
                        results.Add(values);
                    }

                    model.Results = results;
                }
                else
                {
                    ArtContext.Execute(sql);
                    model.Results.Add(new List<string> { "Query ran successfully." });
                }
            }
            catch (Exception ex)
            {
                model.Exception = ex.ToString();
            }


            return View(model);
        }
    }
}
