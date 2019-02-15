using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Configuration;

namespace DataObjects.Linq2Sql
{
    // Data access object for Product
    // ** DAO Pattern

    public class ProductDao : IProductDao
    {
        static ProductDao()
        {
            Mapper.CreateMap<Product, BusinessObjects.Product>();
        }

        public List<BusinessObjects.Product> GetProductsByCategory(int categoryId, string sortExpression)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var products =  context.Products.Where(p => p.CategoryId == categoryId).OrderBy(sortExpression).ToList();
                return Mapper.Map<List<Product>, List<BusinessObjects.Product>>(products);
            }
        }

        public List<BusinessObjects.Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var query = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(productName))
                    query = query.Where(p => p.ProductName.Contains(productName));

                if (priceFrom != -1 && priceThru != -1)
                    query = query.Where(p => p.UnitPrice >= (decimal)priceFrom && p.UnitPrice <= (decimal)priceThru);

                var products = query.OrderBy(sortExpression, null).ToList();
                return Mapper.Map<List<Product>, List<BusinessObjects.Product>>(products);
            }
        }

        public BusinessObjects.Product GetProduct(int productId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var product = context.Products.SingleOrDefault(p => p.ProductId == productId);
                return Mapper.Map<Product, BusinessObjects.Product>(product);
            }
        }
    }
}
