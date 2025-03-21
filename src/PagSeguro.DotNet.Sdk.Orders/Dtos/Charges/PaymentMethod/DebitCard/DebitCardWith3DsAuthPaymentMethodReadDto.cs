using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard
{
    public class DebitCardWith3DsAuthPaymentMethodReadDto : DebitCardPaymentMethodReadDto
    {
        [JsonProperty("authentication_method")]
        public AuthenticationMethodReadDto? AuthenticationMethod { get; set; }
    }
}
