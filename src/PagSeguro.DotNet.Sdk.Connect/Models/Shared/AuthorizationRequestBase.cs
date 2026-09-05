namespace PagSeguro.DotNet.Sdk.Connect.Models.Shared
{
    /// <summary>
    /// Dados comuns das requisições de emissão de access_token via OAuth2.
    /// <see href="https://developer.pagbank.com.br/reference/obter-access-token">ler documentação</see>
    /// </summary>
    public abstract class AuthorizationRequestBase
    {
        internal virtual string GrantType { get; set; } = null!;
    }
}
