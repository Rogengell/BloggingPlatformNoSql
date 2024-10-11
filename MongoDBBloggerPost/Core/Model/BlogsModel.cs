using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBBloggerPost.Model
{
    public class BlogsModel
    {
        public string _id { get; set; }
        public string blogName { get; set; }
        public string description { get; set; } = "";
        public List<string> postIds { get; set; }
    }
}