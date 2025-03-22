using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds
{
    public class CreditCardWith3DsAuthPaymentMethodWriteDto : CreditCardPaymentMethodWriteDto
    {
        [JsonPropertyName("authentication_method")]
        public AuthenticationMethodWriteDto? AuthenticationMethod { get; set; }
    }
}
