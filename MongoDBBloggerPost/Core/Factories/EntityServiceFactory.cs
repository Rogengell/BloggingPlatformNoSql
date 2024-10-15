using MongoDBBloggerPost.Core.Helpers;
using MongoDBBloggerPost.Core.MongoClient;
using MongoDBBloggerPost.Core.Repositories;
using MongoDBBloggerPost.Core.Services;

namespace MongoDBBloggerPost.Core.Factories
{
    public static class EntityServiceFactory<T> where T : IBaseEntity
    {
        public static EntityService<T> Create(CollectionName collectionName)
        {
            var client = new Client("mongodb://localhost:27017");
            var repository = new EntityRepository<T>(client, collectionName.ToString());
            return new EntityService<T>(repository);
        }
    }
}