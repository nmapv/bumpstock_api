using bumpstock_api.entity.Entity.Base;
using bumpstock_api.infrastructure.Validation.Rules;
using System;

namespace bumpstock_api.entity.Entity.Game
{
    public class Team : BaseEntity
    {
        public string name { get; private set; }
        public string logo { get; private set; }
        public bool enabled { get; private set; }

        public Team(int id, string name, string logo, bool enabled, DateTime? register_date)
        {
            var contractPerson = new Contract()
                .Requires()
                .IsNotNullOrEmpty(name, this.GetType().Name.ToLower(), "name is required");

            AddNotifications(contractPerson);

            this.id = id;
            this.name = name;
            this.logo = logo;
            this.enabled = enabled;
            this.register_date = register_date ?? DateTime.Now;
        }
    }
}
