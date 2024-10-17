using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBBloggerPost.Model
{
    public class BlogsModel : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string? id { get; set; }
        public string? authorId { get; set; }
        public string? blogName { get; set; }
        public string description { get; set; } = "";
        public List<string>? postIds { get; set; }
    }
}