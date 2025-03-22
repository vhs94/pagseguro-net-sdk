using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard
{
    public class DebitCardWith3DsAuthPaymentMethodWriteDto : DebitCardPaymentMethodWriteDto
    {
        [JsonPropertyName("authentication_method")]
        public AuthenticationMethodWriteDto? AuthenticationMethod { get; set; }
    }
}
