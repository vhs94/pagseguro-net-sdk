using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Attributes
{
    public class ClientApplicationRequiredAttributeTests : RequiredValidationAttributeTests
    {
        [Fact]
        public async Task ExecuteAsync_ClientIdIsEmpty_MissingClientApplicationExceptionIsThrown()
        {
            Settings.ClientId = null;

            Func<Task> task = Provider.ExecuteAsync;

            await task
                .Should()
                .ThrowAsync<MissingClientApplicationException>();
        }

        [Fact]
        public async Task ExecuteAsync_ClientSecretIsEmpty_MissingClientApplicationExceptionIsThrown()
        {
            Settings.ClientSecret = null;

            Func<Task> task = Provider.ExecuteAsync;

            await task
                .Should()
                .ThrowAsync<MissingClientApplicationException>();
        }
    }
}
