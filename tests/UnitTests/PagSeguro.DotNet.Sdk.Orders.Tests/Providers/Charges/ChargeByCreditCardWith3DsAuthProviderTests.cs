using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard.Auth3Ds;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class ChargeByCreditCardWith3DsAuthProviderTests
        : GenericChargeProviderTests<
            ChargeByCreditCardWith3DsAuthWriteDto,
            ChargeByCreditCardWith3DsAuthReadDto>
    {
        private ChargeByCreditCardWith3DsAuthProvider _chargeByCreditCardWith3DsAuthProvider;

        protected override GenericChargeProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto> CreateProvider()
        {
            _chargeByCreditCardWith3DsAuthProvider = new ChargeByCreditCardWith3DsAuthProvider(Settings);
            return _chargeByCreditCardWith3DsAuthProvider;
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            CreditCardWith3DsAuthPaymentMethodWriteDto paymentMethodWriteDto = CreateCreditCardWith3DsAuthPaymentMethodWriteDto();

            _chargeByCreditCardWith3DsAuthProvider.AddPaymentMethod(paymentMethodWriteDto);

            _chargeByCreditCardWith3DsAuthProvider
                .Build()
                .PaymentMethod
                .Should().BeEquivalentTo(paymentMethodWriteDto);
        }

        private CreditCardWith3DsAuthPaymentMethodWriteDto CreateCreditCardWith3DsAuthPaymentMethodWriteDto()
        {
            return Fixture.Create<CreditCardWith3DsAuthPaymentMethodWriteDto>();
        }

        [Fact]
        public void WithAmount_AmountIsValid_AmountIsSet()
        {
            ChargeAmountWriteDto chargeAmountWriteDto = CreateChargeAmountWriteDto();

            _chargeByCreditCardWith3DsAuthProvider.WithAmount(chargeAmountWriteDto);

            _chargeByCreditCardWith3DsAuthProvider
                .Build()
                .Amount
                .Should().BeEquivalentTo(chargeAmountWriteDto);
        }

        [Fact]
        public void WithMetadata_MetadataIsValid_MetadataIsSet()
        {
            IDictionary<string, string> metadata = CreateMetadata();

            _chargeByCreditCardWith3DsAuthProvider.WithMetadata(metadata);

            _chargeByCreditCardWith3DsAuthProvider
                .Build()
                .Metadata
                .Should().BeEquivalentTo(metadata);
        }
    }
}
