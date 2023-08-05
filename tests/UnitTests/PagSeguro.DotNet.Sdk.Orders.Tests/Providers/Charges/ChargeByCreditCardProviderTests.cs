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
        private ChargeByCreditCardProvider _chargeByCreditCardProvider;
        
        protected override GenericChargeProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto> CreateProvider()
        {
            _chargeByCreditCardProvider = new ChargeByCreditCardProvider(Settings);
            return _chargeByCreditCardProvider;
        }

        [Fact]
        public void AddPaymentMethod_PaymentMethodIsValid_PaymentMethodIsSet()
        {
            CreditCardPaymentMethodWriteDto paymentMethodWriteDto = CreateCreditCardPaymentMethodWriteDto();

            _chargeByCreditCardProvider.AddPaymentMethod(paymentMethodWriteDto);

            _chargeByCreditCardProvider
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

            _chargeByCreditCardProvider.WithAmount(chargeAmountWriteDto);

            _chargeByCreditCardProvider
                .Build()
                .Amount
                .Should().BeEquivalentTo(chargeAmountWriteDto);
        }

        [Fact]
        public void WithMetadata_MetadataIsValid_MetadataIsSet()
        {
            IDictionary<string, string> metadata = CreateMetadata();

            _chargeByCreditCardProvider.WithMetadata(metadata);

            _chargeByCreditCardProvider
                .Build()
                .Metadata
                .Should().BeEquivalentTo(metadata);
        }
    }
}
