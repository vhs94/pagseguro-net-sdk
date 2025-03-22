using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard
{
    public class DebitCardWith3DsAuthPaymentMethodReadDto : DebitCardPaymentMethodReadDto
    {
        [JsonPropertyName("authentication_method")]
        public AuthenticationMethodReadDto? AuthenticationMethod { get; set; }
    }
}
