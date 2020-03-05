using System;

namespace bumpstock_api.repository.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}