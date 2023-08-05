using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public class ChargeByCreditCardProviderTests : GenericChargeProviderTests<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto>
    {
        private ChargeByCreditCardProvider _chargeByCreditCardProviderMock;
        
        protected override GenericChargeProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto> CreateProvider()
        {
            _chargeByCreditCardProviderMock = new ChargeByCreditCardProvider(Settings);
            return _chargeByCreditCardProviderMock;
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            CreditCardPaymentMethodWriteDto paymentMethodWriteDto = CreateCreditCardPaymentMethodWriteDto();

            _chargeByCreditCardProviderMock.AddPaymentMethod(paymentMethodWriteDto);

            _chargeByCreditCardProviderMock
                .Build()
                .PaymentMethod
                .Should().BeEquivalentTo(paymentMethodWriteDto);
        }

        private CreditCardPaymentMethodWriteDto CreateCreditCardPaymentMethodWriteDto()
        {
            return Fixture.Create<CreditCardPaymentMethodWriteDto>();
        }

        [Fact]
        public void WithAmount_AmountIsValid_AmountIsSet()
        {
            ChargeAmountWriteDto chargeAmountWriteDto = CreateChargeAmountWriteDto();

            _chargeByCreditCardProviderMock.WithAmount(chargeAmountWriteDto);

            _chargeByCreditCardProviderMock
                .Build()
                .Amount
                .Should().BeEquivalentTo(chargeAmountWriteDto);
        }

        [Fact]
        public void WithMetadata_MetadataIsValid_MetadataIsSet()
        {
            IDictionary<string, string> metadata = CreateMetadata();

            _chargeByCreditCardProviderMock.WithMetadata(metadata);

            _chargeByCreditCardProviderMock
                .Build()
                .Metadata
                .Should().BeEquivalentTo(metadata);
        }
    }
}
