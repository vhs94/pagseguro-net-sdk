﻿using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class TokenDto
    {
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        public string? Scope { get; set; }
    }
}
