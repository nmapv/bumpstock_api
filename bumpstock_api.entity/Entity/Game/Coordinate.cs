using bumpstock_api.entity.Entity.Base;

namespace bumpstock_api.entity.Entity.Game
{
    public class Coordinate : BaseEntity
    {
        public double lat { get; private set; }
        public double lng { get; private set; }

        public Coordinate(int? id)
        {
            if (!id.HasValue) return;
            this.id = id.Value;
        }
    }
}
