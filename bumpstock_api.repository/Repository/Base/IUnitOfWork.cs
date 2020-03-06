using bumpstock_api.repository.Repository.Public;
using System;

namespace bumpstock_api.repository.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        IContactRepository contactRepository { get; }
        IPersonRepository personRepository { get; }
        IActivateContactRepository activateContactRepository { get; }
    }
}