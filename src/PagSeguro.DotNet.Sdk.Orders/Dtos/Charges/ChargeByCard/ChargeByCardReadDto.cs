using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard
{
    public abstract class ChargeByCardReadDto : ChargeByCardDto
    {
        public new string? Id { get; set; }
        public string? Status { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }
        [JsonPropertyName("paid_at")]
        public DateTime? PaidDate { get; set; }
        public ChargeAmountReadDto? Amount { get; set; }
        [JsonPropertyName("payment_response")]
        public CardPaymentResponseDto? PaymentResponse { get; set; }
        public ICollection<LinkDto> Links { get; set; }

        public ChargeByCardReadDto() => Links = [];
    }
}
