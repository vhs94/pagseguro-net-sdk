using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard
{
    public class ChargeByCreditCardReadDto : ChargeByCardReadDto
    {
        [JsonPropertyName("payment_method")]
        public CreditCardPaymentMethodReadDto? PaymentMethod { get; set; }
    }
}
