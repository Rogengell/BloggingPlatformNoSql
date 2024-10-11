using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBBloggerPost.Model
{
    public class UsersModel
    {
        public string _id { get; set; }
        public string userName { get; set; }
        public DateOnly birthDate { get; set; }
        public string email { get; set; } = "";
        public string password { get; set; }
        public List<string> blogIds { get; set; }
    }
}