using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Shared
{
    public abstract class Account
    {
        public string? Type { get; set; }
        public string? Email { get; set; }
        [JsonPropertyName("business_category")]
        public string? BusinessCategory { get; set; }
        public Person? Person { get; set; }
    }
}
