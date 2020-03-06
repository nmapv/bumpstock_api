using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using System.Data;

namespace bumpstock_api.repository.Repository.Public
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
