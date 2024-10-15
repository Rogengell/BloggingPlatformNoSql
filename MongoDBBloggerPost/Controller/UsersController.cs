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
        public UsersModel GetUser(string id)
        {
            return _entityService.GetById(id);
        }

        [HttpPost("SaveUser")]
        public void SaveUser(UsersModel user)
        {
            _entityService.Save(user);
        }

        [HttpPut("UpdateUser")]
        public void UpdateUser(UsersModel user)
        {
            _entityService.Update(user);
        }

        [HttpDelete("DeleteUser")]
        public void DeleteUser(UsersModel user)
        {
            _entityService.Delete(user);
        }
    }
}