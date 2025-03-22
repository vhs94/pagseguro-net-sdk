using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item
{
    public abstract class ItemDto
    {
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        [JsonPropertyName("unit_amount")]
        public int UnitAmount { get; set; }
    }
}
