using bumpstock_api.entity.Entity.Base;
using bumpstock_api.infrastructure.Security;
using bumpstock_api.infrastructure.Validation.Rules;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bumpstock_api.entity.Entity.Public
{
    [Table("Contact")]
    public class Contact : BaseEntity
    {
        public string email { get; private set; }
        public string password { get; private set; }

        public Contact(int id, string email, string password, DateTime? register_date)
        {
            var contractEmail = new Contract()
                .Requires()
                .IsNotNullOrEmpty(email, this.GetType().Name.ToLower(), "Mail is required")
                .IsEmail(email, this.GetType().Name.ToLower(), "Invalid mail");

            var contractPassword = new Contract()
                .Requires()
                .IsNotNullOrEmpty(password, this.GetType().Name.ToLower(), "Password is required")
                .HasMinLen(password, 8, this.GetType().Name.ToLower(), "Invalid password");

            AddNotifications(contractEmail);
            AddNotifications(contractPassword);

            this.id = id;
            this.email = email;
            this.password = password.Encrypt();
            this.register_date = register_date ?? DateTime.Now;
        }

        public void ContactAlreadyExists(bool exists)
        {
            var contract = new Contract()
                .IsTrue(exists, this.GetType().Name.ToLower(), "Contact already exists");

            AddNotifications(contract);
        }

        public bool ValidatePassowrd(string password)
        {
            if (this.password.Decrypt().Equals(password))
                return true;

            return false;
        }
    }
}
