using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBBloggerPost.Model
{
    public class CommentsModel
    {
        public string _id { get; set; }
        // public string userId { get; set; }
        public string comment { get; set; }
    }
}