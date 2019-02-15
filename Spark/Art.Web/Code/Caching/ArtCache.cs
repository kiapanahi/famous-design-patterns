using Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Art.Web
{
    // general caching tool for caching frequently used, but infrequently changing data.
    // artists is a good example of never changing data.  
    // we could have also included products and possibly users.

    public static class ArtCache
    {
        #region cache management

        static ObjectCache cache = new MemoryCache("Art");
        static object locker = new object();

        public static readonly string ArtistsKey = "ArtistsKey";

        // clear entire cache
        public static void Clear()
        {
            foreach (var item in cache)
                cache.Remove(item.Key);
        }

        // clears single cache entry
        public static void Clear(string key)
        {
            cache.Remove(key);
        }

        // add to cache helper
        static void Add(string key, object value, DateTimeOffset expiration, CacheItemPriority priority = CacheItemPriority.Default)
        {
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = expiration;
            policy.Priority = priority;

            var item = new CacheItem(key, value);
            cache.Add(item, policy);
        }

        #endregion

        // artists cache which is used throughout the app.

        public static Dictionary<int?, Artist> Artists
        {
            get
            {
                // ** Lazy load pattern 

                var dictionary = cache[ArtistsKey] as Dictionary<int?, Artist>;
                if (dictionary == null)
                {
                    lock (locker)
                    {
                        dictionary = ArtContext.Artists.All(orderBy: "LastName").ToDictionary(a => a.Id);
                        Add(ArtistsKey, dictionary, DateTime.Now.AddHours(1));
                    }
                }

                return dictionary;
            }
        }

        // clears artists cache
        public static void ClearArtists()
        {
            Clear(ArtistsKey);
        }
    }
}