using System.Net;
using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class InternalServerErrorException : PagSeguroHttpException
    {
        public InternalServerErrorException(string response)
            : base(
                  HttpStatusCode.InternalServerError,
                  response,
                  ErrorMessages.InternalServerErrorExceptionMessage)
        {
        }
    }
}
