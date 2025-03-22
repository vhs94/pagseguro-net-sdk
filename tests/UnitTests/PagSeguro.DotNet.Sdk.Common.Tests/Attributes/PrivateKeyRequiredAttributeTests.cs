using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Attributes
{
    public class PrivateKeyRequiredAttributeTests : RequiredValidationAttributeTests
    {
        [Fact]
        public async Task ExecuteAsync_PrivateKeyIsEmpty_PrivateKeyNotFoundExceptionIsThrown()
        {
            Settings.PrivateKey = null;

            Func<Task> task = Provider.ExecuteAsync;

            await task
                .Should()
                .ThrowAsync<PrivateKeyNotFoundException>();
        }
    }
}
