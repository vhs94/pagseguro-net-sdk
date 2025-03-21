using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode
{
    public class AuthorizationCodeReadDto : AuthorizationReadDto
    {
        [JsonProperty("refresh_token")]
        public string? RefreshToken { get; set; }
        [JsonProperty("account_id")]
        public string? AccountId { get; set; }
    }
}
