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

    public class RatingsController : BaseApiController
    {

        // GET Collection

        [HttpGet]
        public IEnumerable<ApiRating> Get(string expand = "")
        {
            return new List<ApiRating>();
        }

        // GET Single

        [HttpGet]
        public ApiRating Get(int? id, string expand = "")
        {
            return new ApiRating();
        }

        // POST = Insert

        [HttpPost]
        public ApiRating Post([FromBody] ApiRating apirating)
        {
            return apirating;
        }

        // PUT = Update

        [HttpPut]
        public ApiRating Put(int? id, [FromBody] ApiRating apirating)
        {
            return apirating;
        }

        // DELETE

        [HttpDelete]
        public ApiRating Delete(int? id)
        {
			return new ApiRating();
        }
    }
}
