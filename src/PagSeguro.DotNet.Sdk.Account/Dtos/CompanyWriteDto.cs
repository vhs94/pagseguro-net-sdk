using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CompanyWriteDto : CompanyDto
    {
        [JsonProperty("name")]
        public string? CompanyName { get; set; }
        public AddressDto? Address { get; set; }
    }
}
