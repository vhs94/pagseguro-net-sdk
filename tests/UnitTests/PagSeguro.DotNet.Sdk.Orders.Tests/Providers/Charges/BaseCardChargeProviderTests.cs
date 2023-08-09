using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public abstract class BaseCardChargeProviderTests<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        : BaseChargeProviderTests<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeByCardWriteDto, IChargeWriteDto, new()
        where TChargeReadDto : ChargeByCardReadDto
        where TTopLevelProvider : ICardChargeProviderOf<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
    {
        [Fact]
        public void WithMetadata_MetadataIsValid_MetadataIsSet()
        {
            IDictionary<string, string> metadata = CreateMetadata();

            TopLevelProviderMock.WithMetadata(metadata);

            TopLevelProviderMock.Build().Metadata.Should().BeEquivalentTo(metadata);
        }

        private IDictionary<string, string> CreateMetadata()
        {
            return Fixture.Create<IDictionary<string, string>>();
        }
    }
}
