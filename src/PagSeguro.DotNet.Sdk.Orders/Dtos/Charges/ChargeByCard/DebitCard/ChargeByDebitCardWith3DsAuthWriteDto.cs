using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard
{
    public class ChargeByDebitCardWith3DsAuthWriteDto : ChargeByCardWriteDto
    {
        [JsonPropertyName("payment_method")]
        public DebitCardWith3DsAuthPaymentMethodWriteDto? PaymentMethod { get; set; }
    }
}
