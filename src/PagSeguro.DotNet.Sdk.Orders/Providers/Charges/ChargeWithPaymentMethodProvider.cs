using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    public class ChargeWithPaymentMethodProvider(
        PagSeguroSettings settings,
        IServiceProvider serviceProvider)
        : BaseProvider(settings),
        IChargeWithPaymentMethodProvider
    {
        public IBankSlipChargeProvider WithBankSlip()
            => serviceProvider.GetService<IBankSlipChargeProvider>()!;

        public ICreditCardChargeProvider WithCreditCard()
            => serviceProvider.GetService<ICreditCardChargeProvider>()!;

        public ICreditCardWith3DsAuthChargeProvider WithCreditCardAnd3DsAuthentication()
            => serviceProvider.GetService<ICreditCardWith3DsAuthChargeProvider>()!;

        public IDebitCardWith3DsAuthChargeProvider WithDebitCardAnd3DsAuthentication()
            => serviceProvider.GetService<IDebitCardWith3DsAuthChargeProvider>()!;
    }
}
