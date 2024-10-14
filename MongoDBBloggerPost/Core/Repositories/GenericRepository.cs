using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBBloggerPost.Core.MongoClient;

namespace MongoDBBloggerPost.Core.Repositories
{
    public class GenericRepository<T> where T : IBaseEntity
    {
        private readonly Client _client;
        public readonly string _databaseName;
        public readonly string _collectionName;

        public GenericRepository(Client client, string collectionName, string databaseName = "MongoDBBloggerPost")
        {
            _client = client;
            _databaseName = databaseName;
            _collectionName = collectionName;
        }

        public T GetById(string id)
        {
            var objectId = new ObjectId(id);
            var collection = _client.Collection<T>(_databaseName, _collectionName);

            return collection.Find(x => x._id == objectId).FirstOrDefault();
        }

        public void InsertOne(T item)
        {
            var collection = _client.Collection<T>(_databaseName, _collectionName);
            collection.InsertOne(item);
        }

        public void InsertMany(IEnumerable<T> items)
        {
            var collection = _client.Collection<T>(_databaseName, _collectionName);
            collection.InsertMany(items);
        }
    }
}