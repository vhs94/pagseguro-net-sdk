using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item
{
    public abstract class ItemDto
    {
        [JsonProperty("reference_id")]
        public string? ReferenceId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        [JsonProperty("unit_amount")]
        public int UnitAmount { get; set; }
    }
}
