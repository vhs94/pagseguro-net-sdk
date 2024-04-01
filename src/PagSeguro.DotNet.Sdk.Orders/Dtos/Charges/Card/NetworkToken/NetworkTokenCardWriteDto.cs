using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card.NetworkToken
{
    public class NetworkTokenCardWriteDto : NetworkTokenCardDto
    {
        [JsonProperty("network_token")]
        public string NetworkToken { get; set; }
        [JsonProperty("security_code")]
        public int SecurityCode { get; set; }
    }
}
