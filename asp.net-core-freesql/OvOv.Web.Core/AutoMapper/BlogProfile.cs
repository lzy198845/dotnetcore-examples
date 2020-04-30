using AutoMapper;
using OvOv.Web.Core.Domain;
using OvOv.Web.Core.Models.Blogs;

namespace OvOv.Web.Core.AutoMapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile() 
        {
            CreateMap<CreateBlogDto, Blog>();
            CreateMap<UpdateBlogDto, Blog>();
        }
    }
}