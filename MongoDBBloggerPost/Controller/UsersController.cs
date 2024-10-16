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
    public class UsersController : ControllerBase
    {
        //TODO: Add Controller for User
        private readonly EntityService<UsersModel> _entityService;

        public UsersController(EntityService<UsersModel> entityService)
        {
            _entityService = entityService;
        }

        [HttpGet("GetUsers")]
        public async Task<UsersModel> GetUser(string id)
        {
            return await _entityService.GetById(id);
        }

        [HttpPost("SaveUser")]
        public async Task SaveUser(UsersModel user)
        {
            await _entityService.Save(user);
        }

        [HttpPut("UpdateUser")]
        public async Task UpdateUser(UsersModel user)
        {
            await _entityService.Update(user);
        }

        [HttpDelete("DeleteUser")]
        public async Task DeleteUser(UsersModel user)
        {
            await _entityService.Delete(user);
        }
    }
}