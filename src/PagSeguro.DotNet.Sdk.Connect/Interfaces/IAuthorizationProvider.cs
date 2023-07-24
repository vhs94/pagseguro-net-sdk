using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge;

namespace PagSeguro.DotNet.Sdk.Connect.Interfaces
{
    public interface IAuthorizationProvider : IProvider
    {
        Task<AuthorizationCodeReadDto> CreateAccessTokenByCodeAsync(
            AuthorizationCodeWriteDto authorizationCodeWriteDto);
        Task<ChallengeReadDto> CreateAccessTokenByChallengeAsync();
    }
}
