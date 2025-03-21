using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class PaymentMethodDto
    {
        [JsonProperty("credit_card")]
        public CreditCardDto? CreditCard { get; set; }
    }
}
