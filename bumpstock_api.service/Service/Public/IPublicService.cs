using bumpstock_api.entity.Entity.Public;
using System.Threading.Tasks;

namespace bumpstock_api.service.Service.Public
{
    public interface IPublicService
    {
        Task<Contact> AddContact(Contact contact);
        Task<ActivateContact> ActivateContact(ActivateContact activateContact);
        Task<Person> Signin(string hash);
    }
}