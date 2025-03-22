using PagSeguro.DotNet.Sdk.Common.Attributes;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;

[module: RequiredValidation]
namespace PagSeguro.DotNet.Sdk.Common.Tests.Attributes.Providers
{
    public class TestProvider(PagSeguroSettings settings) : BaseProvider(settings)
    {
        [AccessTokenRequired]
        [ChallengeRequired]
        [ClientApplicationRequired]
        [PrivateKeyRequired]
        public Task ExecuteAsync() => Task.CompletedTask;
    }
}
