using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card.NetworkToken
{
    public class NetworkTokenCardReadDto : NetworkTokenCardDto
    {
        public string? Brand { get; set; }
        [JsonProperty("first_digits")]
        public int FirstDigits { get; set; }
        [JsonProperty("last_digits")]
        public int LastDigits { get; set; }
    }
}
