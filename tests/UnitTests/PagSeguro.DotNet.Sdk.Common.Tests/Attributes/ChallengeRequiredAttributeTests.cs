using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Attributes
{
    public class ChallengeRequiredAttributeTests : RequiredValidationAttributeTests
    {
        [Fact]
        public async Task ExecuteAsync_ChallengeIsEmpty_ClientNotConnectedWithChallengeExceptionIsThrown()
        {
            Settings.Challenge = null;

            Func<Task> task = Provider.ExecuteAsync;

            await task
                .Should()
                .ThrowAsync<ClientNotConnectedWithChallengeException>();
        }
    }
}
