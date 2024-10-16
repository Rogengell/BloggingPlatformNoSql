using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBBloggerPost.Model
{
    public class BlogsModel : IBaseEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public ObjectId authorId { get; set; } = ObjectId.Empty;
        public string? blogName { get; set; }
        public string description { get; set; } = "";
        public List<ObjectId>? postIds { get; set; }
    }
}