using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.NetworkToken
{
    public abstract class CreditCardWithNetworkTokenPaymentMethodDto : CardPaymentMethodDto
    {
        protected CreditCardWithNetworkTokenPaymentMethodDto()
            : base(PaymentMethodType.CreditCard)
        {
        }
    }
}
