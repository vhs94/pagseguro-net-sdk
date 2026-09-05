using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Models.Requests;
using PagSeguro.DotNet.Sdk.Connect.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Connect.Providers
{
    /// <inheritdoc cref="IAuthorizationProvider" />
    public class AuthorizationProvider : BaseProvider, IAuthorizationProvider
    {
        private readonly ICryptoService _cryptoService;

        public AuthorizationProvider(
            ICryptoService cryptoService,
            PagSeguroSettings settings,
            IFlurlClient flurlClient)
            : base(settings, flurlClient)
        {
            _cryptoService = cryptoService;
        }

        /// <inheritdoc />
        public async Task<AuthorizationCodeResponse> CreateAccessTokenByCodeAsync(
            AuthorizationCodeRequest authorizationCodeRequest)
        {
            EnsureClientApplication();

            return await Request()
                .AppendPathSegment(ConnectEndpoints.Token)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(new
                {
                    grant_type = authorizationCodeRequest.GrantType,
                    code = authorizationCodeRequest.Code,
                    redirect_uri = authorizationCodeRequest.RedirectUri,
                    scope = authorizationCodeRequest.Scope.ToStringApiScopes()
                })
                .ReceiveJson<AuthorizationCodeResponse>();
        }

        /// <inheritdoc />
        public async Task<ChallengeResponse> CreateAccessTokenByChallengeAsync()
        {
            EnsureClientApplication();

            var challengeResult = await Request()
                .AppendPathSegment(ConnectEndpoints.Token)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(new
                {
                    grant_type = ApiGrants.Challenge,
                    scope = ApiScopes.CreateCertificate.ToDescription()
                })
                .ReceiveJson<ChallengeResponse>();

            if (!string.IsNullOrEmpty(Settings.PrivateKey))
            {
                challengeResult.DecryptedChallenge = _cryptoService.Decrypt(challengeResult.Challenge!);
            }
            return challengeResult;
        }

        /// <inheritdoc />
        public async Task<AuthorizationCodeResponse> RefreshAccessTokenAsync(
            RefreshTokenRequest refreshTokenRequest)
        {
            EnsureClientApplication();

            return await Request()
                .AppendPathSegment(ConnectEndpoints.Refresh)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(new
                {
                    grant_type = refreshTokenRequest.GrantType,
                    refresh_token = refreshTokenRequest.RefreshToken
                })
                .ReceiveJson<AuthorizationCodeResponse>();
        }

        /// <inheritdoc />
        public async Task RevokeTokenAsync(RevokeTokenRequest revokeTokenRequest)
        {
            EnsureClientApplication();

            await Request()
                .AppendPathSegment(ConnectEndpoints.Revoke)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(new
                {
                    token = revokeTokenRequest.Token,
                    token_type_hint = revokeTokenRequest.TokenTypeHint.ToDescription()
                });
        }
    }
}
