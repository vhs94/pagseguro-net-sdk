using System.Net;
using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class NotAcceptableException : PagSeguroHttpException
    {
        public NotAcceptableException(string response)
            : base(
                  HttpStatusCode.NotAcceptable,
                  response,
                  ErrorMessages.NotAcceptableExceptionMessage)
        {
        }
    }
}
