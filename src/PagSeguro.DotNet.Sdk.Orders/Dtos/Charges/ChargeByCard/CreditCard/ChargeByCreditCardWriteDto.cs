using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard
{
    public class ChargeByCreditCardWriteDto : ChargeByCardWriteDto
    {
        [JsonPropertyName("payment_method")]
        public CreditCardPaymentMethodWriteDto? PaymentMethod { get; set; }
    }
}
