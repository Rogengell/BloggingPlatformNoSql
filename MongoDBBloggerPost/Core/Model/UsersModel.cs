using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBBloggerPost.Model
{
    public class UsersModel : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string? id { get; set; }
        public string? userName { get; set; }
        public string email { get; set; } = "";
        public string? password { get; set; }
        public List<string>? blogIds { get; set; }
    }
}