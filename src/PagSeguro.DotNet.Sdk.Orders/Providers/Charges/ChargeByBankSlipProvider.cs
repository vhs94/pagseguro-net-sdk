using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class ChargeByBankSlipProvider
        : GenericChargeProvider<
            ChargeByBankSlipWriteDto,
            ChargeByBankSlipReadDto>,
        IChargeByBankSlipProvider
    {
        public ChargeByBankSlipProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        public IChargeByBankSlipProvider AddBankSlip(BankSlipWriteDto bankSlipWriteDto)
        {
            ChargeWriteDto.PaymentMethod = new BankSlipPaymentMethodWriteDto
            {
                BankSlip = bankSlipWriteDto
            };
            return this;
        }

        public IChargeByBankSlipProvider WithAmount(ChargeAmountWriteDto chargeAmountWriteDto)
        {
            ChargeWriteDto.Amount = chargeAmountWriteDto;
            return this;
        }
    }
}
