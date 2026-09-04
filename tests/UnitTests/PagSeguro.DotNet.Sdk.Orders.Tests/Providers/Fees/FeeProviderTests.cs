using AutoFixture;
using FluentAssertions;
using Flurl;
using PagSeguro.DotNet.Sdk.Common.Serialization;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;
using PagSeguro.DotNet.Sdk.Orders.Providers.Fees;
using System.Text.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Fees
{
    public class FeeProviderTests : BaseProviderTests<IFeeProvider>
    {
        private FeeResponse _feeResponse = null!;

        protected override IFeeProvider CreateProvider()
        {
            return new FeeProvider(Settings, FlurlClientMock);
        }

        protected override void SetupMocks()
        {
            string feeJson = File.ReadAllText("Assets/fees.json");
            _feeResponse = JsonSerializer.Deserialize<FeeResponse>(feeJson, options: JsonOptions.Default)!;
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
        public async Task CalculateAsync_FeeRequestIsValid_HttpRequestIsCreated()
        {
            FeeRequest feeRequest = CreateFeeRequest();

            FeeResponse result = await Provider.Load(feeRequest).CalculateAsync();

            HttpTestMock
                .ShouldHaveCalled(Url.Combine(
                    Provider.BaseUrl,
                    OrderEndpoint.Charges,
                    OrderEndpoint.CalculateFee))
                .WithOAuthBearerToken(Settings.Token)
                .WithQueryParam("payment_methods", feeRequest.PaymentMethods)
                .WithQueryParam("value", feeRequest.Value)
                .WithQueryParam("max_installments", feeRequest.MaxInstallments)
                .WithQueryParam("max_installments_no_interest", feeRequest.MaxInstallmentsNoInterest)
                .WithQueryParam("credit_card_bin", feeRequest.CreditCardBin)
                .WithVerb(HttpMethod.Get)
                .Times(1);
            result
            .Should()
                .BeEquivalentTo(_feeResponse);
        }

        private FeeRequest CreateFeeRequest()
        {
            return Fixture.Create<FeeRequest>();
        }
    }
}
