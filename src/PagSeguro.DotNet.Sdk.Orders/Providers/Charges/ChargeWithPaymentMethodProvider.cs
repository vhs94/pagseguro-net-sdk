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
            => serviceProvider.GetRequiredService<IBankSlipChargeProvider>();

        public ICreditCardChargeProvider WithCreditCard()
            => serviceProvider.GetRequiredService<ICreditCardChargeProvider>();

        public ICreditCardWith3DsAuthChargeProvider WithCreditCardAnd3DsAuthentication()
            => serviceProvider.GetRequiredService<ICreditCardWith3DsAuthChargeProvider>();

        public IDebitCardWith3DsAuthChargeProvider WithDebitCardAnd3DsAuthentication()
            => serviceProvider.GetRequiredService<IDebitCardWith3DsAuthChargeProvider>();
    }
}
