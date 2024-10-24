using System;
using System.Collections.Generic;
using System.Data;
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
    public class UsersController : ControllerBase
    {
        private readonly EntityService<UsersModel> _userService;
        private readonly EntityService<BlogsModel> _blogService;
        private readonly EntityService<PostsModel> _postService;
        private readonly EntityService<CommentsModel> _commentService;

        public UsersController(EntityService<UsersModel> entityService, EntityService<BlogsModel> blogService, EntityService<PostsModel> postService, EntityService<CommentsModel> commentService)
        {
            _userService = entityService;
            _blogService = blogService;
            _postService = postService;
            _commentService = commentService;
        }

        [HttpGet("GetUsers")]
        public async Task<UsersModel> GetUser(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                return await _userService.GetById(id);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting user" + ex.Message);
                throw;
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<List<UsersModel>> GetAllUsers()
        {
            try
            {
                return await _userService.GetAll();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting all users" + ex.Message);
                throw;
            }
        }

        [HttpGet("GetUserBlogs")]
        public async Task<List<BlogsModel>> GetUserBlogs(string id)
        {
            try
            {
                var user = await _userService.GetById(id);
                var blogs = new List<BlogsModel>();

                if (user.blogIds == null)
                {
                    return blogs;
                }

                foreach (var blogId in user.blogIds)
                {
                    blogs.Add(await _blogService.GetById(blogId));
                }

                return blogs;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting user blogs" + ex.Message);
                throw;
            }
        }

        [HttpPost("SaveUser")]
        public async Task SaveUser(UsersModel user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                user._id = ObjectId.GenerateNewId().ToString();
                user.id = user._id.ToString();
                await _userService.Save(user);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while saving user" + ex.Message);
                throw;
            }
        }

        [HttpPut("UpdateUser")]
        public async Task UpdateUser(string id, UsersModel user)
        {
            try
            {
                var oldUser = await _userService.GetById(id);
                if (oldUser != null && oldUser.userName != user.userName)
                {
                    // Update posts
                    var posts = await _postService.GetAll();
                    var updatedPosts = posts.Where(p => p.userId == oldUser.id).ToList();
                    foreach (var post in updatedPosts)
                    {
                        post.userName = user.userName;
                        await _postService.Update(post);
                    }

                    // Update comments
                    var comments = await _commentService.GetAll();
                    var updatedComments = comments.Where(c => c.userId == oldUser.id).ToList();
                    foreach (var comment in updatedComments)
                    {
                        comment.userName = user.userName;
                        await _commentService.Update(comment);
                    }
                }

                user._id = id;
                await _userService.Update(user);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Invalid ObjectId: " + ex.Message);
                throw;
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task DeleteUser(UsersModel user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                await _userService.Delete(user);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while deleting user" + ex.Message);
            }
        }
    }
}