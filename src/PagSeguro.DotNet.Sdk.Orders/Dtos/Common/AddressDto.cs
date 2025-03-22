using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Common
{
    public class AddressDto
    {
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Locality { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        [JsonPropertyName("region_code")]
        public string? RegionCode { get; set; }
        public string? Country { get; set; }
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }
    }
}