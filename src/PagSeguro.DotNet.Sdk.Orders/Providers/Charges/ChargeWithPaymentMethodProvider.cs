using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class ChargeWithPaymentMethodProvider : BaseProvider, IChargeWithPaymentMethodProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ChargeWithPaymentMethodProvider(
            PagSeguroSettings settings,
            IServiceProvider serviceProvider)
            : base(settings)
        {
            _serviceProvider = serviceProvider;
        }

        public IBankSlipChargeProvider WithBankSlip()
        {
            return _serviceProvider.GetService<IBankSlipChargeProvider>();
        }

        public ICreditCardChargeProvider WithCreditCard()
        {
            return _serviceProvider.GetService<ICreditCardChargeProvider>();
        }

        public ICreditCardWith3DsAuthChargeProvider WithCreditCardAnd3DsAuthentication()
        {
            return _serviceProvider.GetService<ICreditCardWith3DsAuthChargeProvider>();
        }

        public IDebitCardWith3DsAuthChargeProvider WithDebitCardAnd3DsAuthentication()
        {
            return _serviceProvider.GetService<IDebitCardWith3DsAuthChargeProvider>();
        }
    }
}
