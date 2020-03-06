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

        public async Task<Contact> GetContactByNumber(Contact contact)
        {
            var res = await _connection.QueryAsync<Contact>(@"select * from Contact where ddi=@ddi and ddd=@ddd and cellphone=@cellphone", param: new { contact.ddi, contact.ddd, contact.cellphone }, transaction: _transaction);
            return res.FirstOrDefault();
        }
    }
}
