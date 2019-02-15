using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNet
{
    // Data access object for Product    
    // ** DAO Pattern

    public class ProductDao : IProductDao
    {
        static Db db = new Db();
    
        public List<Product> GetProductsByCategory(int categoryId, string sortExpression)
        {
            string sql = 
            @"SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock
                FROM [Product] P JOIN [Category] C ON P.CategoryId = C.CategoryId
               WHERE C.CategoryId = @CategoryId".OrderBy(sortExpression);

            object[] parms = { "@CategoryId", categoryId };
            return db.Read(sql, Make, parms).ToList();
        }

        public List<Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression)
        {
            string sql = 
                @"SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock
                    FROM [Product] ";

            var where = new StringBuilder();
            if (!string.IsNullOrEmpty(productName))
                where.Append("  WHERE ProductName LIKE @ProductName ");

            if (priceFrom != -1 && priceThru != -1)
            {
                where.Append( where.Length == 0 ? " WHERE " : " AND ");
                where.Append("UnitPrice >= @PriceFrom AND ");
                where.Append("UnitPrice <= @PriceThru ");
            }

            sql += where.ToString().OrderBy(sortExpression);

            object[] parms = { "@ProductName", "%" + productName + "%", "@PriceFrom", priceFrom, "@PriceThru", priceThru };
            return db.Read(sql, Make, parms).ToList();
        }

        public Product GetProduct(int productId)
        {
            string sql = 
             @"SELECT ProductId, ProductName, Weight, UnitPrice, UnitsInStock, 
                      C.CategoryId, CategoryName, Description 
                 FROM [Product] P JOIN [Category] C ON P.CategoryId = C.CategoryId
                WHERE P.ProductId = @ProductId";

            object[] parms =  {"@ProductId", productId};
            return db.Read(sql, Make, parms).FirstOrDefault();
        }


        // creates Product object from IDataReader
        
        private static Func<IDataReader, Product> Make = reader =>
          new Product
          {
              ProductId = reader["ProductId"].AsId(),
              ProductName = reader["ProductName"].AsString(),
              Weight = reader["Weight"].AsString(),
              UnitPrice = reader["UnitPrice"].AsDouble(),
              UnitsInStock = reader["UnitsInStock"].AsInt()
          };
        
        // creates query parameter list from Product object

        private object[] Take(Product product)
        {
            return new object[]  
            {
                "@ProductId", product.ProductId,
                "@ProductName", product.ProductName,
                "@Weight", product.Weight,
                "@UnitPrice", product.UnitPrice,
                "@UnitsInStock", product.UnitsInStock
            };
        }
    }
}
