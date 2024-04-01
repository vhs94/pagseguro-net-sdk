using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public abstract class OrderDto
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
        public CustomerDto Customer { get; set; }
        public ShippingDto Shipping { get; set; }
        [JsonProperty("notification_urls")]
        public ICollection<string> NotificationUrls { get; set; }

        public OrderDto()
        {
            NotificationUrls = new List<string>();
        }
    }
}
