using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class ChargeByDebitCardWith3DsAuthProviderTests
        : GenericChargeProviderTests<
            ChargeByDebitCardWith3DsAuthWriteDto,
            ChargeByDebitCardWith3DsAuthReadDto>
    {
        private ChargeByDebitCardWith3DsAuthProvider _chargeByDebitCardWith3DsAuthProvider;

        protected override GenericChargeProvider<ChargeByDebitCardWith3DsAuthWriteDto, ChargeByDebitCardWith3DsAuthReadDto> CreateProvider()
        {
            _chargeByDebitCardWith3DsAuthProvider = new ChargeByDebitCardWith3DsAuthProvider(Settings);
            return _chargeByDebitCardWith3DsAuthProvider;
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            DebitCardWith3DsAuthPaymentMethodWriteDto paymentMethodWriteDto = CreateDebitCardWith3DsAuthPaymentMethodWriteDto();

            _chargeByDebitCardWith3DsAuthProvider.AddPaymentMethod(paymentMethodWriteDto);

            _chargeByDebitCardWith3DsAuthProvider
                .Build()
                .PaymentMethod
                .Should().BeEquivalentTo(paymentMethodWriteDto);
        }

        private DebitCardWith3DsAuthPaymentMethodWriteDto CreateDebitCardWith3DsAuthPaymentMethodWriteDto()
        {
            return Fixture.Create<DebitCardWith3DsAuthPaymentMethodWriteDto>();
        }

        [Fact]
        public void WithAmount_AmountIsValid_AmountIsSet()
        {
            ChargeAmountWriteDto chargeAmountWriteDto = CreateChargeAmountWriteDto();

            _chargeByDebitCardWith3DsAuthProvider.WithAmount(chargeAmountWriteDto);

            _chargeByDebitCardWith3DsAuthProvider
                .Build()
                .Amount
                .Should().BeEquivalentTo(chargeAmountWriteDto);
        }

        [Fact]
        public void WithMetadata_MetadataIsValid_MetadataIsSet()
        {
            IDictionary<string, string> metadata = CreateMetadata();

            _chargeByDebitCardWith3DsAuthProvider.WithMetadata(metadata);

            _chargeByDebitCardWith3DsAuthProvider
                .Build()
                .Metadata
                .Should().BeEquivalentTo(metadata);
        }
    }
}
