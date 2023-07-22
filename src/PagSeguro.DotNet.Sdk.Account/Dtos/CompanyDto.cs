using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CompanyDto
    {
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("tax_id")]
        public string TaxId { get; set; }
        [JsonProperty("trade_name")]
        public string TradeName { get; set; }
        public ICollection<AddressDto> Address { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }

        public CompanyDto()
        {
            Address = new List<AddressDto>();
            Phones = new List<PhoneDto>();
        }
    }
}