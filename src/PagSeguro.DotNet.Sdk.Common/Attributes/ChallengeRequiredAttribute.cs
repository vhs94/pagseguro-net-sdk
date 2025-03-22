using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;

namespace PagSeguro.DotNet.Sdk.Common.Attributes
{
    public class ChallengeRequiredAttribute : RequiredValidationAttribute
    {
        protected override bool IsValid()
            => !string.IsNullOrEmpty(Provider.Settings.Challenge);

        protected override Exception CreateCustomException()
            => new ClientNotConnectedWithChallengeException();
    }
}
