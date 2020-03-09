using bumpstock_api.entity.Entity.Base;
using bumpstock_api.infrastructure.Validation.Rules;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bumpstock_api.entity.Entity.Public
{
    [Table("Contact")]
    public class Contact : BaseEntity
    {
        public string email { get; private set; }

        public Contact(int id, string email, DateTime? register_date)
        {
            var contractEmail = new Contract()
                .Requires()
                .IsEmailOrEmpty(email, this.GetType().Name.ToLower(), "Invalid mail");

            AddNotifications(contractEmail);

            this.id = id;
            this.email = email;
            this.register_date = register_date ?? DateTime.Now;
        }
    }
}
