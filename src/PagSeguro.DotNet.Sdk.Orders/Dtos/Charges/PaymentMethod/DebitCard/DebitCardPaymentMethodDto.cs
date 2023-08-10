using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard
{
    public abstract class DebitCardPaymentMethodDto : PaymentMethodDto
    {
        protected DebitCardPaymentMethodDto()
            : base(PaymentMethodType.DebitCard)
        {
        }
    }
}
