using bumpstock_api.entity.Entity.Base;
using bumpstock_api.entity.Entity.Public;
using bumpstock_api.infrastructure.Validation.Rules;
using System;

namespace bumpstock_api.entity.Entity.Game
{
    public class Player : BaseEntity
    {
        public Person person { get; private set; }
        public Team team { get; private set; }

        public Player(int id, Person person, Team team, DateTime? register_date)
        {
            var contractPerson = new Contract()
                .Requires()
                .IsNotNull(person, this.GetType().Name.ToLower(), "Person is required");

            var contractTeam = new Contract()
                .Requires()
                .IsNotNull(team, this.GetType().Name.ToLower(), "Team is required");

            this.GetType().Name.ToLower();

            AddNotifications(contractPerson);
            AddNotifications(contractTeam);

            this.id = id;
            this.person = person;
            this.team = team;
            this.register_date = register_date ?? DateTime.Now;
        }
    }
}
