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
    public class PostsController : ControllerBase
    {
        //TODO: Add Controller for Posts
        private readonly EntityService<PostsModel> _entityService;

        public PostsController(EntityService<PostsModel> entityService)
        {
            _entityService = entityService;
        }

        [HttpGet]
        public PostsModel GetById(string id)
        {
            return _entityService.GetById(id);
        }

        [HttpPost]
        public void SavePost(PostsModel post, string blogId)
        {
            _entityService.Save(post);
        }
    }
}