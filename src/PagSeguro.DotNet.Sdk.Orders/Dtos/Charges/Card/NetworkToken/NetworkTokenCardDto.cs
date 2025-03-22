using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card.NetworkToken
{
    public abstract class NetworkTokenCardDto : CardDto
    {
        [JsonPropertyName("token_data")]
        public TokenDataDto? TokenData { get; set; }
    }
}
