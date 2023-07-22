using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge
{
    public class ChallengeWriteDto : AuthorizationWriteDto
    {
        internal override string GrantType => ApiGrants.Challenge;
        internal string Scope => ApiScopes.CreateCertificate.ToDescription();
    }
}
