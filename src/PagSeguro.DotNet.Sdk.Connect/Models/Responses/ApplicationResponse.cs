using PagSeguro.DotNet.Sdk.Connect.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Responses
{
    public class ApplicationResponse : Application
    {
        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }
        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; set; }
        [JsonPropertyName("account_id")]
        public string? AccountId { get; set; }
        [JsonPropertyName("client_type")]
        public string? ClientType { get; set; }
    }
}
