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

    public class UsersController : BaseApiController
    {

        // GET Collection

        [HttpGet]
        public IEnumerable<ApiUser> Get(string expand = "")
        {
            return new List<ApiUser>();
        }

        // GET Single

        [HttpGet]
        public ApiUser Get(int? id, string expand = "")
        {
            return new ApiUser();
        }

        // POST = Insert

        [HttpPost]
        public ApiUser Post([FromBody] ApiUser apiuser)
        {
            return apiuser;
        }

        // PUT = Update

        [HttpPut]
        public ApiUser Put(int? id, [FromBody] ApiUser apiuser)
        {
            return apiuser;
        }

        // DELETE

        [HttpDelete]
        public ApiUser Delete(int? id)
        {
			return new ApiUser();
        }
    }
}
