using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface IOrderProvider : IProvider
    {
        Task<OrderReadDto> CreateAsync(OrderWriteDto orderWriteDto);
        Task<OrderReadDto> GetByIdAsync(string orderId);
        IBankSlipOrderProvider WithBankSlip();
        ICreditCardOrderProvider WithCreditCard();
        ICreditCardWith3DsAuthOrderProvider WithCreditCardAnd3DsAuthentication();
        IDebitCardWith3DsAuthOrderProvider WithDebitCardAnd3DsAuthentication();
    }
}
