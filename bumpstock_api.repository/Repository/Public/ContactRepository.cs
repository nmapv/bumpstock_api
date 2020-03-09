using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bumpstock_api.repository.Repository.Public
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<Contact> GetContactByEmail(Contact contact)
        {
            var res = await _connection.QueryAsync<Contact>(@"select * from Contact where email=@email", param: new { contact.email }, transaction: _transaction);
            return res.FirstOrDefault();
        }
    }
}
