using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Requests
{
    public class TosAcceptanceRequest
    {
        [JsonPropertyName("user_ip")]
        public string? UserIp { get; set; }
        public DateTime Date { get; set; }
    }
}
