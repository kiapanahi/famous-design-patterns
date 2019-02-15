
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

    public class ArtistsController : BaseApiController
    {

        // GET Collection

        [HttpGet]
        public IEnumerable<ApiArtist> Get(string expand = "")
        {
            return new List<ApiArtist>();
        }

        // GET Single

        [HttpGet]
        public ApiArtist Get(int? id, string expand = "")
        {
            return new ApiArtist();
        }

        // POST = Insert

        [HttpPost]
        public ApiArtist Post([FromBody] ApiArtist apiartist)
        {
            return apiartist;
        }

        // PUT = Update

        [HttpPut]
        public ApiArtist Put(int? id, [FromBody] ApiArtist apiartist)
        {
            return apiartist;
        }

        // DELETE

        [HttpDelete]
        public ApiArtist Delete(int? id)
        {
			return new ApiArtist();
        }
    }
}
