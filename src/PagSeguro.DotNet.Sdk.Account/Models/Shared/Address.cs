using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Shared
{
    public class Address
    {
        [JsonPropertyName("region_code")]
        public string? RegionCode { get; set; }
        public string? City { get; set; }
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string? Locality { get; set; }
        public string? Country { get; set; }
    }
}
