using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
            try
            {
                return await _postService.GetById(id);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting post" + ex.Message);
                throw;
            }
        }

        [HttpGet("GetAllPosts")]
        public async Task<List<PostsModel>> GetAllPosts()
        {
            try
            {
                return await _postService.GetAll();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting all posts" + ex.Message);
                throw;
            }
        }

        [HttpGet("GetAllPostComments")]
        public async Task<List<CommentsModel>> GetPostComments(string id)
        {
            try
            {
                var post = await _postService.GetById(id);
                var comments = new List<CommentsModel>();
                if (post.commentIds == null)
                {
                    throw new ArgumentNullException(nameof(post.commentIds));
                }
                
                foreach (var commentId in post.commentIds)
                {
                    comments.Add(await _commentService.GetById(commentId.ToString()));
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
        public async Task SavePost(string userId, PostsModel post)
        {
            try
            {
                if (post == null)
                {
                    throw new ArgumentNullException(nameof(post));
                }
                post._id = ObjectId.GenerateNewId();
                post.id = post._id.ToString();
                post.userId = userId;
                await _postService.Save(post);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while saving post" + ex.Message);
                throw;
            }
        }

        [HttpPut("UpdatePost")]
        public async Task UpdatePost(PostsModel post)
        {
            try
            {
                if (post == null)
                {
                    throw new ArgumentNullException(nameof(post));
                }
                await _postService.Update(post);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while updating post" + ex.Message);
                throw;
            }
        }

        [HttpDelete("DeletePost")]
        public async Task DeletePost(PostsModel post)
        {
            try
            {
                if (post == null)
                {
                    throw new ArgumentNullException(nameof(post));
                }
                await _postService.Delete(post);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while deleting post" + ex.Message); 
                throw;
            }
        }
    }
}