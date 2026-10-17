using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Maps
{
    /// <inheritdoc cref="IOrderRequestMapper" />
    public class OrderRequestMapper : IOrderRequestMapper
    {
        /// <inheritdoc />
        public ChargedOrderRequest<TChargeRequest> ToChargedOrder<TChargeRequest>(OrderRequest orderRequest)
            where TChargeRequest : ChargeBase
        {
            return new ChargedOrderRequest<TChargeRequest>
            {
                ReferenceId = orderRequest.ReferenceId,
                Customer = orderRequest.Customer,
                Shipping = orderRequest.Shipping,
                NotificationUrls = orderRequest.NotificationUrls?.ToList() ?? [],
                // Coleções nulas continuam nulas: a API recusa qr_codes: [] / items: []
                // com "must have at least 1 element".
                QrCodes = orderRequest.QrCodes?.ToList(),
                Items = orderRequest.Items?.ToList()
            };
        }
    }
}
