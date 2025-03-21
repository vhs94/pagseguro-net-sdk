using PagSeguro.DotNet.Sdk.Common.Attributes;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

[module: RequiredValidation]
namespace PagSeguro.DotNet.Sdk.Common.Tests.Attributes.Providers
{
    public class TestProvider : BaseProvider
    {
        public TestProvider(PagSeguroSettings settings)
            : base(settings)
        {
        }

        [AccessTokenRequired]
        [ChallengeRequired]
        [ClientApplicationRequired]
        [PrivateKeyRequired]
        public Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }
    }
}
