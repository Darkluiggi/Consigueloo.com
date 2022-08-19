using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Anuncios
{
    public class PaymentData
    {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public int amount_in_cents { get; set; }
        public string reference { get; set; }
        public string currency { get; set; }
        public string payment_method_type { get; set; }
        [ForeignKey("payment_method")]
        public int? payment_methodId { get; set; }
        public PaymentMethod payment_method { get; set; }
        public string redirect_url { get; set; }
        public string status { get; set; }
        public object status_message { get; set; }
        [ForeignKey("merchant")]
        public string merchantId { get; set; }
        public Merchant merchant { get; set; }
        public List<object> taxes { get; set; }
    }

    public class Extra
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string last_four { get; set; }
        public string external_identifier { get; set; }
        public string processor_response_code { get; set; }
    }

    public class Merchant
    {
        public string name { get; set; }
        public string legal_name { get; set; }
        public string contact_name { get; set; }
        public string phone_number { get; set; }
        public object logo_url { get; set; }
        public string legal_id_type { get; set; }
        public string email { get; set; }
        [Key]
        public string legal_id { get; set; }
    }

    public class Meta
    {
        public string trace_id { get; set; }
    }

    public class PaymentMethod
    {
        public int Id { get; set; }
        public string type { get; set; }
        [ForeignKey("extra")]
        public int? extraId { get; set; }
        public Extra extra { get; set; }
        public int installments { get; set; }
    }

    public class Root
    {
        public PaymentData data { get; set; }
        public Meta meta { get; set; }
    }
}