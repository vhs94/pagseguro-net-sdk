namespace PagSeguro.DotNet.Sdk.Connect.Models.Requests
{
    public abstract class AuthorizationRequest
    {
        internal virtual string GrantType { get; set; } = null!;
    }
}
