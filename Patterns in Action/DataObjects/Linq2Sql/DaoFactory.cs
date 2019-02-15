using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Linq2Sql
{
    // Data access object factory
    // ** Factory Pattern

    public class DaoFactory : IDaoFactory
    {
        public IMemberDao MemberDao { get { return new MemberDao(); } }
        public IOrderDao OrderDao { get { return new OrderDao(); } }
        public IOrderDetailDao OrderDetailDao { get { return new OrderDetailDao(); } }
        public IProductDao ProductDao { get { return new ProductDao(); } }
        public ICategoryDao CategoryDao { get { return new CategoryDao(); } }
    }
}
