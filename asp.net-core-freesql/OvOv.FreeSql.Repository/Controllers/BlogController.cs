using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OvOv.FreeSql.Repository;
using OvOv.FreeSql.Repository.Repositories;
using OvOv.FreeSql.Repository.Services;
using OvOv.Web.Core.Domain;
using OvOv.Web.Core.Models.Blogs;

namespace OvOv.FreeSql.Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogRepository _blogRepository;
        private readonly TagRepository tagRepository;
        private readonly IMapper _mapper;
        private readonly BlogService blogService;

        public BlogController(BlogRepository blogRepository, IMapper mapper, TagRepository tagRepository, BlogService blogService)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            this.tagRepository = tagRepository;
            this.blogService = blogService;
        }

        [HttpGet]
        public List<Blog> Get()
        {
            return _blogRepository.GetBlogs();
        }

        // POST api/blog
        [HttpPost]
        public void CreateBlog([FromBody] CreateBlogDto createBlogDto)
        {
            blogService.CreateBlog(createBlogDto);
        }

        /// <summary>
        /// 当出现异常时，不会插入数据
        /// </summary>
        /// <param name="createBlogDto"></param>
        [HttpPost("create")]
        [Transactional]
        public void CreateBlogTransactional([FromBody] CreateBlogDto createBlogDto)
        {
            blogService.CreateBlogTransactional(createBlogDto);
        }


        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _blogRepository.DeleteAsync(r => r.Id == id);
        }
    }
}