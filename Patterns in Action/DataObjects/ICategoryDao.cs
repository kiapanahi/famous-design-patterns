using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    // defines methods to access categories.
    // this is a database-independent interface. implementations are database specific

    // ** DAO Pattern

    public interface ICategoryDao
    {
        // gets a list of product categories.
        
        List<Category> GetCategories();

        // gets a product category for a given product

        Category GetCategoryByProduct(int productId);
    }
}
