using bumpstock_api.entity.Entity.Base;
using Dapper.Contrib.Extensions;

namespace bumpstock_api.entity.Entity.Game
{
    public class Play : BaseEntity
    {
        public string url { get; private set; }
        public int? map_id { get { return map?.id; } set { this.map = new Map(value); } }
        public int? coordinate_id { get { return coordinate?.id; } set { this.coordinate = new Coordinate(value); } }
        

        [Write(false)]
        public Map map { get; private set; }
        [Write(false)]
        public Coordinate coordinate { get; private set; }
    }
}
