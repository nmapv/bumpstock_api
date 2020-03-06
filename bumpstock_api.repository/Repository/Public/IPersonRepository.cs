using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bumpstock_api.repository.Repository.Public
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task CleanPerson(IEnumerable<int> person_ids);
    }
}