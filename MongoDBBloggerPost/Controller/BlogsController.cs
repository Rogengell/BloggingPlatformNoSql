using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBBloggerPost.Core.MongoClient;
using MongoDBBloggerPost.Core.Services;
using MongoDBBloggerPost.Model;

namespace MongoDBBloggerPost.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : ControllerBase
    {
        //TODO: Add Controller for Blog
        private readonly EntityService<BlogsModel> _blogService;
        private readonly EntityService<PostsModel> _postService;
        private readonly EntityService<UsersModel> _userService;

        public BlogsController(EntityService<BlogsModel> blogService, EntityService<PostsModel> postService , EntityService<UsersModel> userService)
        {
            _blogService = blogService;
            _postService = postService;
            _userService = userService;
        }

        [HttpGet("GetBlog")]
        public async Task<BlogsModel> GetBlog(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                return await _blogService.GetById(id);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting blog" + ex.Message);
                throw;
            }
        }

        [HttpGet("GetAllBlogPosts")]
        public async Task<List<PostsModel>> GetBlogs(string id)
        {
            try
            {
                var blog = await _blogService.GetById(id);
                var posts = new List<PostsModel>();

                if (blog.postIds == null)
                {
                   return posts;
                }

                foreach (var postId in blog.postIds)
                {
                    posts.Add(await _postService.GetById(postId.ToString()));
                }

                return posts;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("something went wrong while getting blog posts" + ex.Message);
                throw;
            }
        }

        [HttpPost("SaveBlog")]
        public async Task SaveBlog(string userId, BlogsModel blog)
        {
            try
            {
                if (blog == null)
                {
                    throw new ArgumentNullException(nameof(blog));
                }

                blog._id = ObjectId.GenerateNewId();
                blog.id = blog._id.ToString();
                blog.authorId = userId;

                var user = await _userService.GetById(userId);

                if (user.blogIds == null)
                {
                    user.blogIds = new List<string>();
                }
                user.blogIds.Add(blog.id);

                await _blogService.Save(blog);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while saving blog" + ex.Message);
                throw;
            }
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