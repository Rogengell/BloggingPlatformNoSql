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
    public class CommentsController : ControllerBase
    {
        private readonly EntityService<CommentsModel> _commentsService;
        private readonly EntityService<PostsModel> _postService;

        public CommentsController(EntityService<CommentsModel> commentsService, EntityService<PostsModel> postService)
        {
            _commentsService = commentsService;
            _postService = postService;
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
        public async Task SaveComment(string userId, string PostId, CommentsModel comment)
        {
            try
            {
                if (comment == null)
                {
                    throw new ArgumentNullException(nameof(comment));
                }
                comment._id = ObjectId.GenerateNewId();
                comment.id = comment._id.ToString();
                comment.userId = userId;

                var post = await _postService.GetById(PostId);

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