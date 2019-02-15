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

    public class CartItemsController : BaseApiController
    {

        // GET Collection

        [HttpGet]
        public IEnumerable<ApiCartItem> Get(string expand = "")
        {
            return new List<ApiCartItem>();
        }

        // GET Single

        [HttpGet]
        public ApiCartItem Get(int? id, string expand = "")
        {
            return new ApiCartItem();
        }

        // POST = Insert

        [HttpPost]
        public ApiCartItem Post([FromBody] ApiCartItem apicartitem)
        {
            return apicartitem;
        }

        // PUT = Update

        [HttpPut]
        public ApiCartItem Put(int? id, [FromBody] ApiCartItem apicartitem)
        {
            return apicartitem;
        }

        // DELETE

        [HttpDelete]
        public ApiCartItem Delete(int? id)
        {
			return new ApiCartItem();
        }
    }
}
