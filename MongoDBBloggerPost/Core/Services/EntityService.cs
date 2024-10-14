using MongoDBBloggerPost.Core.Repositories;
using MongoDBBloggerPost.Model;

namespace MongoDBBloggerPost.Core.Services
{
    public class EntityService<T> where T : IBaseEntity
    {
        private readonly EntityRepository<T> _repository;

        public EntityService(EntityRepository<T> repository)
        {
            _repository = repository;
        }

        public T GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void Save(T item)
        {
            _repository.InsertOne(item);
        }

        public void SaveMany(IEnumerable<T> items)
        {
            _repository.InsertMany(items);
        }
    }
}