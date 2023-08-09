using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Charges
{
    public abstract class BaseCreditCardChargeProviderTests<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        : BaseCardChargeProviderTests<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeByCardWriteDto, IChargeWriteDto, new()
        where TChargeReadDto : ChargeByCardReadDto
        where TTopLevelProvider : ICreditCardChargeProviderOf<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
    {
        [Fact]
        public async Task CaptureAsync_ChargeIsValid_HttpRequestIsCreated()
        {
            string chargeId = Guid.NewGuid().ToString();

            TChargeReadDto result = await Provider
                .WithId(chargeId)
                .CaptureAsync(100);

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Charges,
                    chargeId,
                    OrderEndpoint.Capture))
                .WithOAuthBearerToken(Settings.Token)
                .WithRequestJson(new
                {
                    amount = new
                    {
                        value = 100
                    }
                })
                .WithVerb(HttpMethod.Post)
                .Times(1);
            result.Should().BeEquivalentTo(ChargeReadDto);
        }
    }
}
