using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard.CreditCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    public interface ICreditCardOrderProvider
        : IChargedOrderProviderOf<
            ChargeByCreditCardWriteDto,
            ChargeByCreditCardReadDto>
    {
    }
}
