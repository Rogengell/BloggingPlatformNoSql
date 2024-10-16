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

        public async Task<T> GetById(string id)
        {
            return await _repository.GetByIdAsync(id);
        }


        public async Task Save(T item)
        {
            await _repository.InsertAsync(item);
        }

        public async Task Update(T item)
        {
            await _repository.UpdateAsync(item);
        }

        public async Task Delete(T item)
        {
            await _repository.DeleteAsync(item);
        }
    }
}