using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNet
{
    // Data access object for Category
    // ** DAO Pattern

    public class CategoryDao : ICategoryDao
    {
        static Db db = new Db();

        public List<Category> GetCategories()
        {
            string sql =
            @"SELECT CategoryId, CategoryName, Description
                FROM [Category]";

            return db.Read(sql, Make).ToList();
        }

        public Category GetCategoryByProduct(int productId)
        {
            string sql =
            @"SELECT C.CategoryId, CategoryName, Description 
                FROM [Category] C INNER JOIN [Product] P ON P.CategoryId = C.CategoryId 
               WHERE ProductId = @ProductId";

            object[] parms = { "@ProductId", productId };
            return db.Read(sql, Make, parms).FirstOrDefault();
        }


        static Func<IDataReader, Category> Make = reader =>
           new Category
           {
               CategoryId = reader["CategoryId"].AsId(),
               CategoryName = reader["CategoryName"].AsString(),
               Description = reader["Description"].AsString()
           };

        object[] Take(Category category)
        {
            return new object[]  
            {
                "@CategoryId", category.CategoryId,
                "@CategoryName", category.CategoryName,
                "@Description", category.Description
            };
        }
    }
}
