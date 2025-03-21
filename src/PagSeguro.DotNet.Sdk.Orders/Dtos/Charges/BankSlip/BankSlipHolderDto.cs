using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip
{
    public class BankSlipHolderDto : HolderDto
    {
        public string? Email { get; set; }
        [JsonProperty("tax_id")]
        public string? TaxId { get; set; }
        public AddressDto? Address { get; set; }
    }
}
