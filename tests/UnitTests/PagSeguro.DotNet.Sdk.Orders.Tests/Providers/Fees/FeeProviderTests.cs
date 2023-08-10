using AutoFixture;
using FluentAssertions;
using Flurl;
using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Fees;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;
using PagSeguro.DotNet.Sdk.Orders.Providers.Fees;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Fees
{
    public class FeeProviderTests : BaseProviderTests<IFeeProvider>
    {
        private FeeReadDto _feeReadDto;

        protected override IFeeProvider CreateProvider()
        {
            return new FeeProvider(Settings);
        }

        protected override void SetupMocks()
        {
            string feeJson = File.ReadAllText("Assets/fees.json");
            _feeReadDto = JsonConvert.DeserializeObject<FeeReadDto>(feeJson);
            HttpTestMock
                .ForCallsTo(
                    Url.Combine(Provider.BaseUrl, OrderEndpoint.Charges, "*"))
                .WithVerb(HttpMethod.Get)
                .RespondWith(feeJson);
        }

        [Fact]
        public void WithCreditCardBin_CreditCardBinIsSet()
        {
            int creditCardBin = 552100;

            Provider.WithCreditCardBin(creditCardBin);

            Provider.Build()
                .CreditCardBin
                .Should()
                .Be(creditCardBin);
        }

        [Fact]
        public void WithMaxInstallments_MaxInstallmentsIsSet()
        {
            int maxInstallments = 10;

            Provider.WithMaxInstallments(maxInstallments);

            Provider.Build()
                .MaxInstallments
                .Should()
                .Be(maxInstallments);
        }

        [Fact]
        public void WithMaxInstallmentsNoInterest_MaxInstallmentsNoInterestIsSet()
        {
            int maxInstallmentsNoInterest = 4;

            Provider.WithMaxInstallmentsNoInterest(maxInstallmentsNoInterest);

            Provider.Build()
                .MaxInstallmentsNoInterest
                .Should()
                .Be(maxInstallmentsNoInterest);
        }

        [Fact]
        public void WithValue_ValueIsSet()
        {
            int value = 10000;

            Provider.WithValue(value);

            Provider.Build()
                .Value
                .Should()
                .Be(value);
        }

        [Fact]
        public async Task CalculateAsync_FeeWriteDtoIsValid_HttpRequestIsCreated()
        {
            FeeWriteDto feeWriteDto = CreateFeeWriteDto();

            FeeReadDto result = await Provider.Load(feeWriteDto).CalculateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Charges,
                    OrderEndpoint.CalculateFee))
                .WithOAuthBearerToken(Settings.Token)
                .WithQueryParam("payment_methods", feeWriteDto.PaymentMethods)
                .WithQueryParam("value", feeWriteDto.Value)
                .WithQueryParam("max_installments", feeWriteDto.MaxInstallments)
                .WithQueryParam("max_installments_no_interest", feeWriteDto.MaxInstallmentsNoInterest)
                .WithQueryParam("credit_card_bin", feeWriteDto.CreditCardBin)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
            .Should()
                .BeEquivalentTo(_feeReadDto);
        }

        private FeeWriteDto CreateFeeWriteDto()
        {
            return Fixture.Create<FeeWriteDto>();
        }
    }
}
