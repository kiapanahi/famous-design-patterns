using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13

    // Change code for each method

    public class ProductsController : BaseApiController
    {

        // GET Collection

        [HttpGet]
        public IEnumerable<ApiProduct> Get(string expand = null, string filter = null, string sort = null, int page = 1, int pagesize = 20)
        {
            var products = ArtContext.Products.All();
            var apiproducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ApiProduct>>(products).ToList();

            int index = 0;
            if (expand != null && expand.ToLower() == "artist")
            {
                var artists = ArtContext.Artists.All().ToDictionary(a => a.Id);
                foreach (var product in products)
                {
                    var artist = artists[product.ArtistId];
                    apiproducts[index].Artist = Mapper.Map<Artist, ApiArtist>(artist);

                    //each artist itself has a product collection
                    foreach (var prod in products)
                    {
                        if (prod.ArtistId == product.ArtistId)
                        {
                            (apiproducts[index].Artist as ApiArtist).Products.Add(new ApiEntity { Href = prod.Id.ToProductHref() });
                        }
                    }

                    index++;
                }
            }
            else
            {
                // only hold artist reference
                foreach (var product in products)
                {
                    apiproducts[index++].Artist = new ApiEntity { Href = product.ArtistId.ToArtistHref() };
                }
            }
            return apiproducts;
        }

        // GET Single instance

        [HttpGet]
        public ApiProduct Get(int? id, string expand = null)
        {
            var product = ArtContext.Products.Single(id);
            var apiproduct = Mapper.Map<Product, ApiProduct>(product);

            if (expand != null && expand.ToLower() == "artist")
            {
                var artist = ArtContext.Artists.Single(product.ArtistId);
                apiproduct.Artist = Mapper.Map<Artist, ApiArtist>(artist);

                var products = ArtContext.Products.All(where: "ArtistId = @0", parms: artist.Id);
                foreach (var prod in products)
                    (apiproduct.Artist as ApiArtist).Products.Add(new ApiEntity { Href = prod.Id.ToProductHref() });
            }
            else
            {
                apiproduct.Artist = new ApiEntity { Href = product.ArtistId.ToArtistHref() };
            }

            return apiproduct;
        }

        // POST = Insert

        [HttpPost]
        public ApiProduct Post([FromBody] ApiProduct apiproduct)
        {
            var product = Mapper.Map<ApiProduct, Product>(apiproduct, new Product(true));
            product.ArtistId = apiproduct.Artist.Href.ToId();

            if (product.IsValid)
            {
                ArtContext.Products.Insert(product);
                apiproduct.Href = product.Id.ToProductHref();
                return apiproduct;
            }
            return null;
        }

        // PUT = Update

        [HttpPut]
        public ApiProduct Put(int? id, [FromBody] ApiProduct apiproduct)
        {
            var product = Mapper.Map<ApiProduct, Product>(apiproduct);
            product.ArtistId = apiproduct.Artist.Href.ToId();

            if (product.IsValid)
            {
                ArtContext.Products.Update(product);
                return apiproduct;
            }

            return null;
        }

        // DELETE

        [HttpDelete]
        public ApiProduct Delete(int? id)
        {
            var product = ArtContext.Products.Single(id);
            ArtContext.Products.Delete(product);

            return Mapper.Map<Product, ApiProduct>(product);
        }
    }
}
