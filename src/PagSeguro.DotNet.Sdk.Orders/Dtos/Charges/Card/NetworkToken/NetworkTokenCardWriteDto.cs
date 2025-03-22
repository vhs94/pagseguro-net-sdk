using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card.NetworkToken
{
    public class NetworkTokenCardWriteDto : NetworkTokenCardDto
    {
        [JsonPropertyName("network_token")]
        public string? NetworkToken { get; set; }
        [JsonPropertyName("security_code")]
        public int SecurityCode { get; set; }
    }
}
