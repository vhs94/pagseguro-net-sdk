using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.DebitCard;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface IOrderProvider : IProvider
    {
        Task<OrderReadDto> CreateAsync(OrderWriteDto orderWriteDto);
        Task<OrderReadDto> GetByIdAsync(string orderId);
        IGenericOrderProvider<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto> WithBankSlip();
        IGenericOrderProvider<ChargeByCreditCardWriteDto, ChargeByCreditCardReadDto> WithCreditCard();
        IGenericOrderProvider<ChargeByCreditCardWith3DsAuthWriteDto, ChargeByCreditCardWith3DsAuthReadDto> WithCreditCardAnd3DsAuthentication();
        IGenericOrderProvider<ChargeByDebitCardWith3DsAuthWriteDto, ChargeByDebitCardWith3DsAuthReadDto> WithDebitCardAnd3DsAuthentication();
    }
}
