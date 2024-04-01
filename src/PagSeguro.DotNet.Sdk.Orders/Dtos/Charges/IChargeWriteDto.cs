using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public interface IChargeWriteDto
    {
        public ChargeAmountWriteDto Amount { get; set; }
    }
}
