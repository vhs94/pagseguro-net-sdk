using System.Net;
using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class NotFoundException : PagSeguroHttpException
    {
        public NotFoundException(string response)
            : base(
                  HttpStatusCode.NotFound,
                  response,
                  ErrorMessages.NotFoundExceptionMessage)
        {
        }
    }
}
