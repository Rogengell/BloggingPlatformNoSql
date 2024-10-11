using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBBloggerPost.Model
{
    public class PostsModel
    {
        public string _id { get; set; }
        // public string userId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public List<string> commentIds { get; set; }
    }
}