using bumpstock_api.entity.Entity.Base;
using bumpstock_api.infrastructure.Validation.Rules;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace bumpstock_api.entity.Entity.Public
{
    [Table("Person")]
    public class Person : BaseEntity
    {
        public string first_name { get; private set; }
        public string last_name { get; private set; }
        public string hash { get; private set; }

        [Write(false)]
        public string name { get { return string.Format("{0} {1}", first_name, last_name); } }

        [Write(false)]
        public string role { get; set; }

        public Person(int id, string first_name, string last_name, string hash, DateTime? register_date)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.hash = hash;
            this.register_date = register_date;
        }

        public Person(List<Claim> claims)
        {
            var contractClaims = new Contract()
                .Requires()
                .IsNotNull(claims, this.GetType().Name.ToLower(), "Invalid token for person");

            var claim_id = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var claim_first_name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var claim_last_name = claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName);
            var claim_hash = claims.FirstOrDefault(x => x.Type == ClaimTypes.Hash);
            var claim_role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            contractClaims
                .IsNotNull(claim_id, this.GetType().Name.ToLower(), "Invalid token for person")
                .IsNotNull(claim_first_name, this.GetType().Name.ToLower(), "Invalid token for person")
                .IsNotNull(claim_last_name, this.GetType().Name.ToLower(), "Invalid token for person")
                .IsNotNull(claim_hash, this.GetType().Name.ToLower(), "Invalid token for person")
                .IsNotNull(claim_role, this.GetType().Name.ToLower(), "Invalid token for person")
                .IsFalse(!int.TryParse(claim_id.Value, out int _id), this.GetType().Name.ToLower(), "Invalid id for person");

            AddNotifications(contractClaims);

            this.id = _id;
            this.first_name = claim_first_name.Value;
            this.last_name = claim_last_name.Value;
            this.hash = claim_hash.Value;
        }

        public Person(string hash)
        {
            var contractHash = new Contract()
              .Requires()
              .IsFalse(hash.Length <= 0, this.GetType().Name.ToLower(), "Invalid hash in person")
              .IsNotNullOrEmpty(hash, this.GetType().Name.ToLower(), "Hash is required in person");

            AddNotifications(contractHash);
            this.hash = hash;
            this.register_date = DateTime.Now;
        }

        public Person(string first_name, string last_name, string hash)
        {
            var contractFirstName = new Contract()
                .Requires()
                .HasMaxLen(first_name, (40), this.GetType().Name.ToLower(), "Invalid first_name in person")
                .HasMinLen(first_name, (5), this.GetType().Name.ToLower(), "Invalid first_name in person");

            var contractLastName = new Contract()
                .Requires()
                .HasMaxLen(last_name, (40), this.GetType().Name.ToLower(), "Invalid last_name in person")
                .HasMinLen(last_name, (5), this.GetType().Name.ToLower(), "Invalid last_name in person");

            var contractHash = new Contract()
                .Requires()
                .IsFalse(hash.Length <= 0, this.GetType().Name.ToLower(), "Invalid hash in person")
                .IsNotNullOrEmpty(hash, this.GetType().Name.ToLower(), "Hash is required in person");

            AddNotifications(contractFirstName);
            AddNotifications(contractLastName);
            AddNotifications(contractHash);

            this.first_name = first_name.Trim();
            this.last_name = last_name.Trim();
            this.hash = hash;
        }
    }
}
