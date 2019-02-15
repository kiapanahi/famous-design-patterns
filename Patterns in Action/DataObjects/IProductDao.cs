using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    // Defines db independent methods to access products
    // ** DAO Pattern

    public interface IProductDao
    {
        // gets a list of products for a given category

        List<Product> GetProductsByCategory(int categoryId, string sortExpression);

        // performs a search for products given several criteria

        List<Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression);

        // gets a specific product

        Product GetProduct(int productId);
    }
}

