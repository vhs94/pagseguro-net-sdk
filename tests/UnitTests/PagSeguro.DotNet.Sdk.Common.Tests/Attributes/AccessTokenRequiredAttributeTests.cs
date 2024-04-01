using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Attributes
{
    public class AccessTokenRequiredAttributeTests : RequiredValidationAttributeTests
    {
        [Fact]
        public async Task ExecuteAsync_AccessTokenIsEmpty_ClientNotConnectedExceptionIsThrown()
        {
            Settings.AccessToken = null;

            Func<Task> task = Provider.ExecuteAsync;

            await task
                .Should()
                .ThrowAsync<ClientNotConnectedException>();
        }
    }
}
