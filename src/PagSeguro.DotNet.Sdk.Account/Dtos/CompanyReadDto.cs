using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CompanyReadDto : CompanyDto
    {
        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }
        public ICollection<AddressDto> Address { get; set; }

        public CompanyReadDto() => Address = [];
    }
}
