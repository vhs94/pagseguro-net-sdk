using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CompanyWriteDto : CompanyDto
    {
        [JsonPropertyName("name")]
        public string? CompanyName { get; set; }
        public AddressDto? Address { get; set; }
    }
}
