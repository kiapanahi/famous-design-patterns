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
	
	// Add custom code here

    public class BaseApiController : ApiController
    {
        static bool initialized = false;

        static BaseApiController()
        {
            if (!initialized)
            {
                Mapper.CreateMap<Task, ApiTask>().ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Id.ToTaskHref()));
                Mapper.CreateMap<User, ApiUser>().ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Id.ToUserHref()));

                Mapper.CreateMap<ApiTask, Task>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Href.ToId()));
                Mapper.CreateMap<ApiUser, User>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Href.ToId()));

                initialized = true;
            }
        }
    }

    // extension methods
    public static class HrefHelper
    {
        static string root = "http://localhost:52112/api/v1/";

        public static string ToTaskHref(this int? id) { return root + "tasks/" + id; }
        public static string ToUserHref(this int? id) { return root + "users/" + id; }

        public static int? ToId(this string href) 
        {
            if (string.IsNullOrEmpty(href)) return null;
            return int.Parse(href.Substring(href.LastIndexOf('/') + 1)); 
        }

    }
}
