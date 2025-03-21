using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;


namespace PagSeguro.DotNet.Sdk.Common.Attributes
{
    public class PrivateKeyRequiredAttribute : RequiredValidationAttribute
    {
        protected override bool IsValid()
            => !string.IsNullOrEmpty(Provider.Settings.PrivateKey);

        protected override Exception CreateCustomException()
            => new PrivateKeyNotFoundException();
    }
}
