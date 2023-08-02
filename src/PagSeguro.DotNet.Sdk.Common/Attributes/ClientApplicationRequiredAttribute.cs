using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;

namespace PagSeguro.DotNet.Sdk.Common.Attributes
{
    public class ClientApplicationRequiredAttribute : RequiredValidationAttribute
    {
        protected override bool IsValid()
        {
            return !string.IsNullOrEmpty(Provider.Settings.ClientId)
                && !string.IsNullOrEmpty(Provider.Settings.ClientSecret);
        }

        protected override Exception CreateCustomException()
        {
            return new MissingClientApplicationException();
        }
    }
}
