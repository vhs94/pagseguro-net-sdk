using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public class OrderProvider : BaseProvider, IOrderProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private OrderWriteDto _orderWriteDto;

        public OrderProvider(
            PagSeguroSettings settings,
            IServiceProvider serviceProvider)
            : base(settings)
        {
            _serviceProvider = serviceProvider;
            InitOrder();
        }

        private void InitOrder()
        {
            _orderWriteDto = new OrderWriteDto();
        }

        public IOrderProvider WithCustomer(CustomerDto customerDto)
        {
            _orderWriteDto.Customer = customerDto;
            return this;
        }

        public IOrderProvider WithItem(ItemWriteDto itemWriteDto)
        {
            _orderWriteDto.Items.Add(itemWriteDto);
            return this;
        }

        public IOrderProvider WithItems(
            ICollection<ItemWriteDto> itemWriteDtos)
        {
            List<ItemWriteDto> newItems = _orderWriteDto.Items.ToList();
            newItems.AddRange(itemWriteDtos);
            _orderWriteDto.Items = newItems;
            return this;
        }

        public IOrderProvider WithNotificationUrl(string notificationUrl)
        {
            _orderWriteDto.NotificationUrls.Add(notificationUrl);
            return this;
        }

        public IOrderProvider WithNotificationUrls(
            ICollection<string> notificationUrls)
        {
            List<string> newNotificationUrls = _orderWriteDto.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            _orderWriteDto.NotificationUrls = newNotificationUrls;
            return this;
        }

        public IOrderProvider WithQrCode(QrCodeWriteDto qrCodeWriteDto)
        {
            _orderWriteDto.QrCodes.Add(qrCodeWriteDto);
            return this;
        }

        public IOrderProvider WithQrCodes(
            ICollection<QrCodeWriteDto> qrCodeWriteDtos)
        {
            List<QrCodeWriteDto> newQrCodes = _orderWriteDto.QrCodes.ToList();
            newQrCodes.AddRange(qrCodeWriteDtos);
            _orderWriteDto.QrCodes = newQrCodes;
            return this;
        }

        public IOrderProvider WithReferenceId(string referenceId)
        {
            _orderWriteDto.ReferenceId = referenceId;
            return this;
        }

        public IOrderProvider WithShipping(ShippingDto shippingDto)
        {
            _orderWriteDto.Shipping = shippingDto;
            return this;
        }

        public IOrderProvider Load(OrderWriteDto orderWriteDto)
        {
            _orderWriteDto = orderWriteDto;
            return this;
        }

        public OrderWriteDto Build()
        {
            OrderWriteDto order = _orderWriteDto;
            InitOrder();
            return order;
        }

        public async Task<OrderReadDto> CreateAsync()
        {
            OrderReadDto orderReadDto = await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(_orderWriteDto)
                .ReceiveJson<OrderReadDto>();
            InitOrder();
            return orderReadDto;
        }

        public async Task<OrderReadDto> GetByIdAsync(string orderId)
        {
            return await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<OrderReadDto>();
        }

        public IBankSlipOrderProvider WithBankSlip()
        {
            OrderWriteDto orderWriteDto = Build();
            var chargedOrderProvider = _serviceProvider.GetService<IBankSlipOrderProvider>();
            chargedOrderProvider.Load(orderWriteDto);
            return chargedOrderProvider;
        }

        public ICreditCardOrderProvider WithCreditCard()
        {
            OrderWriteDto orderWriteDto = Build();
            var chargedOrderProvider = _serviceProvider.GetService<ICreditCardOrderProvider>();
            chargedOrderProvider.Load(orderWriteDto);
            return chargedOrderProvider;
        }

        public ICreditCardWith3DsAuthOrderProvider WithCreditCardAnd3DsAuthentication()
        {
            OrderWriteDto orderWriteDto = Build();
            var chargedOrderProvider = _serviceProvider.GetService<ICreditCardWith3DsAuthOrderProvider>();
            chargedOrderProvider.Load(orderWriteDto);
            return chargedOrderProvider;
        }

        public IDebitCardWith3DsAuthOrderProvider WithDebitCardAnd3DsAuthentication()
        {
            OrderWriteDto orderWriteDto = Build();
            var chargedOrderProvider = _serviceProvider.GetService<IDebitCardWith3DsAuthOrderProvider>();
            chargedOrderProvider.Load(orderWriteDto);
            return chargedOrderProvider;
        }
    }
}
