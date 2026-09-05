using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    /// <summary>
    /// Cobertura viva dos endpoints /oauth2/refresh e /oauth2/revoke.
    /// </summary>
    /// <remarks>
    /// O caminho feliz (rotacionar um token de verdade) nao e automatizavel: obter um
    /// refresh_token exige o fluxo authorization_code, que depende do consentimento do
    /// usuario no navegador. O unico grant que roda sem interacao (challenge) nao devolve
    /// refresh_token. Estes testes entao validam o encanamento contra o sandbox real:
    /// a URL, o bearer token, os headers X_CLIENT_ID/X_CLIENT_SECRET e o corpo JSON sao
    /// aceitos pelo PagBank, e apenas o VALOR do token e recusado (41001 invalid_request).
    /// O tipo BadRequestException e a propria asserção de que a rota existe: uma URL errada
    /// devolveria NotFoundException e credenciais erradas, UnauthorizedException.
    /// </remarks>
    public class AuthorizationIntegrationTests : BaseIntegrationTests
    {
        private const string InvalidToken = "invalid-token-for-integration-test";

        [Fact]
        public async Task RefreshAccessTokenAsync_RefreshTokenIsInvalid_ApiRejectsOnlyTheToken()
        {
            RefreshTokenRequest refreshTokenRequest = new()
            {
                RefreshToken = InvalidToken
            };

            Func<Task> task = async () => await Client
                .ForAuthorization()
                .RefreshAccessTokenAsync(refreshTokenRequest);

            var assertion = await task.Should().ThrowAsync<BadRequestException>();
            assertion.Which.Response.Should().Contain("refresh_token");
            assertion.Which.Response.Should().Contain("invalid_request");
        }

        [Fact]
        public async Task RevokeTokenAsync_TokenIsInvalid_ApiRejectsOnlyTheToken()
        {
            // IMPORTANTE: nunca informe aqui o token real de Settings. Revoga-lo invalidaria
            // as credenciais compartilhadas de sandbox usadas por todos os outros testes de
            // integracao, de forma permanente.
            RevokeTokenRequest revokeTokenRequest = new()
            {
                Token = InvalidToken,
                TokenTypeHint = TokenTypeHint.RefreshToken
            };

            Func<Task> task = async () => await Client
                .ForAuthorization()
                .RevokeTokenAsync(revokeTokenRequest);

            var assertion = await task.Should().ThrowAsync<BadRequestException>();
            assertion.Which.Response.Should().Contain("token");
            assertion.Which.Response.Should().Contain("invalid_request");
        }
    }
}
