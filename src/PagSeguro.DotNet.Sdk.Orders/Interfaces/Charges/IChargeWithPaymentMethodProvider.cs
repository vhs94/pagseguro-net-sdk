using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IChargeWithPaymentMethodProvider : IProvider
    {
        IBankSlipChargeProvider WithBankSlip();
        ICreditCardChargeProvider WithCreditCard();
        ICreditCardWith3DsAuthChargeProvider WithCreditCardAnd3DsAuthentication();
        IDebitCardWith3DsAuthChargeProvider WithDebitCardAnd3DsAuthentication();
    }
}
