using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CompanyDto
    {
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }
        [JsonPropertyName("trade_name")]
        public string? TradeName { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }

        public CompanyDto() => Phones = [];
    }
}