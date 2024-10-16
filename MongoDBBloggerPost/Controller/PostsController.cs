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
        private readonly EntityService<PostsModel> _postService;
        private readonly EntityService<CommentsModel> _commentService;

        public PostsController(EntityService<PostsModel> entityService, EntityService<CommentsModel> commentService)
        {
            _postService = entityService;
            _commentService = commentService;
        }

        [HttpGet("GetPost")]
        public async Task<PostsModel> GetPost(string id)
        {
            return await _postService.GetById(id);
        }

        [HttpGet("GetAllPosts")]
        public async Task<List<PostsModel>> GetAllPosts()
        {
            return await _postService.GetAll();
        }

        [HttpGet("GetAllPostComments")]
        public async Task<List<CommentsModel>> GetPostComments(string id)
        {
            try
            {
                var post = await _postService.GetById(id);
                var comments = new List<CommentsModel>();
                if (post.commentIds != null)
                {
                    foreach (var commentId in post.commentIds)
                    {
                        comments.Add(await _commentService.GetById(commentId.ToString()));
                    }
                }
                return comments;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPost("SavePost")]
        public async Task SavePost(PostsModel post)
        {
            await _postService.Save(post);
        }

        [HttpPut("UpdatePost")]
        public async Task UpdatePost(PostsModel post)
        {
            await _postService.Update(post);
        }

        [HttpDelete("DeletePost")]
        public async Task DeletePost(PostsModel post)
        {
            await _postService.Delete(post);
        }
    }
}