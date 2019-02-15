using System;
using System.Data.Linq;

namespace DataObjects.Linq2Sql
{
    // helper class to facilitate Binary timestamp conversions 
    // Note: not used in this release
    
    public static class VersionConverter
    {
        public static string ToString(Binary version)
        {
            if (version == null)
                return null;

            return Convert.ToBase64String(version.ToArray());
        }

        public static Binary ToBinary(string version)
        {
            if (string.IsNullOrEmpty(version))
                return null;

            return new Binary(Convert.FromBase64String(version));
        }
    }
}
