using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class ChargeDto
    {
        public string Id { get; set; }
        public string Status { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("paid_at")]
        public DateTime PaidDate { get; set; }
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
        public string Description { get; set; }
        public ChargeAmountDto Amount { get; set; }
        [JsonProperty("payment_response")]
        public PaymentResponseDto PaymentResponse { get; set; }
        [JsonProperty("payment_method")]
        public PaymentMethodDto PaymentMethod { get; set; }
        public RecurringDto Recurring { get; set; }
        [JsonProperty("sub_merchant")]
        public SubMerchantDto SubMerchant { get; set; }
        public ICollection<string> NotificationUrls { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public ICollection<LinkDto> Links { get; set; }

        public ChargeDto()
        {
            NotificationUrls = new List<string>();
            Metadata = new Dictionary<string, string>();
            Links = new List<LinkDto>();
        }
    }
}
