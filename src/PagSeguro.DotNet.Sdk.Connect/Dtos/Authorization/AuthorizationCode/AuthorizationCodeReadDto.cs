using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode
{
    public class AuthorizationCodeReadDto : AuthorizationReadDto
    {
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        [JsonPropertyName("account_id")]
        public string? AccountId { get; set; }
    }
}
