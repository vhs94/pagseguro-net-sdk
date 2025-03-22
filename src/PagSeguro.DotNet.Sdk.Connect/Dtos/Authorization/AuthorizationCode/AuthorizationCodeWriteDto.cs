using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode
{
    public class AuthorizationCodeWriteDto : AuthorizationWriteDto
    {
        internal override string GrantType => ApiGrants.AuthorizationCode;
        public string? Code { get; set; }
        public string? RedirectUri { get; set; }
        public ApiScopes? Scope { get; set; }
    }
}
