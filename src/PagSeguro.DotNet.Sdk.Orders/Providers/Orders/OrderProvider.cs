using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    /// <inheritdoc cref="IOrderProvider" />
    public class OrderProvider : BaseProvider, IOrderProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private OrderRequest _orderRequest = null!;

        public OrderProvider(
            PagSeguroSettings settings,
            IServiceProvider serviceProvider,
            IFlurlClient flurlClient)
            : base(settings, flurlClient)
        {
            _serviceProvider = serviceProvider;
            InitOrder();
        }

        private void InitOrder()
        {
            _orderRequest = new OrderRequest();
        }

        /// <inheritdoc />
        public IOrderProvider WithCustomer(Customer customer)
        {
            _orderRequest.Customer = customer;
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider WithItem(ItemRequest itemRequest)
        {
            _orderRequest.Items.Add(itemRequest);
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider WithItems(
            ICollection<ItemRequest> itemRequests)
        {
            List<ItemRequest> newItems = _orderRequest.Items.ToList();
            newItems.AddRange(itemRequests);
            _orderRequest.Items = newItems;
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider WithNotificationUrl(string notificationUrl)
        {
            _orderRequest.NotificationUrls.Add(notificationUrl);
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider WithNotificationUrls(
            ICollection<string> notificationUrls)
        {
            List<string> newNotificationUrls = _orderRequest.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            _orderRequest.NotificationUrls = newNotificationUrls;
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider WithQrCode(QrCodeRequest qrCodeRequest)
        {
            _orderRequest.QrCodes.Add(qrCodeRequest);
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider WithQrCodes(
            ICollection<QrCodeRequest> qrCodeRequests)
        {
            List<QrCodeRequest> newQrCodes = _orderRequest.QrCodes.ToList();
            newQrCodes.AddRange(qrCodeRequests);
            _orderRequest.QrCodes = newQrCodes;
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider WithReferenceId(string referenceId)
        {
            _orderRequest.ReferenceId = referenceId;
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider WithShipping(Shipping shipping)
        {
            _orderRequest.Shipping = shipping;
            return this;
        }

        /// <inheritdoc />
        public IOrderProvider Load(OrderRequest orderRequest)
        {
            _orderRequest = orderRequest;
            return this;
        }

        /// <inheritdoc />
        public OrderRequest Build()
        {
            OrderRequest order = _orderRequest;
            InitOrder();
            return order;
        }

        /// <inheritdoc />
        public async Task<OrderResponse> CreateAsync()
        {
            OrderResponse orderResponse = await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(_orderRequest)
                .ReceiveJson<OrderResponse>();
            InitOrder();
            return orderResponse;
        }

        /// <inheritdoc />
        public async Task<OrderResponse> GetByIdAsync(string orderId)
        {
            return await Request()
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<OrderResponse>();
        }

        /// <inheritdoc />
        public IBankSlipOrderProvider WithBankSlip()
        {
            OrderRequest orderRequest = Build();
            var chargedOrderProvider = _serviceProvider.GetService<IBankSlipOrderProvider>()!;
            chargedOrderProvider.Load(orderRequest);
            return chargedOrderProvider;
        }

        /// <inheritdoc />
        public ICreditCardOrderProvider WithCreditCard()
        {
            OrderRequest orderRequest = Build();
            var chargedOrderProvider = _serviceProvider.GetService<ICreditCardOrderProvider>()!;
            chargedOrderProvider.Load(orderRequest);
            return chargedOrderProvider;
        }

        /// <inheritdoc />
        public ICreditCardWith3DsAuthOrderProvider WithCreditCardAnd3DsAuthentication()
        {
            OrderRequest orderRequest = Build();
            var chargedOrderProvider = _serviceProvider.GetService<ICreditCardWith3DsAuthOrderProvider>()!;
            chargedOrderProvider.Load(orderRequest);
            return chargedOrderProvider;
        }

        /// <inheritdoc />
        public IDebitCardWith3DsAuthOrderProvider WithDebitCardAnd3DsAuthentication()
        {
            OrderRequest orderRequest = Build();
            var chargedOrderProvider = _serviceProvider.GetService<IDebitCardWith3DsAuthOrderProvider>()!;
            chargedOrderProvider.Load(orderRequest);
            return chargedOrderProvider;
        }
    }
}
