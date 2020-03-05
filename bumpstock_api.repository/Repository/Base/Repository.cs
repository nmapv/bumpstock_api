using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace bumpstock_api.repository.Repository.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IDbTransaction _transaction { get; private set; }
        protected IDbConnection _connection { get { return _transaction.Connection; } }

        public Repository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task Add(TEntity entity)
        {
            await _connection.InsertAsync(entity, _transaction);
        }

        public async Task Delete(TEntity entity)
        {
            await _connection.DeleteAsync(entity, _transaction);
        }

        public async Task<TEntity> Get(int id)
        {
            return await _connection.GetAsync<TEntity>(id, _transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _connection.GetAllAsync<TEntity>();
        }

        public async Task Update(TEntity entity)
        {
            await _connection.UpdateAsync(entity, _transaction);
        }
    }
}
