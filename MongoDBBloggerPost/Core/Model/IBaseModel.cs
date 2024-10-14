using MongoDB.Bson;

public interface IBaseEntity
{
    ObjectId _id { get; set; }
}