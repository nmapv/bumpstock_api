using bumpstock_api.entity.Entity.Base;
using System;

namespace bumpstock_api.entity.Entity.Game
{
    public class Play : BaseEntity
    {
        public string url { get; private set; }
        public int coordinate_id { get; private set; }

        public Play(int id, string url, int coordinate_id, DateTime? register_date)
        {
            this.id = id;
            this.url = url;
            this.coordinate_id = coordinate_id;
            this.register_date = register_date ?? DateTime.Now;
        }
    }
}
