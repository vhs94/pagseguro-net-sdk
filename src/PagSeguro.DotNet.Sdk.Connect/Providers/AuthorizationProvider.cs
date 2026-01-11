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
    public class AuthorizationProvider(PagSeguroSettings settings)
        : BaseProvider(settings),
        IAuthorizationProvider
    {
        public async Task<AuthorizationCodeResponse> CreateAccessTokenByCodeAsync(AuthorizationCodeRequest authorizationCodeRequest)
        {
            EnsureClientApplication();

            return await BaseUrl
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

        public async Task<ChallengeResponse> CreateAccessTokenByChallengeAsync()
        {
            EnsureClientApplication();
            EnsurePrivateKey();
            EnsureChallenge();

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
                .ReceiveJson<ChallengeResponse>();

            if (!string.IsNullOrEmpty(Settings.PrivateKey))
            {
                challengeResult.DecryptedChallenge = CryptoHelper.DecryptRsa(Settings.PrivateKey, challengeResult.Challenge!);
            }
            return challengeResult;
        }
    }
}
