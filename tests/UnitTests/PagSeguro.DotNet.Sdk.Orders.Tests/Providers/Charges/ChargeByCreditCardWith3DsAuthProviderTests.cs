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
        private ChargeByCreditCardWith3DsAuthProvider _chargeByCreditCardWith3DsAuthProviderMock;

        protected override GenericChargeProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto> CreateProvider()
        {
            _chargeByCreditCardWith3DsAuthProviderMock = new ChargeByCreditCardWith3DsAuthProvider(Settings);
            return _chargeByCreditCardWith3DsAuthProviderMock;
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            CreditCardWith3DsAuthPaymentMethodWriteDto paymentMethodWriteDto = CreateCreditCardWith3DsAuthPaymentMethodWriteDto();

            _chargeByCreditCardWith3DsAuthProviderMock.AddPaymentMethod(paymentMethodWriteDto);

            _chargeByCreditCardWith3DsAuthProviderMock
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

            _chargeByCreditCardWith3DsAuthProviderMock.WithAmount(chargeAmountWriteDto);

            _chargeByCreditCardWith3DsAuthProviderMock
                .Build()
                .Amount
                .Should().BeEquivalentTo(chargeAmountWriteDto);
        }

        [Fact]
        public void WithMetadata_MetadataIsValid_MetadataIsSet()
        {
            IDictionary<string, string> metadata = CreateMetadata();

            _chargeByCreditCardWith3DsAuthProviderMock.WithMetadata(metadata);

            _chargeByCreditCardWith3DsAuthProviderMock
                .Build()
                .Metadata
                .Should().BeEquivalentTo(metadata);
        }
    }
}
