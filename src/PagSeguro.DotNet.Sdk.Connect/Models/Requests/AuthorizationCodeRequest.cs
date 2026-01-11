using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Connect.Models.Requests
{
    public class AuthorizationCodeRequest : AuthorizationRequest
    {
        internal override string GrantType => ApiGrants.AuthorizationCode;
        public string? Code { get; set; }
        public string? RedirectUri { get; set; }
        public ApiScopes? Scope { get; set; }
    }
}
