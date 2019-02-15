
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace DataObjects.Linq2Sql
{
    // ** Factory pattern

    // DataContext factory caches the connectionstring and the 
    // mapping source so that DataContext instances can be created quickly.
    // this significantly reduces the DataContext creation times. 
    
    public static class DataContextFactory
    {
        static readonly string connectionString;
        static readonly MappingSource mappingSource;

        // static initialization of connectionstring and mappingSource.
        // This significantly increases performance, primarily due to mappingSource cache.

        static DataContextFactory()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Action"].ConnectionString;

            var context = new ActionDataContext(connectionString);
            mappingSource = context.Mapping.MappingSource;
        }

        // ** Factory method. creates a new DataContext using cached connectionstring

        public static ActionDataContext CreateContext()
        {
            return new ActionDataContext(connectionString, mappingSource);
        }
    }
}
