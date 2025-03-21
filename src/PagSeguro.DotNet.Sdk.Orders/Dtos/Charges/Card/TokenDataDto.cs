using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card
{
    public class TokenDataDto
    {
        [JsonProperty("requestor_id")]
        public string? RequestorId { get; set; }
        public string? Wallet { get; set; }
        public string? Cryptogram { get; set; }
        [JsonProperty("ecommerce_domain")]
        public string? EcommerceDomain { get; set; }
        [JsonProperty("assurance_level")]
        public int AssuranceLevel { get; set; }
    }
}