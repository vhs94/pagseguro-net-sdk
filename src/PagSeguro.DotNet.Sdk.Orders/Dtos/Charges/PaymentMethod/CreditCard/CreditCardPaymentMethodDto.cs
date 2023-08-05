using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard
{
    public abstract class CreditCardPaymentMethodDto : CardPaymentMethodDto
    {
        [JsonProperty("soft_descriptor")]
        public string SoftDescriptor { get; set; }

        public CreditCardPaymentMethodDto()
            : base(PaymentMethodType.CreditCard)
        {
        }
    }
}
