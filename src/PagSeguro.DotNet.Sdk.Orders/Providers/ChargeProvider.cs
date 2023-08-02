using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces;

namespace PagSeguro.DotNet.Sdk.Orders.Providers
{
    public class ChargeProvider : BaseProvider, IChargeProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ChargeProvider(
            PagSeguroSettings settings,
            IServiceProvider serviceProvider)
            : base(settings)
        {
            _serviceProvider = serviceProvider;
        }

        public IGenericChargeProvider<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto> ForBankSlip()
        {
            return _serviceProvider.GetService<IGenericChargeProvider<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto>>();
        }

        public IGenericChargeProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto> ForCreditCard()
        {
            return _serviceProvider.GetService<IGenericChargeProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto>>();
        }

        public IGenericChargeProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto> ForCreditCardWith3DsAuthCard()
        {
            return _serviceProvider.GetService<IGenericChargeProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto>>();
        }

        public IGenericChargeProvider<ChargeByDebitCardWith3DsAuthWriteDto, ChargeByDebitCardWith3DsAuthReadDto> ForDebitCardWith3DsAuthCard()
        {
            return _serviceProvider.GetService<IGenericChargeProvider<ChargeByDebitCardWith3DsAuthWriteDto, ChargeByDebitCardWith3DsAuthReadDto>>();
        }
    }
}
