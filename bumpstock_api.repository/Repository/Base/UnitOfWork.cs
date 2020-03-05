using System.Data;
using System.Data.SqlClient;

namespace bumpstock_api.repository.Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        //private IAdministratorRepository _administratorRepository;

        //public IAdministratorRepository administratorRepository
        //{
        //    get { return _administratorRepository ?? (_administratorRepository = new AdministratorRepository(openconnection())); }
        //}

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
                resetRepositories();
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null)
                    _transaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                Dispose();
                resetRepositories();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        private IDbTransaction openconnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        private void resetRepositories()
        {
            //_administratorRepository = null;

        }
    }
}
