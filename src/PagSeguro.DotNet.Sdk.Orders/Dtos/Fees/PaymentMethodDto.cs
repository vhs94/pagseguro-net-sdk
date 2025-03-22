using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class PaymentMethodDto
    {
        [JsonPropertyName("credit_card")]
        public CreditCardDto? CreditCard { get; set; }
    }
}
