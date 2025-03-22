using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public abstract class OrderDto
    {
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }
        public CustomerDto? Customer { get; set; }
        public ShippingDto? Shipping { get; set; }
        [JsonPropertyName("notification_urls")]
        public ICollection<string> NotificationUrls { get; set; }

        public OrderDto() => NotificationUrls = [];
    }
}
