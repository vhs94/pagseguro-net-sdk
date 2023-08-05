using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;

namespace PagSeguro.DotNet.Sdk.Common.Attributes
{
    public class ChallengeRequiredAttribute : RequiredValidationAttribute
    {
        protected override bool IsValid()
        {
            return !string.IsNullOrEmpty(Provider.Settings.Challenge);
        }

        protected override Exception CreateCustomException()
        {
            return new ClientNotConnectedWithChallengeException();
        }
    }
}
