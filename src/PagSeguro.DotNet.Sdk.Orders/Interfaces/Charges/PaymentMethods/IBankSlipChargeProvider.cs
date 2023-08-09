using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    public interface IBankSlipChargeProvider
        : IChargeProviderOf<
            IBankSlipChargeProvider,
            ChargeByBankSlipWriteDto,
            ChargeByBankSlipReadDto>
    {
        public IBankSlipChargeProvider AddBankSlip(BankSlipWriteDto bankSlipWriteDto)
        {
            ChargeWriteDto.PaymentMethod = new BankSlipPaymentMethodWriteDto
            {
                BankSlip = bankSlipWriteDto
            };
            return this;
        }
    }
}
