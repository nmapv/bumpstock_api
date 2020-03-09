using bumpstock_api.entity.Entity.Public;
using bumpstock_api.infrastructure.Security;
using bumpstock_api.repository.Repository.Base;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace bumpstock_api.service.Service.Public
{
    public class PublicService : IPublicService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActivateContact> ActivateContact(ActivateContact activateContact)
        {
            using (_unitOfWork)
            {
                var lst = await _unitOfWork.activateContactRepository.GetActivateContactByContact(activateContact);
                var res = lst.Where(x => x.activation_date == null).OrderByDescending(x => x.register_date).FirstOrDefault();

                if (res == null || !res.Activate(activateContact.activation_code))
                    return null;

                //cria uma nova pessoa na base
                var person = new Person(res.hash);
                await _unitOfWork.personRepository.Add(person);
                _unitOfWork.Commit();

                //vincula a nova pessoa a ativação
                res.SetPerson(person.id);
                await _unitOfWork.activateContactRepository.Update(res);
                activateContact = res;
                _unitOfWork.Commit();

                //limpa as ativações anteriores
                var person_ids = lst.Where(x => x.person_id != activateContact.person_id).Select(x => x.person_id);
                await _unitOfWork.activateContactRepository.CleanActivateContact(activateContact.contact_id, person_ids);
                _unitOfWork.Commit();
                await _unitOfWork.personRepository.CleanPerson(person_ids);
                _unitOfWork.Commit();
            }

            return activateContact;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            using (_unitOfWork)
            {
                var res = await _unitOfWork.contactRepository.GetContactByEmail(contact);

                if (res != null)
                    contact = res;
                else
                    await _unitOfWork.contactRepository.Add(contact);

                _unitOfWork.Commit();

                var activeContact = new ActivateContact(contact.id);
                await _unitOfWork.activateContactRepository.Add(activeContact);

                _unitOfWork.Commit();
            }

            return contact;
        }

        public async Task<Person> Signin(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                return null;

            var activateproof = !string.IsNullOrEmpty(hash.Decrypt()) ? JsonConvert.DeserializeObject<ActivateContact>(hash.Decrypt()) : null;

            if (activateproof == null)
                return null;

            using (_unitOfWork)
            {
                var activate = await _unitOfWork.activateContactRepository.Get(activateproof.id);

                if (activate == null || !activate.hash.Equals(hash))
                    return null;

                var person = await _unitOfWork.personRepository.Get(activate.person_id);
                if (person == null)
                    return null;

                return person;
            }
        }
    }
}
