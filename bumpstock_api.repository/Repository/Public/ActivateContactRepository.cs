using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using System.Data;

namespace bumpstock_api.repository.Repository.Public
{
    public class ActivateContactRepository : Repository<ActivateContact>, IActivateContactRepository
    {
        public ActivateContactRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
