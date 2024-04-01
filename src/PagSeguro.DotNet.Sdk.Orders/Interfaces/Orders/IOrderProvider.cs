using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface IOrderProvider : IProvider
    {
        IOrderProvider WithCustomer(CustomerDto customerDto);
        IOrderProvider WithItem(ItemWriteDto itemWriteDto);
        IOrderProvider WithItems(ICollection<ItemWriteDto> itemWriteDtos);
        IOrderProvider WithNotificationUrl(string notificationUrl);
        IOrderProvider WithNotificationUrls(ICollection<string> notificationUrls);
        IOrderProvider WithQrCode(QrCodeWriteDto qrCodeWriteDto);
        IOrderProvider WithQrCodes(ICollection<QrCodeWriteDto> qrCodeWriteDtos);
        IOrderProvider WithReferenceId(string referenceId);
        IOrderProvider WithShipping(ShippingDto shippingDto);
        IOrderProvider Load(OrderWriteDto orderWriteDto);
        OrderWriteDto Build();
        IBankSlipOrderProvider WithBankSlip();
        ICreditCardOrderProvider WithCreditCard();
        ICreditCardWith3DsAuthOrderProvider WithCreditCardAnd3DsAuthentication();
        IDebitCardWith3DsAuthOrderProvider WithDebitCardAnd3DsAuthentication();
        Task<OrderReadDto> CreateAsync();
        Task<OrderReadDto> GetByIdAsync(string orderId);
    }
}
