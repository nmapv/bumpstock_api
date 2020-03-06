using bumpstock_api.entity.Entity.Base;
using bumpstock_api.infrastructure.Validation.Rules;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bumpstock_api.entity.Entity.Public
{
    [Table("Contact")]
    public class Contact : BaseEntity
    {
        public int? ddi { get; private set; }
        public int? ddd { get; private set; }
        public long? cellphone { get; private set; }

        public Contact(int id, int? ddi, int? ddd, long? cellphone, DateTime? register_date)
        {
            var contractDDI = new Contract()
                .Requires()
                .IsNullOrNullable(ddi, "contact", "DDI is required in contact")
                .IsFalse(ddi <= 0, "contact", "Invalid DDI in contact");

            var contractDDD = new Contract()
                .Requires()
                .IsNullOrNullable(ddd, "contact", "DDD is required in contact")
                .IsFalse(ddd <= 0, "contact", "Invalid DDD in contact");

            var contractCellphone = new Contract()
                .Requires()
                .IsFalse(cellphone == null, "contact", "Cellphone is required in contact")
                .IsFalse(cellphone <= 0, "contact", "Invalid Cellphone in contact");

            AddNotifications(contractDDI);
            AddNotifications(contractDDD);
            AddNotifications(contractCellphone);

            this.id = id;
            this.ddi = ddi;
            this.ddd = ddd;
            this.cellphone = cellphone;
            this.register_date = register_date ?? DateTime.Now;
        }
    }
}
