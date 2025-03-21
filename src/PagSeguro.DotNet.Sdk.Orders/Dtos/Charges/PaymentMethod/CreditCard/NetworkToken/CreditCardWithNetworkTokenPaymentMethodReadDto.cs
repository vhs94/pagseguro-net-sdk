using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card.NetworkToken;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.NetworkToken
{
    public class CreditCardWithNetworkTokenPaymentMethodReadDto : CreditCardWithNetworkTokenPaymentMethodDto
    {
        public NetworkTokenCardReadDto? Card { get; set; }
    }
}
