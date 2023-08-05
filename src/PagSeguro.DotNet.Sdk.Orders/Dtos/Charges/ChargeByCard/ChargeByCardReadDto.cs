using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard
{
    public abstract class ChargeByCardReadDto : ChargeByCardDto
    {
        public string Id { get; set; }
        public string Status { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("paid_at")]
        public DateTime? PaidDate { get; set; }
        public ChargeAmountReadDto Amount { get; set; }
        [JsonProperty("payment_response")]
        public CardPaymentResponseDto PaymentResponse { get; set; }
        public ICollection<LinkDto> Links { get; set; }

        public ChargeByCardReadDto()
        {
            Links = new List<LinkDto>();
        }
    }
}
