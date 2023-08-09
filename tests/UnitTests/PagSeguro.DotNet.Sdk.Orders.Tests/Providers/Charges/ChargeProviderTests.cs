using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class ChargeProviderTests : BaseProviderTests<ChargeWithPaymentMethodProvider>
    {
        private IServiceProvider _serviceProviderMock;

        protected override void CreateMocks()
        {
            _serviceProviderMock = Substitute.For<IServiceProvider>();
        }


        protected override ChargeWithPaymentMethodProvider CreateProvider()
        {
            return new ChargeWithPaymentMethodProvider(Settings, _serviceProviderMock);
        }

        [Fact]
        public void WithBankSlip_GetServiceIChargeByBankSlipProviderIsCalled()
        {
            Provider.WithBankSlip();

            _serviceProviderMock
                .Received(1)
                .GetService<IBankSlipChargeProvider>();
        }

        [Fact]
        public void WithCreditCard_GetServiceIChargeByCreditCardProviderIsCalled()
        {
            Provider.WithCreditCard();

            _serviceProviderMock
                .Received(1)
         
                .GetService<ICreditCardChargeProvider>();
        }

        [Fact]
        public void WithCreditCardAnd3DsAuthentication_GetServiceIChargeByCreditCardWith3DsAuthProviderIsCalled()
        {
            Provider.WithCreditCardAnd3DsAuthentication();

            _serviceProviderMock
                .Received(1)
                .GetService<ICreditCardWith3DsAuthChargeProvider>();
        }

        [Fact]
        public void WithDebitCardAnd3DsAuthentication_GetServiceIChargeByDebitCardWith3DsAuthProviderIsCalled()
        {
            Provider.WithDebitCardAnd3DsAuthentication();

            _serviceProviderMock
                .Received(1)
                .GetService<IDebitCardWith3DsAuthChargeProvider>();
        }
    }
}
