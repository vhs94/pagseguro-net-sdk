using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class TosAcceptanceDto
    {
        [JsonPropertyName("user_ip")]
        public string? UserIp { get; set; }
        public DateTime Date { get; set; }
    }
}
