using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard
{
    public class ChargeByCreditCardWriteDto : ChargeByCardWriteDto
    {
        [JsonProperty("payment_method")]
        public CreditCardPaymentMethodWriteDto? PaymentMethod { get; set; }
    }
}
