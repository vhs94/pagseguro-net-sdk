using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.PublicKey.Dtos
{
    public class PublicKeyReadDto
    {
        [JsonPropertyName("public_key")]
        public string? PublicKey { get; set; }
        [JsonPropertyName("created_at")]
        public long CreateTimestamp { get; set; }
    }
}
