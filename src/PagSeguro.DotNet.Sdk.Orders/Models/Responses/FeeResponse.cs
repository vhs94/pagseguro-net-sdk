using PagSeguro.DotNet.Sdk.Orders.Dtos.Fees;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    public class FeeResponse
    {
        [JsonPropertyName("payment_methods")]
        public PaymentMethodDto? PaymentMethods { get; set; }
    }
}
