using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Responses
{
    public class AuthorizationCodeResponse : AuthorizationResponse
    {
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        [JsonPropertyName("account_id")]
        public string? AccountId { get; set; }
    }
}
