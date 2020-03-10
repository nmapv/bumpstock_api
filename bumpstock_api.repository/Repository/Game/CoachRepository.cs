using bumpstock_api.entity.Entity.Game;
using bumpstock_api.repository.Repository.Base;
using System.Data;

namespace bumpstock_api.repository.Repository.Game
{
    public class CoachRepository : Repository<Coach>, ICoachRepository
    {
        public CoachRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
