using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard
{
    public class ChargeByDebitCardWith3DsAuthReadDto : ChargeByCardReadDto
    {
        [JsonProperty("payment_method")]
        public DebitCardWith3DsAuthPaymentMethodReadDto? PaymentMethod { get; set; }
    }
}
