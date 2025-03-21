using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Connect.Dtos.Application
{
    public class ApplicationReadDto : ApplicationDto
    {
        [JsonProperty("client_id")]
        public string? ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string? ClientSecret { get; set; }
        [JsonProperty("account_id")]
        public string? AccountId { get; set; }
        [JsonProperty("client_type")]
        public string? ClientType { get; set; }
    }
}
