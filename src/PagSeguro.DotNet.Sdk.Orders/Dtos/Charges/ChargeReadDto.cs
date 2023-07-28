using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class ChargeReadDto : ChargeDto
    {
        public string Id { get; set; }
        public string Status { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("paid_at")]
        public DateTime PaidDate { get; set; }
        public ChargeAmountReadDto Amount { get; set; }
        [JsonProperty("payment_response")]
        public PaymentResponseDto PaymentResponse { get; set; }
        [JsonProperty("payment_method")]
        public PaymentMethodReadDto PaymentMethod { get; set; }
    }
}
