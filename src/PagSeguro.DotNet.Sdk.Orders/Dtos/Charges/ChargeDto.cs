using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public abstract class ChargeDto
    {
        internal string? Id { get; set; }
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }
        public string? Description { get; set; }
        [JsonPropertyName("notification_urls")]
        public ICollection<string> NotificationUrls { get; set; }

        public ChargeDto() => NotificationUrls = [];
    }
}
