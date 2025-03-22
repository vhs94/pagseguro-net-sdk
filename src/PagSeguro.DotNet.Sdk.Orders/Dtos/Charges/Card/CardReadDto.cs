using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card
{
    public class CardReadDto : CardDto
    {
        public string? Brand { get; set; }
        [JsonPropertyName("first_digits")]
        public int FirstDigits { get; set; }
        [JsonPropertyName("last_digits")]
        public int LastDigits { get; set; }
    }
}
