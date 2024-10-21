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
        private readonly EntityService<CommentsModel> _entityService;

        public CommentsController(EntityService<CommentsModel> entityService)
        {
            _entityService = entityService;
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
                
                return await _entityService.GetById(id);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting comment "+ ex.Message);
                throw;
            }
        }


        [HttpGet("GetAllComments")]
        public async Task<List<CommentsModel>> GetAllComments()
        {
            try
            {
                return await _entityService.GetAll();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while getting all comments " + ex.Message);
                throw;
            }
        }

        [HttpPost("SaveComment")]
        public async Task SaveComment(string userId, CommentsModel comment)
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
                await _entityService.Save(comment);
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
                await _entityService.Update(comment);
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
                await _entityService.Delete(GetComments(id).Result);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("something went wrong while deleting comment " + ex.Message);
                throw;
            }
        }
    }
}