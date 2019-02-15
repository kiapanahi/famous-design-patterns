using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    // Data access object for Category
    // ** DAO Pattern

    public class CategoryDao : ICategoryDao
    {
        static CategoryDao()
        {
            Mapper.CreateMap<CategoryEntity, Category>();
        }

        public List<Category> GetCategories()
        {
            using (var context = new actionEntities())
            {
                var categories = context.CategoryEntities.ToList();
                return Mapper.Map<List<CategoryEntity>, List<Category>>(categories);
            }
        }

        public Category GetCategoryByProduct(int productId)
        {
            using (var context = new actionEntities())
            {
                var product = context.ProductEntities.SingleOrDefault(p => p.ProductId == productId);
                var category = context.CategoryEntities.SingleOrDefault(c => c.CategoryId == product.CategoryId);

                return Mapper.Map<CategoryEntity, Category>(category);
            }
        }
    }
}
