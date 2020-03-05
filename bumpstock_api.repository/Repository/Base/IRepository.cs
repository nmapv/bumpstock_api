using System.Collections.Generic;
using System.Threading.Tasks;

namespace bumpstock_api.repository.Repository.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity);
    }
}