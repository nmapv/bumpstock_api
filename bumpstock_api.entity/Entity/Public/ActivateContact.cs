using bumpstock_api.entity.Entity.Base;
using bumpstock_api.infrastructure.Security;
using bumpstock_api.infrastructure.Validation.Rules;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;

namespace bumpstock_api.entity.Entity.Public
{
    [Table("ActivateContact")]
    public class ActivateContact : BaseEntity
    {
        public int contact_id { get; private set; }
        public int person_id { get; private set; }
        public int activation_code { get; private set; }
        public DateTime? activation_date { get; private set; }

        [Write(false)]
        public string hash { get { return activation_date == null ? string.Empty : JsonConvert.SerializeObject(new { this.id, this.contact_id, this.activation_code, activation_date = this.activation_date?.ToString("yyyy-MM-ddTHH:mm"), register_date = this.register_date?.ToString("yyyy-MM-ddTHH:mm") }).Encrypt(); } }

        public ActivateContact(int id, int contact_id, int person_id, int activation_code, DateTime? activation_date, DateTime? register_date)
        {
            this.id = id;
            this.contact_id = contact_id;
            this.person_id = person_id;
            this.activation_code = activation_code;
            this.activation_date = activation_date;
            this.register_date = register_date;
        }

        public ActivateContact(int contact_id)
        {
            var contractContactId = new Contract()
                .Requires()
                .IsFalse(contact_id <= 0, "activatecontact", "Invalid contact_id in activate contact");

            AddNotifications(contractContactId);

            this.contact_id = contact_id;
            this.activation_code = generateActivationCode();
            this.register_date = DateTime.Now;
            this.activation_date = null;
        }

        [JsonConstructor]
        public ActivateContact(int id, int contact_id, int activation_code)
        {
            var contractContactId = new Contract()
                .Requires()
                .IsFalse(contact_id <= 0, "activatecontact", "Invalid contact_id in activate contact");

            var contractCode = new Contract()
                .Requires()
                .IsFalse(activation_code <= 0 || (Math.Log10(activation_code) + 1) < 6, "activatecontact", "Invalid activation_code in activate contact");

            AddNotifications(contractContactId);
            AddNotifications(contractCode);

            this.id = id;
            this.contact_id = contact_id;
            this.activation_code = activation_code;
        }

        public bool Activate(int activation_code)
        {
            if (!IsValidActivation(activation_code))
                return false;

            this.activation_date = DateTime.Now;
            return true;
        }

        public void SetPerson(int person_id)
        {
            this.person_id = person_id;
        }

        private bool IsValidActivation(int activation_code)
        {
            if (!this.activation_code.Equals(activation_code)) //códigos diferentes
                return false;

            var d1 = DateTime.Now;
            var d2 = this.register_date.GetValueOrDefault();

            if (this.register_date == null || d1.Subtract(d2).Minutes > 5) //tempo do registro já passou de 5 minutos ou erro ao cadastrar a data de registro
                return false;

            return true;
        }

        private int generateActivationCode()
        {
            return new Random().Next(100000, 999999);
        }
    }
}
