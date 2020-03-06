using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bumpstock_api.repository.Repository.Public
{
    public interface IActivateContactRepository : IRepository<ActivateContact>
    {
        Task<IEnumerable<ActivateContact>> GetActivateContactByContact(ActivateContact activateContact);
        Task CleanActivateContact(int contact_id, IEnumerable<int> person_ids);
    }
}