using bumpstock_api.entity.Entity.Base;
using System;

namespace bumpstock_api.entity.Entity.Game
{
    public class Coordinate : BaseEntity
    {
        public double lat { get; private set; }
        public double lng { get; private set; }
        public int map_id { get; private set; }

        public Coordinate(int id, double lat, double lng, int map_id, DateTime? register_date)
        {
            this.id = id;
            this.lat = lat;
            this.lng = lng;
            this.map_id = map_id;
            this.register_date = register_date ?? DateTime.Now;
        }
    }
}
