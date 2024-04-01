using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip
{
    public abstract class BankSlipPaymentMethodDto : PaymentMethodDto
    {
        public BankSlipPaymentMethodDto()
            : base(PaymentMethodType.BankSlip)
        {
        }
    }
}
