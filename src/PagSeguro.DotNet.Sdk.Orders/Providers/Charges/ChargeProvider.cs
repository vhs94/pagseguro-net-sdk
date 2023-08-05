using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
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

        public IChargeByBankSlipProvider WithBankSlip()
        {
            return _serviceProvider.GetService<IChargeByBankSlipProvider>();
        }

        public IChargeByCreditCardProvider WithCreditCard()
        {
            return _serviceProvider.GetService<IChargeByCreditCardProvider>();
        }

        public IChargeByCreditCardWith3DsAuthProvider WithCreditCardAnd3DsAuthentication()
        {
            return _serviceProvider.GetService<IChargeByCreditCardWith3DsAuthProvider>();
        }

        public IChargeByDebitCardWith3DsAuthProvider WithDebitCardAnd3DsAuthentication()
        {
            return _serviceProvider.GetService<IChargeByDebitCardWith3DsAuthProvider>();
        }
    }
}
