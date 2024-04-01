using System.Net;
using FluentAssertions;
using Flurl.Http;
using NSubstitute;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;
using PagSeguro.DotNet.Sdk.Common.Factories;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Common.Tests.Factories
{
    public class PagSeguroHttpExceptionFactoryTests : BaseTests
    {
        private IPagSeguroHttpExceptionFactory _factory;
        private IFlurlResponse _response;
        private string _responseBody;

        protected override void InitializeMocks()
        {
            _factory = new PagSeguroHttpExceptionFactory();
            _response = Substitute.For<IFlurlResponse>();
            _responseBody = Guid.NewGuid().ToString();
            _response
                .GetStringAsync()
                .Returns(_responseBody);
        }

        [Fact]
        public async Task CreateHttpExceptionAsync_ResponseIsBadRequest_BadRequestExceptionIsReturned()
        {
            _response
                .StatusCode
                .Returns((int)HttpStatusCode.BadRequest);

            PagSeguroHttpException httpException = await _factory.CreateHttpExceptionAsync(_response);

            httpException
                .Should()
                .BeOfType<BadRequestException>();
            var badRequestException = (BadRequestException)httpException;
            badRequestException.Message
                .Should()
                .Be(ErrorMessages.BadRequestExceptionMessage);
            badRequestException.StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);
            badRequestException.Response
                .Should()
                .BeEquivalentTo(_responseBody);
        }

        [Fact]
        public async Task CreateHttpExceptionAsync_ResponseIsConflict_ConflictExceptionIsReturned()
        {
            _response
                .StatusCode
                .Returns((int)HttpStatusCode.Conflict);

            PagSeguroHttpException httpException = await _factory.CreateHttpExceptionAsync(_response);

            httpException
                .Should()
                .BeOfType<ConflictException>();
            var conflictException = (ConflictException)httpException;
            conflictException.Message
                .Should()
                .Be(ErrorMessages.ConflicExceptionMessage);
            conflictException.StatusCode
                .Should()
                .Be(HttpStatusCode.Conflict);
            conflictException.Response
                .Should()
                .BeEquivalentTo(_responseBody);
        }

        [Fact]
        public async Task CreateHttpExceptionAsync_ResponseIsForbidden_ForbiddenExceptionIsReturned()
        {
            _response
                .StatusCode
                .Returns((int)HttpStatusCode.Forbidden);

            PagSeguroHttpException httpException = await _factory.CreateHttpExceptionAsync(_response);

            httpException
                .Should()
                .BeOfType<ForbiddenException>();
            var forbiddenException = (ForbiddenException)httpException;
            forbiddenException.Message
                .Should()
                .Be(ErrorMessages.ForbiddenExceptionMessage);
            forbiddenException.StatusCode
                .Should()
                .Be(HttpStatusCode.Forbidden);
            forbiddenException.Response
                .Should()
                .BeEquivalentTo(_responseBody);
        }

        [Fact]
        public async Task CreateHttpExceptionAsync_ResponseIsInternalServerError_InternalServerErrorExceptionIsReturned()
        {
            _response
                .StatusCode
                .Returns((int)HttpStatusCode.InternalServerError);

            PagSeguroHttpException httpException = await _factory.CreateHttpExceptionAsync(_response);

            httpException
                .Should()
                .BeOfType<InternalServerErrorException>();
            var internalServerErrorException = (InternalServerErrorException)httpException;
            internalServerErrorException.Message
                .Should()
                .Be(ErrorMessages.InternalServerErrorExceptionMessage);
            internalServerErrorException.StatusCode
                .Should()
                .Be(HttpStatusCode.InternalServerError);
            internalServerErrorException.Response
                .Should()
                .BeEquivalentTo(_responseBody);
        }

        [Fact]
        public async Task CreateHttpExceptionAsync_ResponseIsNotAcceptable_NotAcceptableExceptionIsReturned()
        {
            _response
                .StatusCode
                .Returns((int)HttpStatusCode.NotAcceptable);

            PagSeguroHttpException httpException = await _factory.CreateHttpExceptionAsync(_response);

            httpException
                .Should()
                .BeOfType<NotAcceptableException>();
            var notAcceptableException = (NotAcceptableException)httpException;
            notAcceptableException.Message
                .Should()
                .Be(ErrorMessages.NotAcceptableExceptionMessage);
            notAcceptableException.StatusCode
                .Should()
                .Be(HttpStatusCode.NotAcceptable);
            notAcceptableException.Response
                .Should()
                .BeEquivalentTo(_responseBody);
        }

        [Fact]
        public async Task CreateHttpExceptionAsync_ResponseIsNotFound_NotFoundExceptionIsReturned()
        {
            _response
                .StatusCode
                .Returns((int)HttpStatusCode.NotFound);

            PagSeguroHttpException httpException = await _factory.CreateHttpExceptionAsync(_response);

            httpException
                .Should()
                .BeOfType<NotFoundException>();
            var notFoundException = (NotFoundException)httpException;
            notFoundException.Message
                .Should()
                .Be(ErrorMessages.NotFoundExceptionMessage);
            notFoundException.StatusCode
                .Should()
                .Be(HttpStatusCode.NotFound);
            notFoundException.Response
                .Should()
                .BeEquivalentTo(_responseBody);
        }

        [Fact]
        public async Task CreateHttpExceptionAsync_ResponseIsUnauthorized_UnauthorizedExceptionIsReturned()
        {
            _response
                .StatusCode
                .Returns((int)HttpStatusCode.Unauthorized);

            PagSeguroHttpException httpException = await _factory.CreateHttpExceptionAsync(_response);

            httpException
                .Should()
                .BeOfType<UnauthorizedException>();
            var unauthorizedException = (UnauthorizedException)httpException;
            unauthorizedException.Message
                .Should()
                .Be(ErrorMessages.UnauthorizedExceptionMessage);
            unauthorizedException.StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);
            unauthorizedException.Response
                .Should()
                .BeEquivalentTo(_responseBody);
        }

        [Fact]
        public async Task CreateHttpExceptionAsync_ResponseIsNotMapped_PagSeguroHttpExceptionIsReturned()
        {
            _response
                .StatusCode
                .Returns((int)HttpStatusCode.BadGateway);

            PagSeguroHttpException httpException = await _factory.CreateHttpExceptionAsync(_response);

            httpException
                .Should()
                .BeOfType<UnknownHttpException>();
            var unknownHttpException = (UnknownHttpException)httpException;
            unknownHttpException.Message
                .Should()
                .Be(ErrorMessages.UnkownHttpExceptionMessage);
            unknownHttpException.StatusCode
                .Should()
                .Be(HttpStatusCode.BadGateway);
            unknownHttpException.Response
                .Should()
                .BeEquivalentTo(_responseBody);
        }
    }
}
