using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBBloggerPost.Model
{
    public class UsersModel : IBaseEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string? userName { get; set; }
        public DateOnly birthDate { get; set; }
        public string email { get; set; } = "";
        public string? password { get; set; }
        public List<ObjectId>? blogIds { get; set; }
    }
}                  