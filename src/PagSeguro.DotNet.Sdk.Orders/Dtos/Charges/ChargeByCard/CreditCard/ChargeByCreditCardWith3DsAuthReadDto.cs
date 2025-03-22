using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard
{
    public class ChargeByCreditCardWith3DsAuthReadDto : ChargeByCardReadDto
    {
        [JsonPropertyName("payment_method")]
        public CreditCardWith3DsAuthPaymentMethodReadDto? PaymentMethod { get; set; }
    }
}
