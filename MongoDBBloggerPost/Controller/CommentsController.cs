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
        public async Task<CommentsModel> GetComments(string id)
        {
            return await _entityService.GetById(id);
        }

        [HttpPost("SaveComment")]
        public async Task SaveComment(CommentsModel comment, string postId)
        {
            await _entityService.Save(comment);
        }

        [HttpPut("UpdateComment")]
        public async Task UpdateComment(CommentsModel comment)
        {
            await _entityService.Update(comment);
        }

        [HttpDelete("DeleteComment")]
        public async Task DeleteComment(string id)
        {
            await _entityService.Delete(GetComments(id).Result);
        }
    }
}