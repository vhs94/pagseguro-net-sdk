using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Responses
{
    public abstract class AuthorizationResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        public string? Scope { get; set; }
    }
}
