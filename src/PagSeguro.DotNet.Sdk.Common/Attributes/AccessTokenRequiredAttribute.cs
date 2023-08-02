using PagSeguro.DotNet.Sdk.Common.Exceptions.Validations;

namespace PagSeguro.DotNet.Sdk.Common.Attributes
{
    public class AccessTokenRequiredAttribute : RequiredValidationAttribute
    {
        protected override bool IsValid()
        {
            return !string.IsNullOrEmpty(Provider.Settings.AccessToken);
        }

        protected override Exception CreateCustomException()
        {
            return new ClientNotConnectedException();
        }
    }
}
