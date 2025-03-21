using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card
{
    public class CardReadDto : CardDto
    {
        public string? Brand { get; set; }
        [JsonProperty("first_digits")]
        public int FirstDigits { get; set; }
        [JsonProperty("last_digits")]
        public int LastDigits { get; set; }
    }
}
