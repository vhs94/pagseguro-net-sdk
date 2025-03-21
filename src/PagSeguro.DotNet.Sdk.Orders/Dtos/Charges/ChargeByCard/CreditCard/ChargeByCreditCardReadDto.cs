using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard
{
    public class ChargeByCreditCardReadDto : ChargeByCardReadDto
    {
        [JsonProperty("payment_method")]
        public CreditCardPaymentMethodReadDto? PaymentMethod { get; set; }
    }
}
