using PagSeguro.DotNet.Sdk.Common.Tests.Attributes.Providers;
using PagSeguro.DotNet.Sdk.Common.Tests.Providers;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Attributes
{
    public class RequiredValidationAttributeTests : BaseProviderTests<TestProvider>
    {
        protected override TestProvider CreateProvider()
        {
            return new TestProvider(Settings);
        }

        [Fact]
        public async Task ExecuteAsync_AttributesAreValid_NoExceptionIsThrown()
        {
            await Provider.ExecuteAsync();
        }
    }
}
