using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class PaymentDataDTO : ModelBase
    {
        public PaymentDataDTO(PaymentData data)
        {
            id = data.id;
            created_at = data.created_at;
            amount_in_cents = data.amount_in_cents;
            reference = data.reference;
            currency = data.currency;
            payment_method_type = data.payment_method_type;
            payment_methodId = data.payment_methodId;
            redirect_url = data.redirect_url;
            status = data.status;
            status_message = data.status_message;
            merchantId = data.merchantId;
            payment_method = new PaymentMethodDTO(data.payment_method);

        }

        public PaymentDataDTO()
        {

        }
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public int amount_in_cents { get; set; }
        public string reference { get; set; }
        public string currency { get; set; }
        public string payment_method_type { get; set; }
        public int? payment_methodId { get; set; }
        public PaymentMethodDTO payment_method { get; set; }
        public string redirect_url { get; set; }
        public string status { get; set; }
        public object status_message { get; set; }
        public string merchantId { get; set; }
        public MerchantDTO merchant { get; set; }
    }

    public class ExtraDTO
    {
        public ExtraDTO(Extra extra)
        {
            Id = extra.Id;
            name = extra.name;
            brand = extra.brand;
            last_four = extra.last_four;
            external_identifier = extra.external_identifier;
            processor_response_code = extra.processor_response_code;
        }
        public int Id { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string last_four { get; set; }
        public string external_identifier { get; set; }
        public string processor_response_code { get; set; }
    }

    public class MerchantDTO
    {
        public string name { get; set; }
        public string legal_name { get; set; }
        public string contact_name { get; set; }
        public string phone_number { get; set; }
        public object logo_url { get; set; }
        public string legal_id_type { get; set; }
        public string email { get; set; }
        public string legal_id { get; set; }
    }

    public class MetaDTO
    {
        public string trace_id { get; set; }
    }

    public class PaymentMethodDTO
    {
        public PaymentMethodDTO(PaymentMethod data)
        {
            Id = data.Id;
            type = data.type;
            extraId = data.extraId;
            extra = new ExtraDTO(data.extra);
        }
        public int Id { get; set; }
        public string type { get; set; }
        public int? extraId { get; set; }
        public ExtraDTO extra { get; set; }
        public int installments { get; set; }
    }

    public class RootDTO
    {
        public PaymentDataDTO data { get; set; }
        public MetaDTO meta { get; set; }
    }
}