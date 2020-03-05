using bumpstock_api.infrastructure.Validation;
using System;

namespace bumpstock_api.entity.Entity.Base
{
    public abstract class BaseEntity : Notifiable
    {
        public int id { get; protected set; }
        public DateTime? register_date { get; protected set; }
    }
}
