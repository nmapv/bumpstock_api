using bumpstock_api.entity.Entity.Game;
using bumpstock_api.repository.Repository.Base;
using System.Data;

namespace bumpstock_api.repository.Repository.Game
{
    public class MapRepository : Repository<Map>, IMapRepository
    {
        public MapRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
