using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBBloggerPost.Core.Services;
using MongoDBBloggerPost.Model;
using MongoDBBloggerPost.Services;

namespace MongoDBBloggerPost.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly EntityService<CommentsModel> _commentsService;
        private readonly EntityService<PostsModel> _postService;
        private readonly Services.Client _client;

        // TODO : add userId and PostId to update and delete comments
        public CommentsController(EntityService<CommentsModel> commentsService, EntityService<PostsModel> postService, Client client)
        {
            _commentsService = commentsService;
            _postService = postService;
            _client = client;
            _client.Connect();
        }

        [HttpGet("GetComment")]
        public async Task<CommentsModel> GetComments(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                return await _commentsService.GetById(id);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting comment " + ex.Message);
                throw;
            }
        }


        [HttpGet("GetAllComments")]
        public async Task<List<CommentsModel>> GetAllComments()
        {
            try
            {
                return await _commentsService.GetAll();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting all comments " + ex.Message);
                throw;
            }
        }

        [HttpPost("SaveComment")]
        public async Task SaveComment(string userId, string postId, CommentsModel comment)
        {
            try
            {
                if (comment == null)
                {
                    throw new ArgumentNullException(nameof(comment));
                }

                if (!_client.CommentRateLimit(userId, postId))
                {
                    throw new Exception("Comment rate limit exceeded");
                }

                comment._id = ObjectId.GenerateNewId().ToString();
                comment.id = comment._id.ToString();
                comment.userId = userId;

                var post = await _postService.GetById(postId);

                if (post.commentIds == null)
                {
                    post.commentIds = new List<string>();
                }

                post.commentIds.Add(comment.id);
                await _postService.Update(post);

                await _commentsService.Save(comment);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while saving comment " + ex.Message);
                throw;
            }
        }

        [HttpPut("UpdateComment")]
        public async Task UpdateComment(CommentsModel comment)
        {
            try
            {
                if (comment == null)
                {
                    throw new ArgumentNullException(nameof(comment));
                }
                await _commentsService.Update(comment);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while updating comment " + ex.Message);
                throw;
            }
        }

        [HttpDelete("DeleteComment")]
        public async Task DeleteComment(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                await _commentsService.Delete(GetComments(id).Result);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while deleting comment " + ex.Message);
                throw;
            }
        }
    }
}