using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDBBloggerPost.Core.Services;
using MongoDBBloggerPost.Model;

namespace MongoDBBloggerPost.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : ControllerBase
    {
        //TODO: Add Controller for Blog
        private readonly EntityService<BlogsModel> _entityService;

        public BlogsController(EntityService<BlogsModel> entityService)
        {
            _entityService = entityService;
        }

        [HttpGet("GetBlog")]
        public BlogsModel GetBlog(string id)
        {
            return _entityService.GetById(id);
        }

        [HttpPost("SaveBlog")]
        public void SaveBlog(BlogsModel blog, string userId)
        {
            _entityService.Save(blog);
        }
    }
}