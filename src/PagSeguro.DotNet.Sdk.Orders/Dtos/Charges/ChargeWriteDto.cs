using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class ChargeWriteDto : ChargeDto
    {
        public ChargeAmountWriteDto Amount { get; set; }
        [JsonProperty("payment_method")]
        public PaymentMethodWriteDto PaymentMethod { get; set; }
    }
}
