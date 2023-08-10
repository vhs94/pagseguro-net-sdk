using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class FeeReadDto
    {
        [JsonProperty("payment_methods")]
        public PaymentMethodDto PaymentMethods { get; set; }
    }
}
