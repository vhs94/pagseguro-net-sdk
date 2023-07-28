using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Common
{
    public class HolderDto
    {
        public string Name { get; set; }
        [JsonProperty("tax_id")]
        public string TaxId { get; set; }
    }
}