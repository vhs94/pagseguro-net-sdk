using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions
{
    public class ClientNotConnectedWithChallengeException : Exception
    {
        public ClientNotConnectedWithChallengeException()
            : base(ErrorMessages.ClientNotConnectedWithChallengeExceptionMessage)
        {
        }
    }
}
