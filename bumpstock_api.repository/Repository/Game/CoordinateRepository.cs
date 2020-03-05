using bumpstock_api.entity.Entity.Game;
using bumpstock_api.repository.Repository.Base;
using System.Data;

namespace bumpstock_api.repository.Repository.Game
{
    public class CoordinateRepository : Repository<Coordinate>, ICoordinateRepository
    {
        public CoordinateRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
