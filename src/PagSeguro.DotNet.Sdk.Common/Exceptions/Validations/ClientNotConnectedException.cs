using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Validations
{
    public class ClientNotConnectedException : Exception
    {
        public ClientNotConnectedException()
            : base(ErrorMessages.ClientNotConnectedExceptionMessage)
        {
        }
    }
}
