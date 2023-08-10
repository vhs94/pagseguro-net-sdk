using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Fees;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Fees
{
    public class FeeProvider : BaseProvider, IFeeProvider
    {
        public FeeWriteDto Entity { get; set; }

        public FeeProvider(PagSeguroSettings settings)
            : base(settings)
        {
            Reset();
        }

        public void Reset()
        {
            Entity = new FeeWriteDto();
        }
    }
}
