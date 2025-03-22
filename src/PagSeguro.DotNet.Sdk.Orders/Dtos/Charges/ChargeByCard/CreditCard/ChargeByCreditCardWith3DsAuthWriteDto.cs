using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard
{
    public class ChargeByCreditCardWith3DsAuthWriteDto : ChargeByCardWriteDto
    {
        [JsonPropertyName("payment_method")]
        public CreditCardWith3DsAuthPaymentMethodWriteDto? PaymentMethod { get; set; }
    }
}
