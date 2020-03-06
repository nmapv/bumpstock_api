using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bumpstock_api.repository.Repository.Public
{
    public class ActivateContactRepository : Repository<ActivateContact>, IActivateContactRepository
    {
        public ActivateContactRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task CleanActivateContact(int contact_id, IEnumerable<int> person_ids)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@contact_id", contact_id);
            parameters.Add("@person_id", person_ids.ToArray());

            await _connection.QueryAsync(@"delete ActivateContact where contact_id=@contact_id and person_id in @person_id", param: parameters, transaction: _transaction);
        }

        public async Task<IEnumerable<ActivateContact>> GetActivateContactByContact(ActivateContact activateContact)
        {
            return await _connection.QueryAsync<ActivateContact>(@"select * from ActivateContact where contact_id=@contact_id", param: new { activateContact.contact_id }, transaction: _transaction);
        }
    }
}
