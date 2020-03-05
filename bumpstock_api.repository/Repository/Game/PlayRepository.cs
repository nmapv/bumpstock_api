using bumpstock_api.entity.Entity.Game;
using bumpstock_api.repository.Repository.Base;
using System.Data;

namespace bumpstock_api.repository.Repository.Game
{
    public class PlayRepository : Repository<Play>, IPlayRepository
    {
        public PlayRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
