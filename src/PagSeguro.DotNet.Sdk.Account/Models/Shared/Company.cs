using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Shared
{
    public class Company
    {
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }
        [JsonPropertyName("trade_name")]
        public string? TradeName { get; set; }
        public ICollection<Phone> Phones { get; set; }

        public Company() => Phones = [];
    }
}