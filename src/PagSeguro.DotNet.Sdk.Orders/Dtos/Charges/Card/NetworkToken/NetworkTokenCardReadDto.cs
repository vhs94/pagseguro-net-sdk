using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card.NetworkToken
{
    public class NetworkTokenCardReadDto : NetworkTokenCardDto
    {
        public string? Brand { get; set; }
        [JsonPropertyName("first_digits")]
        public int FirstDigits { get; set; }
        [JsonPropertyName("last_digits")]
        public int LastDigits { get; set; }
    }
}
