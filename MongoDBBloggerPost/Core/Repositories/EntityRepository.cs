using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBBloggerPost.Core.Helpers;
using MongoDBBloggerPost.Core.MongoClient;
using MongoDBBloggerPost.Model;

namespace MongoDBBloggerPost.Core.Repositories
{
    public class EntityRepository<T> where T : IBaseEntity
    {
        private readonly Client _client;
        public readonly string _databaseName;
        public readonly string _collectionName;

        public EntityRepository(Client client, string collectionName, string databaseName = "MongoDBBloggerPost")
        {
            _client = client;
            _databaseName = databaseName;
            _collectionName = collectionName;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            var collection = _client.Collection<T>(_databaseName, _collectionName);

            return await collection.Find(x => x._id == objectId).FirstOrDefaultAsync();
        }


        public async Task InsertAsync(T item)
        {
            var collection = _client.Collection<T>(_databaseName, _collectionName);
            await collection.InsertOneAsync(item);
        }

        // public void InsertMany(IEnumerable<T> items)
        // {
        //     var collection = _client.Collection<T>(_databaseName, _collectionName);
        //     collection.InsertMany(items);
        // }

        public async Task UpdateAsync(T item)
        {
            var collection = _client.Collection<T>(_databaseName, _collectionName);
            await collection.ReplaceOneAsync(x => x._id == item._id, item);
        }

        public async Task DeleteAsync(T item)
        {
            var collection = _client.Collection<T>(_databaseName, _collectionName);
            await collection.DeleteOneAsync(x => x._id == item._id);
        }
    }
}