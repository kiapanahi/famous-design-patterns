using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using AutoMapper;
using Plan.Domain;
using db = Plan.Domain.PlanContext;

namespace Plan.Rest.v1
{
    // Generated 07/26/2013 00:04:12
	
	// Change code for each method

    public class UsersController : BaseApiController
    {

        // GET Collection

        [HttpGet]
        public IEnumerable<ApiUser> Get(string expand = "")
        {
            var users = db.Users.All();
            return Mapper.Map<IEnumerable<User>, IEnumerable<ApiUser>>(users);
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
