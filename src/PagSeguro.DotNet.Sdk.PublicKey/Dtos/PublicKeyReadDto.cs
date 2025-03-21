using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.PublicKey.Dtos
{
    public class PublicKeyReadDto
    {
        [JsonProperty("public_key")]
        public string? PublicKey { get; set; }
        [JsonProperty("created_at")]
        public long CreateTimestamp { get; set; }
    }
}
