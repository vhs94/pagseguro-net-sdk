using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Attributes;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge;
using PagSeguro.DotNet.Sdk.Connect.Helpers;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;

[module: RequiredValidation]
namespace PagSeguro.DotNet.Sdk.Connect.Providers
{
    public class AuthorizationProvider : BaseProvider, IAuthorizationProvider
    {
        private readonly ICryptoService _cryptoService;

        public AuthorizationProvider(
            ICryptoService cryptoService,
            PagSeguroSettings settings)
            : base(settings)
        {
            _cryptoService = cryptoService;
        }

        [ClientApplicationRequired]
        public async Task<AuthorizationCodeReadDto> CreateAccessTokenByCodeAsync(
            AuthorizationCodeWriteDto authorizationCodeWriteDto)
        {
            return await BaseUrl
                .AppendPathSegment(ConnectEndpoints.Token)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(new
                {
                    grant_type = authorizationCodeWriteDto.GrantType,
                    code = authorizationCodeWriteDto.Code,
                    redirect_uri = authorizationCodeWriteDto.RedirectUri,
                    scope = authorizationCodeWriteDto.Scope.ToStringApiScopes()
                })
                .ReceiveJson<AuthorizationCodeReadDto>();
        }

        [ClientApplicationRequired]
        public async Task<ChallengeReadDto> CreateAccessTokenByChallengeAsync()
        {
            var challengeResult = await BaseUrl
                .AppendPathSegment(ConnectEndpoints.Token)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(CommonHeaders.ClientId, Settings.ClientId)
                .WithHeader(CommonHeaders.ClientSecret, Settings.ClientSecret)
                .PostJsonAsync(new
                {
                    grant_type = ApiGrants.Challenge,
                    scope = ApiScopes.CreateCertificate.ToDescription()
                })
                .ReceiveJson<ChallengeReadDto>();

            if (!string.IsNullOrEmpty(Settings.PrivateKey))
            {
                challengeResult.DecryptedChallenge = _cryptoService.Decrypt(challengeResult.Challenge);
            }
            return challengeResult;
        }
    }
}
