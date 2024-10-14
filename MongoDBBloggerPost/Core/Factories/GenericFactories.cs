using MongoDBBloggerPost.Core.MongoClient;
using MongoDBBloggerPost.Core.Repositories;
using MongoDBBloggerPost.Core.Services;
using MongoDBBloggerPost.Model;

namespace MongoDBBloggerPost.Core.Factories
{
    public static class GenericFactories<T> where T : IBaseEntity
    {
        public static GenericService<T> Create()
        {
            var client = new Client("mongodb://localhost:27017");
            var repository = new GenericRepository<T>(client, "Users");
            return new GenericService<T>(repository);
        }
    }
}