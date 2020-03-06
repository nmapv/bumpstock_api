using bumpstock_api.entity.Entity.Public;
using bumpstock_api.repository.Repository.Base;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bumpstock_api.repository.Repository.Public
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task CleanPerson(IEnumerable<int> person_ids)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", person_ids.ToArray());

            await _connection.QueryAsync(@"delete Person where id in @id", param: parameters, transaction: _transaction);
        }
    }
}
