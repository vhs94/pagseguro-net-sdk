using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Charges
{
    /// <inheritdoc cref="IChargeWithPaymentMethodProvider" />
    public class ChargeWithPaymentMethodProvider(
        PagSeguroSettings settings,
        IServiceProvider serviceProvider,
        IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        IChargeWithPaymentMethodProvider
    {
        /// <inheritdoc />
        public IBankSlipChargeProvider WithBankSlip()
            => serviceProvider.GetService<IBankSlipChargeProvider>()!;

        /// <inheritdoc />
        public ICreditCardChargeProvider WithCreditCard()
            => serviceProvider.GetService<ICreditCardChargeProvider>()!;

        /// <inheritdoc />
        public ICreditCardWith3DsAuthChargeProvider WithCreditCardAnd3DsAuthentication()
            => serviceProvider.GetService<ICreditCardWith3DsAuthChargeProvider>()!;

        /// <inheritdoc />
        public IDebitCardWith3DsAuthChargeProvider WithDebitCardAnd3DsAuthentication()
            => serviceProvider.GetService<IDebitCardWith3DsAuthChargeProvider>()!;
    }
}
