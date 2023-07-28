using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class CardWriteDto : CardDto
    {
        public string Id { get; set; }
        public string Encrypted { get; set; }
        public string Number { get; set; }
        [JsonProperty("network_token")]
        public string NetworkToken { get; set; }
        [JsonProperty("security_code")]
        public int SecurityCode { get; set; }
        public bool Store { get; set; }
        [JsonProperty("token_data")]
        public TokenDataDto TokenData { get; set; }
    }
}
