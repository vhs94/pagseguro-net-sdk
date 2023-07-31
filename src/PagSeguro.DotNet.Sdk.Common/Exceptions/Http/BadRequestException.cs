using System.Net;
using PagSeguro.DotNet.Sdk.Common.Dtos;
using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions
{
    public class BadRequestException : BasePagSeguroHttpException<BadRequestResponseDto>
    {
        public BadRequestException(BadRequestResponseDto response)
            : base(
                  HttpStatusCode.BadRequest,
                  response,
                  ErrorMessages.BadRequestExceptionMessage)
        {
        }
    }
}
