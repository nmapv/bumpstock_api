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
            var contractDDI = new Contract()
                .Requires()
                .IsEmailOrEmpty(email, "contact", "email is required in contact")
                .IsEmail(email, "contact", "email is invalid");

            AddNotifications(contractDDI);

            this.id = id;
            this.email = email;
            this.register_date = register_date ?? DateTime.Now;
        }
    }
}
