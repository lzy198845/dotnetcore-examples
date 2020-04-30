using AutoMapper;
using OvOv.Web.Core.Domain;
using OvOv.Web.Core.Models.Posts;

namespace OvOv.Web.Core.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto,Post>();
        }
    }
}