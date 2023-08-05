using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public abstract class GenericOrderProvider<TChargeWriteDto, TChargeReadDto> : BaseProvider,
        IGenericOrderProvider<TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeDto
        where TChargeReadDto : ChargeDto
    {
        private ChargedOrderWriteDto<TChargeWriteDto> _chargedOrderWriteDto;

        public GenericOrderProvider(PagSeguroSettings settings)
            : base(settings)
        {
            InitOrder();
        }

        private void InitOrder()
        {
            _chargedOrderWriteDto = new ChargedOrderWriteDto<TChargeWriteDto>();
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> AddCharge(TChargeWriteDto chargeWrite)
        {
            _chargedOrderWriteDto.Charges.Add(chargeWrite);
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> AddCharges(
            ICollection<TChargeWriteDto> chargeWriteDtos)
        {
            List<TChargeWriteDto> newCharges = _chargedOrderWriteDto.Charges.ToList();
            newCharges.AddRange(chargeWriteDtos);
            _chargedOrderWriteDto.Charges = newCharges;
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithCustomer(CustomerDto customerDto)
        {
            _chargedOrderWriteDto.Customer = customerDto;
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithItem(ItemWriteDto itemWriteDto)
        {
            _chargedOrderWriteDto.Items.Add(itemWriteDto);
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithItems(
            ICollection<ItemWriteDto> itemWriteDtos)
        {
            List<ItemWriteDto> newItems = _chargedOrderWriteDto.Items.ToList();
            newItems.AddRange(itemWriteDtos);
            _chargedOrderWriteDto.Items = newItems;
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithNotificationUrl(string notificationUrl)
        {
            _chargedOrderWriteDto.NotificationUrls.Add(notificationUrl);
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithNotificationUrls(
            ICollection<string> notificationUrls)
        {
            List<string> newNotificationUrls = _chargedOrderWriteDto.NotificationUrls.ToList();
            newNotificationUrls.AddRange(notificationUrls);
            _chargedOrderWriteDto.NotificationUrls = newNotificationUrls;
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithQrCode(QrCodeWriteDto qrCodeWriteDto)
        {
            _chargedOrderWriteDto.QrCodes.Add(qrCodeWriteDto);
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithQrCodes(
            ICollection<QrCodeWriteDto> qrCodeWriteDtos)
        {
            List<QrCodeWriteDto> newQrCodes = _chargedOrderWriteDto.QrCodes.ToList();
            newQrCodes.AddRange(qrCodeWriteDtos);
            _chargedOrderWriteDto.QrCodes = newQrCodes;
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithReferenceId(string referenceId)
        {
            _chargedOrderWriteDto.ReferenceId = referenceId;
            return this;
        }

        public IGenericOrderProvider<TChargeWriteDto, TChargeReadDto> WithShipping(ShippingDto shippingDto)
        {
            _chargedOrderWriteDto.Shipping = shippingDto;
            return this;
        }

        public ChargedOrderWriteDto<TChargeWriteDto> Build()
        {
            ChargedOrderWriteDto<TChargeWriteDto> order = _chargedOrderWriteDto;
            InitOrder();
            return order;
        }

        public async Task<ChargedOrderReadDto<TChargeReadDto>> CreateAsync()
        {
            var orderReadDto = await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(_chargedOrderWriteDto)
                .ReceiveJson<ChargedOrderReadDto<TChargeReadDto>>();
            InitOrder();
            return orderReadDto;
        }

        public async Task<ChargedOrderReadDto<TChargeReadDto>> GetByIdAsync(string orderId)
        {
            return await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargedOrderReadDto<TChargeReadDto>>();
        }

        public async Task<ChargedOrderReadDto<TChargeReadDto>> PayAsync(string orderId)
        {
            var orderReadtDto = await BaseUrl
                .AppendPathSegments(OrderEndpoint.Orders, orderId, OrderEndpoint.Pay)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    charges = _chargedOrderWriteDto.Charges
                })
                .ReceiveJson<ChargedOrderReadDto<TChargeReadDto>>();
            InitOrder();
            return orderReadtDto;
        }
    }
}
