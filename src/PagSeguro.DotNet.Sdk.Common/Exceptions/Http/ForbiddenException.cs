using System.Net;
using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class ForbiddenException : PagSeguroHttpException
    {
        public ForbiddenException(string response)
            : base(
                  HttpStatusCode.Forbidden,
                  response,
                  ErrorMessages.ForbiddenExceptionMessage)
        {
        }
    }
}
