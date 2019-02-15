using System.Linq.Dynamic;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    // Data access object for Product
    // ** DAO Pattern

    public class ProductDao : IProductDao
    {
        static ProductDao()
        {
            Mapper.CreateMap<ProductEntity, Product>();
        }

        public List<Product> GetProductsByCategory(int categoryId, string sortExpression)
        {
            using (var context = new actionEntities())
            {
                var products = context.ProductEntities.Where(p => p.CategoryId == categoryId)
                    .AsQueryable().OrderBy(sortExpression).ToList();
                return Mapper.Map<List<ProductEntity>, List<Product>>(products);
            }
        }

        public List<Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression)
        {
            using (var context = new actionEntities())
            {
                var query = context.ProductEntities.AsQueryable();
                if (!string.IsNullOrEmpty(productName))
                    query = query.Where(p => p.ProductName.Contains(productName));

                if (priceFrom != -1 && priceThru != -1)
                    query = query.Where(p => p.UnitPrice >= (decimal)priceFrom && p.UnitPrice <= (decimal)priceThru);

                var products = query.OrderBy(sortExpression, null).ToList();
                return Mapper.Map<List<ProductEntity>, List<Product>>(products);
            }
        }

        public Product GetProduct(int productId)
        {
            using (var context = new actionEntities())
            {
                var product = context.ProductEntities.SingleOrDefault(p => p.ProductId == productId);
                return Mapper.Map<ProductEntity, Product>(product);
            }
        }
    }
}
