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

    public class CartsController : BaseApiController
    {

        // GET Collection

        [HttpGet]
        public IEnumerable<ApiCart> Get(string expand = "")
        {
            return new List<ApiCart>();
        }

        // GET Single

        [HttpGet]
        public ApiCart Get(int? id, string expand = "")
        {
            return new ApiCart();
        }

        // POST = Insert

        [HttpPost]
        public ApiCart Post([FromBody] ApiCart apicart)
        {
            return apicart;
        }

        // PUT = Update

        [HttpPut]
        public ApiCart Put(int? id, [FromBody] ApiCart apicart)
        {
            return apicart;
        }

        // DELETE

        [HttpDelete]
        public ApiCart Delete(int? id)
        {
			return new ApiCart();
        }
    }
}
