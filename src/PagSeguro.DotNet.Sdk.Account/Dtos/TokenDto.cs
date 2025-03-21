using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class TokenDto
    {
        [JsonProperty("token_type")]
        public string? TokenType { get; set; }
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string? RefreshToken { get; set; }
        public string? Scope { get; set; }
    }
}
