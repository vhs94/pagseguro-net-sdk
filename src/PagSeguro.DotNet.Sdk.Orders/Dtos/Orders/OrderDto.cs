using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public abstract class OrderDto
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
        public CustomerDto Customer { get; set; }
        public ICollection<ItemDto> Items { get; set; }
        public ShippingDto Shipping { get; set; }
        [JsonProperty("notification_urls")]
        public ICollection<string> NotificationUrls { get; set; }

        public OrderDto()
        {
            Items = new List<ItemDto>();
            NotificationUrls = new List<string>();
        }
    }
}
