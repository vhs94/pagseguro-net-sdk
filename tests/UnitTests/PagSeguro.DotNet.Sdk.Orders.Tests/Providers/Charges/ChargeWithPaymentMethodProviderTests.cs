using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class ChargeWithPaymentMethodProviderTests : BaseProviderTests<ChargeWithPaymentMethodProvider>
    {
        protected override ChargeWithPaymentMethodProvider CreateProvider()
        {
            return new ChargeWithPaymentMethodProvider(Settings, ServiceProviderMock);
        }

        [Fact]
        public void WithBankSlip_ProviderIsCalled()
        {
            Provider.WithBankSlip();

            ServiceProviderMock
                .Received(1)
                .GetService<IBankSlipChargeProvider>();
        }

        [Fact]
        public void WithCreditCard_ProviderIsCalled()
        {
            Provider.WithCreditCard();

            ServiceProviderMock
                .Received(1)
                .GetService<ICreditCardChargeProvider>();
        }

        [Fact]
        public void WithCreditCardAnd3DsAuthentication_ProviderIsCalled()
        {
            Provider.WithCreditCardAnd3DsAuthentication();

            ServiceProviderMock
                .Received(1)
                .GetService<ICreditCardWith3DsAuthChargeProvider>();
        }

        [Fact]
        public void WithDebitCardAnd3DsAuthentication_ProviderIsCalled()
        {
            Provider.WithDebitCardAnd3DsAuthentication();

            ServiceProviderMock
                .Received(1)
                .GetService<IDebitCardWith3DsAuthChargeProvider>();
        }
    }
}
