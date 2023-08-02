using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card.NetworkToken;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.NetworkToken
{
    public class CreditCardWithNetworkTokenPaymentMethodWriteDto : CreditCardWithNetworkTokenPaymentMethodDto
    {
        public NetworkTokenCardWriteDto Card { get; set; }
    }
}
