namespace PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization
{
    public abstract class AuthorizationWriteDto
    {
        internal virtual string GrantType { get; set; } = null!;
    }
}
