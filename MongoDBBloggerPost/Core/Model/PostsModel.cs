using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBBloggerPost.Model
{
    public class PostsModel : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string? id { get; set; }
        public string? userId { get; set; }
        public string userName { get; set; } = "";
        public string? title { get; set; }
        public string? content { get; set; }
        public List<string>? commentIds { get; set; }
    }
}