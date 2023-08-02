using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Validations
{
    public class ClientNotConnectedWithChallengeException : Exception
    {
        public ClientNotConnectedWithChallengeException()
            : base(ErrorMessages.ClientNotConnectedWithChallengeExceptionMessage)
        {
        }
    }
}
