using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card
{
    public class CardWriteDto : CardDto
    {
        public string? Number { get; set; }
        [JsonPropertyName("security_code")]
        public int SecurityCode { get; set; }
    }
}
