using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBBloggerPost.Model
{
    public class CommentsModel : IBaseEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public ObjectId userId { get; set; }
        public string userName { get; set; } = "";
        public string? comment { get; set; }
    }
}