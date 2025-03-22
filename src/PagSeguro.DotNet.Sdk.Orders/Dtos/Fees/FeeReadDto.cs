using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class FeeReadDto
    {
        [JsonPropertyName("payment_methods")]
        public PaymentMethodDto? PaymentMethods { get; set; }
    }
}
