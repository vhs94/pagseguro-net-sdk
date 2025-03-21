using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card.NetworkToken
{
    public abstract class NetworkTokenCardDto : CardDto
    {
        [JsonProperty("token_data")]
        public TokenDataDto? TokenData { get; set; }
    }
}
