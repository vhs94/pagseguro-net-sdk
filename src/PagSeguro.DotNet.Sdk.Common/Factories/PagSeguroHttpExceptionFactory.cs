using System.Net;
using Flurl.Http;
using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Common.Dtos;
using PagSeguro.DotNet.Sdk.Common.Exceptions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Common.Factories
{
    public class PagSeguroHttpExceptionFactory : IPagSeguroHttpExceptionFactory
    {
        public async Task<Exception> CreateHttpExceptionAsync(IFlurlResponse response)
        {
            string responseBody = await response.GetStringAsync();
            HttpStatusCode httpStatusCode = (HttpStatusCode)response.StatusCode;
            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    var badRequestResponseBody = JsonConvert.DeserializeObject<BadRequestResponseDto>(responseBody);
                    return new BadRequestException(badRequestResponseBody);
                case HttpStatusCode.Conflict:
                    return new ConflictException(responseBody);
                case HttpStatusCode.Forbidden:
                    return new ForbiddenException(responseBody);
                case HttpStatusCode.InternalServerError:
                    return new InternalServerErrorException(responseBody);
                case HttpStatusCode.NotAcceptable:
                    return new NotAcceptableException(responseBody);
                case HttpStatusCode.NotFound:
                    return new NotFoundException(responseBody);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedException(responseBody);
                default:
                    return new PagSeguroHttpException(
                        httpStatusCode,
                        responseBody,
                        ErrorMessages.DefaultHttpExceptionMessage);
            }
        }
    }
}
