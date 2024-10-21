using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDBBloggerPost.Core.MongoClient;
using MongoDBBloggerPost.Core.Services;
using MongoDBBloggerPost.Model;

namespace MongoDBBloggerPost.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly EntityService<BlogsModel> _blogService;
        private readonly EntityService<PostsModel> _postService;

        public BlogsController(EntityService<BlogsModel> blogService, EntityService<PostsModel> postService)
        {
            _blogService = blogService;
            _postService = postService;
        }

        [HttpGet("GetBlog")]
        public async Task<BlogsModel> GetBlog(string id)
        {
            return await _blogService.GetById(id);
        }

        [HttpGet("GetAllBlogPosts")]
        public async Task<List<PostsModel>> GetBlogPosts(string id)
        {
            try
            {
                var blog = await _blogService.GetById(id);
                var posts = new List<PostsModel>();

                if (blog.postIds != null)
                {
                    foreach (var postId in blog.postIds)
                    {
                        posts.Add(await _postService.GetById(postId.ToString()));
                    }
                }

                return posts;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPost("SaveBlog")]
        public async Task SaveBlog(BlogsModel blog)
        {
            await _blogService.Save(blog);
        }

        [HttpPut("UpdateBlog")]
        public async Task UpdateBlog(BlogsModel blog)
        {
            await _blogService.Update(blog);
        }

        [HttpDelete("DeleteBlog")]
        public async Task DeleteBlog(BlogsModel blog)
        {
            await _blogService.Delete(blog);
        }
    }
}