using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Models.Maps;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Models.Maps
{
    public class OrderRequestMapperTests
    {
        private readonly Fixture _fixture = new();
        private readonly OrderRequestMapper _mapper = new();

        [Fact]
        public void ToChargedOrder_OrderIsFilled_AllDataIsCopied()
        {
            OrderRequest orderRequest = _fixture.Create<OrderRequest>();

            ChargedOrderRequest<ChargeByBankSlipRequest> result = _mapper
                .ToChargedOrder<ChargeByBankSlipRequest>(orderRequest);

            result.Should().BeEquivalentTo(orderRequest);
            result.Charges.Should().BeEmpty();
        }

        [Fact]
        public void ToChargedOrder_CollectionsAreNull_CollectionsStayNull()
        {
            var orderRequest = new OrderRequest
            {
                QrCodes = null,
                Items = null
            };

            ChargedOrderRequest<ChargeByBankSlipRequest> result = _mapper
                .ToChargedOrder<ChargeByBankSlipRequest>(orderRequest);

            result.QrCodes.Should().BeNull();
            result.Items.Should().BeNull();
        }

        [Fact]
        public void ToChargedOrder_OrderIsMapped_CollectionsAreNotShared()
        {
            OrderRequest orderRequest = _fixture.Create<OrderRequest>();
            int expectedItemCount = orderRequest.Items!.Count;

            ChargedOrderRequest<ChargeByBankSlipRequest> result = _mapper
                .ToChargedOrder<ChargeByBankSlipRequest>(orderRequest);
            result.Items!.Add(_fixture.Create<ItemRequest>());
            result.NotificationUrls.Add("https://example.com");

            orderRequest.Items.Should().HaveCount(expectedItemCount);
            orderRequest.NotificationUrls.Should().NotContain("https://example.com");
        }
    }
}
