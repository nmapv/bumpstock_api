using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using System.Data;

namespace bumpstock_api.repository.Repository.Public
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
