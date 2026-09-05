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

        /// <remarks>
        /// O caminho feliz do Connect via SMS nao e automatizavel: o SMS vai para o
        /// telefone cadastrado de uma conta de vendedor de verdade, e as credenciais
        /// compartilhadas de sandbox nao sao uma. O 403 MERCHANT_ACCOUNT_REQUIRED e a
        /// prova de que a rota, os headers e o corpo foram aceitos: um corpo mal
        /// formado para antes, em 400 INVALID_PARAMETER (foi o que aconteceu ao
        /// enviar o numero da conta sem o digito verificador).
        /// </remarks>
        [Fact]
        public async Task RequestSmsAuthorizationAsync_AccountIsNotAMerchant_ApiRejectsOnlyTheAccount()
        {
            SmsAuthorizationRequest smsAuthorizationRequest = new()
            {
                BankBranch = "0001",
                AccountNumber = "12345678-9"
            };

            Func<Task> task = async () => await Client
                .ForAuthorization()
                .RequestSmsAuthorizationAsync(smsAuthorizationRequest);

            var assertion = await task.Should().ThrowAsync<ForbiddenException>();
            assertion.Which.Response.Should().Contain("MERCHANT_ACCOUNT_REQUIRED");
        }

        [Fact]
        public async Task RequestSmsAuthorizationAsync_AccountNumberIsMalformed_ApiRejectsTheFormat()
        {
            // Confirma que account_number chega mesmo a API: sem o digito
            // verificador a validacao de formato reprova antes do 403 acima.
            SmsAuthorizationRequest smsAuthorizationRequest = new()
            {
                BankBranch = "0001",
                AccountNumber = "12345678"
            };

            Func<Task> task = async () => await Client
                .ForAuthorization()
                .RequestSmsAuthorizationAsync(smsAuthorizationRequest);

            var assertion = await task.Should().ThrowAsync<BadRequestException>();
            assertion.Which.Response.Should().Contain("account_number");
        }

        [Fact]
        public async Task CreateAccessTokenBySmsAsync_ThereIsNoPendingAuthorization_ApiAcceptsThePayload()
        {
            SmsTokenRequest smsTokenRequest = new()
            {
                AuthorizationId = "AUTH_00000000-0000-0000-0000-000000000000",
                SmsCode = "123456"
            };

            Func<Task> task = async () => await Client
                .ForAuthorization()
                .CreateAccessTokenBySmsAsync(smsTokenRequest);

            // NO_PENDING_AUTHORIZATION significa que grant_type=sms foi reconhecido
            // e que authorization_id e sms_code passaram pela validacao de formato.
            var assertion = await task.Should().ThrowAsync<BadRequestException>();
            assertion.Which.Response.Should().Contain("NO_PENDING_AUTHORIZATION");
        }

        [Fact]
        public async Task CreateAccessTokenBySmsAsync_SmsCodeIsMissing_ApiRejectsTheCode()
        {
            SmsTokenRequest smsTokenRequest = new()
            {
                AuthorizationId = "AUTH_00000000-0000-0000-0000-000000000000"
            };

            Func<Task> task = async () => await Client
                .ForAuthorization()
                .CreateAccessTokenBySmsAsync(smsTokenRequest);

            var assertion = await task.Should().ThrowAsync<BadRequestException>();
            assertion.Which.Response.Should().Contain("sms_code");
        }
    }
}
