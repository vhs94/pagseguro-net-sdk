using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class SubMerchantDto
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        [JsonProperty("tax_id")]
        public string TaxId { get; set; }
        public string Mcc { get; set; }
        public AddressDto Address { get; set; }
        public PhoneDto Phone { get; set; }
    }
}