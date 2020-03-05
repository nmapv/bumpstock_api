using bumpstock_api.entity.Entity.Base;
using System;

namespace bumpstock_api.entity.Entity.Game
{
    public enum Competitive { dust = 1, inferno = 2, mirage = 3, nuke = 4, overpass = 5, train = 6, vertigo = 7 }

    public class Map : BaseEntity
    {
        public Competitive name { get; private set; }

        public Map(int? id)
        {
            if (!id.HasValue) return;
            this.id = id.Value;
        }

        public Map(int id, int name, DateTime? register_date)
        {
            this.id = id;
            this.name = (Competitive)name;
            this.register_date = register_date ?? DateTime.Now;
        }
    }
}
