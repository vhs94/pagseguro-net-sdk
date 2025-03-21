using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard
{
    public abstract class ChargeByCardWriteDto : ChargeByCardDto, IChargeWriteDto
    {
        public ChargeAmountWriteDto? Amount { get; set; }
    }
}
