using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds
{
    public class CreditCardWith3DsAuthPaymentMethodWriteDto : CreditCardPaymentMethodWriteDto
    {
        [JsonProperty("authentication_method")]
        public AuthenticationMethodWriteDto AuthenticationMethod { get; set; }
    }
}
