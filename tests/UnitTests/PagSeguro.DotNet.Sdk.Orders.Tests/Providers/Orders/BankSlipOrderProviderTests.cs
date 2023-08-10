using FluentAssertions;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Tests.Providers.Orders
{
    public class BankSlipOrderProviderTests : ChargedOrderProviderOfTests<ChargeByBankSlipWriteDto, ChargeByBankSlipReadDto>
    {
        protected override BankSlipOrderProvider CreateProvider()
        {
            return new BankSlipOrderProvider(Settings, MapperMock);
        }

        protected override void AssertChargeResponse(
            ChargedOrderReadDto<ChargeByBankSlipReadDto> expectedReadDto,
            ChargedOrderReadDto<ChargeByBankSlipReadDto> receivedReadDto)
        {
            receivedReadDto
                .Should()
                .BeEquivalentTo(
                    expectedReadDto,
                    options => options.Excluding(f => f.Charges));
            receivedReadDto
                .Charges
                .Should()
                .BeEquivalentTo(expectedReadDto.Charges,
                    options => options.Excluding(f => f.PaymentMethod.BankSlip.DueDate));
            receivedReadDto
                .Charges
                .Select(cg => cg.PaymentMethod.BankSlip.DueDate)
                .Should()
                .BeEquivalentTo(expectedReadDto.Charges.Select(cg => cg.PaymentMethod.BankSlip.DueDate.Date));
        }
    }
}
