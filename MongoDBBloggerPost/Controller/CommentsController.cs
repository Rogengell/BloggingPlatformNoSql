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
    public class CommentsController : ControllerBase
    {
        //TODO: Add comments controller
        private readonly EntityService<CommentsModel> _entityService;

        public CommentsController(EntityService<CommentsModel> entityService)
        {
            _entityService = entityService; 
        }

        [HttpGet]
        public CommentsModel GetbyId(string id)
        {
            return _entityService.GetById(id);
        }

        [HttpPost]
        public void SaveComment(CommentsModel comment, string postId)
        {
            _entityService.Save(comment);
        }
    }
}