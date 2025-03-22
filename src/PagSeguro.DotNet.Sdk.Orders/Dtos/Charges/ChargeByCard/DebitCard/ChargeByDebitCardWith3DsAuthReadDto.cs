using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard
{
    public class ChargeByDebitCardWith3DsAuthReadDto : ChargeByCardReadDto
    {
        [JsonPropertyName("payment_method")]
        public DebitCardWith3DsAuthPaymentMethodReadDto? PaymentMethod { get; set; }
    }
}
