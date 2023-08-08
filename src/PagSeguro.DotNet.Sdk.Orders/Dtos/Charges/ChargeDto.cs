using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public abstract class ChargeDto
    {
        internal string Id { get; set; }
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
        public string Description { get; set; }
        [JsonProperty("notification_urls")]
        public ICollection<string> NotificationUrls { get; set; }

        public ChargeDto()
        {
            NotificationUrls = new List<string>();
        }
    }
}
