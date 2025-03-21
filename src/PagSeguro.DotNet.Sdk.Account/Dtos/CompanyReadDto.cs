using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CompanyReadDto : CompanyDto
    {
        [JsonProperty("company_name")]
        public string? CompanyName { get; set; }
        public ICollection<AddressDto> Address { get; set; }

        public CompanyReadDto() => Address = [];
    }
}
