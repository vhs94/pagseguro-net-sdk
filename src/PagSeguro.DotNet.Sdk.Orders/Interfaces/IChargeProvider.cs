using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces
{
    public interface IChargeProvider : IProvider
    {
        IGenericChargeProvider<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto> ForBankSlip();
        IGenericChargeProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto> ForCreditCard();
        IGenericChargeProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto> ForCreditCardWith3DsAuthCard();
        IGenericChargeProvider<ChargeByDebitCardWith3DsAuthWriteDto, ChargeByDebitCardWith3DsAuthReadDto> ForDebitCardWith3DsAuthCard();
    }
}
