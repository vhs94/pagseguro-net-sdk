﻿using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class ChargeProviderTests : BaseProviderTests<ChargeProvider>
    {
        private IServiceProvider _serviceProviderMock;

        protected override void CreateMocks()
        {
            _serviceProviderMock = Substitute.For<IServiceProvider>();
        }


        protected override ChargeProvider CreateProvider()
        {
            return new ChargeProvider(Settings, _serviceProviderMock);
        }

        [Fact]
        public void WithBankSlip_GetServiceIChargeByBankSlipProviderIsCalled()
        {
            Provider.WithBankSlip();

            _serviceProviderMock
                .Received(1)
                .GetService<IChargeByBankSlipProvider>();
        }

        [Fact]
        public void WithCreditCard_GetServiceIChargeByCreditCardProviderIsCalled()
        {
            Provider.WithCreditCard();

            _serviceProviderMock
                .Received(1)
         
                .GetService<IChargeByCreditCardProvider>();
        }

        [Fact]
        public void WithCreditCardAnd3DsAuthentication_GetServiceIChargeByCreditCardWith3DsAuthProviderIsCalled()
        {
            Provider.WithCreditCardAnd3DsAuthentication();

            _serviceProviderMock
                .Received(1)
                .GetService<IChargeByCreditCardWith3DsAuthProvider>();
        }

        [Fact]
        public void WithDebitCardAnd3DsAuthentication_GetServiceIChargeByDebitCardWith3DsAuthProviderIsCalled()
        {
            Provider.WithDebitCardAnd3DsAuthentication();

            _serviceProviderMock
                .Received(1)
                .GetService<IChargeByDebitCardWith3DsAuthProvider>();
        }
    }
}