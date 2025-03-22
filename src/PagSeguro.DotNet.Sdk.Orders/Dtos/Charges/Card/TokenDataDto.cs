using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card
{
    public class TokenDataDto
    {
        [JsonPropertyName("requestor_id")]
        public string? RequestorId { get; set; }
        public string? Wallet { get; set; }
        public string? Cryptogram { get; set; }
        [JsonPropertyName("ecommerce_domain")]
        public string? EcommerceDomain { get; set; }
        [JsonPropertyName("assurance_level")]
        public int AssuranceLevel { get; set; }
    }
}