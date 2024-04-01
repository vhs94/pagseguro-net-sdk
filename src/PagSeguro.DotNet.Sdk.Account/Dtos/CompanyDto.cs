using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CompanyDto
    {
        [JsonProperty("tax_id")]
        public string TaxId { get; set; }
        [JsonProperty("trade_name")]
        public string TradeName { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }

        public CompanyDto()
        {
            Phones = new List<PhoneDto>();
        }
    }
}