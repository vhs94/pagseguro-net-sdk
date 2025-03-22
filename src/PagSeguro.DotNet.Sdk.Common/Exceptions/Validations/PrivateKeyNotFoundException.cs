using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Validations
{
    public class PrivateKeyNotFoundException : Exception
    {
        public PrivateKeyNotFoundException()
            : base(ErrorMessages.PrivateKeyNotFoundMessage)
        {
        }
    }
}
