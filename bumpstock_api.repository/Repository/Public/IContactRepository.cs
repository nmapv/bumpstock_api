using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using System.Threading.Tasks;

namespace bumpstock_api.repository.Repository.Public
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact> GetContactByNumber(Contact contact);
    }
}