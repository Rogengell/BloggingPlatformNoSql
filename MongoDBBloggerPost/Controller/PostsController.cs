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
        private readonly EntityService<PostsModel> _postService;
        private readonly EntityService<CommentsModel> _commentService;
        private readonly EntityService<BlogsModel> _blogService;
        
        private readonly Services.Client _client;

        public PostsController(EntityService<PostsModel> entityService, EntityService<BlogsModel> blogService, EntityService<CommentsModel> commentService,  Services.Client client)
        {
            _postService = entityService;
            _commentService = commentService;
            _blogService = blogService;
            _client = client;
            _client.Connect();
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
        public async Task SavePost(string userId, string blogId, PostsModel post)
        {
            try
            {
                if (post == null)
                {
                    throw new ArgumentNullException(nameof(post));
                }


                post._id = ObjectId.GenerateNewId().ToString();
                post.id = post._id.ToString();
                post.userId = userId;


                var blog = await _blogService.GetById(blogId);
                if (blog.postIds == null)
                {
                    blog.postIds = new List<string>();
                }
                blog.postIds?.Add(post.id);
                await _blogService.Update(blog);

                await _postService.Save(post);

                await CashUpdate(blogId);
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

                if (post.commentIds != null)
                {
                    foreach (var postId in post.commentIds)
                    {
                        var comment = await _commentService.GetById(postId);
                        await _commentService.Delete(comment);
                    }
                }
                await _postService.Delete(post);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while deleting post" + ex.Message);
                throw;
            }
        }

        private async Task CashUpdate(string id)
        {
            await _client.CashUpdatePosts(id);
        }
    }
}