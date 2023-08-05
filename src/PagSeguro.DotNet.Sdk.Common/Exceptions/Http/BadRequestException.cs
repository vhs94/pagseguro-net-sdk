using System.Net;
using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class BadRequestException : PagSeguroHttpException
    {
        public BadRequestException(string response)
            : base(
                  HttpStatusCode.BadRequest,
                  response,
                  ErrorMessages.BadRequestExceptionMessage)
        {
        }
    }
}
