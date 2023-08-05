using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    public interface IChargeByBankSlipProvider
        : IGenericChargeProvider<
            ChargeByBankSlipWriteDto,
            ChargeByBankSlipReadDto>
    {
        IChargeByBankSlipProvider AddBankSlip(BankSlipWriteDto bankSlipWriteDto);
        IChargeByBankSlipProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto);
    }
}
