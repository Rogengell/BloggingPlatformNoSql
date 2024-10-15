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

        [HttpGet("GetComments")]
        public CommentsModel GetComments(string id)
        {
            return _entityService.GetById(id);
        }

        [HttpPost("SaveComment")]
        public void SaveComment(CommentsModel comment, string postId)
        {
            _entityService.Save(comment);
        }

        [HttpPut("UpdateComment")]
        public void UpdateComment(CommentsModel comment)
        {
            _entityService.Update(comment);
        }

        [HttpDelete("DeleteComment")]
        public void DeleteComment(string id)
        {
            _entityService.Delete(_entityService.GetById(id));
        }
    }
}