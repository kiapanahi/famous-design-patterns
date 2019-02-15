using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using AutoMapper;
using System.Configuration;

namespace DataObjects.Linq2Sql
{
    // Data access object for Category
    // ** DAO Pattern

    public class CategoryDao : ICategoryDao
    {
        static CategoryDao()
        {
            Mapper.CreateMap<Category, BusinessObjects.Category>();
        }

        public List<BusinessObjects.Category> GetCategories()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var categories = context.Categories.ToList();
                return Mapper.Map<List<Category>, List<BusinessObjects.Category>>(categories);
            }
        }

        public BusinessObjects.Category GetCategoryByProduct(int productId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var category = context.Categories.SelectMany(c => context.Products
                    .Where(p => c.CategoryId == p.CategoryId)
                    .Where(p => p.ProductId == productId),
                     (c, p) => c).SingleOrDefault(c => true);

                return Mapper.Map<Category, BusinessObjects.Category>(category);
            }
        }
    }
}
