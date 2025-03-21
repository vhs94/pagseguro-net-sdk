using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard
{
    public abstract class CreditCardPaymentMethodDto : CardPaymentMethodDto
    {
        [JsonProperty("soft_descriptor")]
        public string? SoftDescriptor { get; set; }

        public CreditCardPaymentMethodDto()
            : base(PaymentMethodType.CreditCard)
        {
        }
    }
}
