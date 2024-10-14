using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBBloggerPost.Model
{
    public class BlogsModel : IBaseEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string? blogName { get; set; }
        public string description { get; set; } = "";
        public List<ObjectId>? postIds { get; set; }
    }
}