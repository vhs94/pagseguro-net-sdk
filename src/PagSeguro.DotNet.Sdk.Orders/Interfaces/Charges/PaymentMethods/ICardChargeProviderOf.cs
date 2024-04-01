using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    public interface ICardChargeProviderOf<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        : IChargeProviderOf<TTopLevelProvider, TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeByCardWriteDto, IChargeWriteDto, new()
        where TChargeReadDto : ChargeByCardReadDto
        where TTopLevelProvider : IChargeProvider<TChargeWriteDto, TChargeReadDto>
    {
        public TTopLevelProvider WithMetadata(IDictionary<string, string> metadata)
        {
            ChargeWriteDto.Metadata = metadata;
            return (TTopLevelProvider)this;
        }
    }
}
