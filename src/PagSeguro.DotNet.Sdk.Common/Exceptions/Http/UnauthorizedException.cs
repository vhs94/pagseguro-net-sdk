using System.Net;
using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class UnauthorizedException : PagSeguroHttpException
    {
        public UnauthorizedException(string response)
            : base(
                  HttpStatusCode.Unauthorized,
                  response,
                  ErrorMessages.UnauthorizedExceptionMessage)
        {
        }
    }
}
