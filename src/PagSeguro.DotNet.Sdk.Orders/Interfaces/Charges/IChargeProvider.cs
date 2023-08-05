using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IChargeProvider : IProvider
    {
        IChargeByBankSlipProvider WithBankSlip();
        IChargeByCreditCardProvider WithCreditCard();
        IChargeByCreditCardWith3DsAuthProvider WithCreditCardAnd3DsAuthentication();
        IChargeByDebitCardWith3DsAuthProvider WithDebitCardAnd3DsAuthentication();
    }
}
