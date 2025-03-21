using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds
{
    public class CreditCardWith3DsAuthPaymentMethodReadDto : CreditCardPaymentMethodReadDto
    {
        [JsonProperty("authentication_method")]
        public AuthenticationMethodReadDto? AuthenticationMethod { get; set; }
    }
}
