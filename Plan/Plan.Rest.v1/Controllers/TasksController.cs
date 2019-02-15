
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using AutoMapper;
using Plan.Domain;

namespace Plan.Rest.v1
{
    // Generated 07/26/2013 00:04:12
	
	// Change code for each method

    public class TasksController : BaseApiController
    {

        // GET Collection

        [HttpGet]
        public IEnumerable<ApiTask> Get(string expand = "")
        {
            return new List<ApiTask>();
        }

        // GET Single

        [HttpGet]
        public ApiTask Get(int? id, string expand = "")
        {
            return new ApiTask();
        }

        // POST = Insert

        [HttpPost]
        public ApiTask Post([FromBody] ApiTask apitask)
        {
            return apitask;
        }

        // PUT = Update

        [HttpPut]
        public ApiTask Put(int? id, [FromBody] ApiTask apitask)
        {
            return apitask;
        }

        // DELETE

        [HttpDelete]
        public ApiTask Delete(int? id)
        {
			return new ApiTask();
        }
    }
}
