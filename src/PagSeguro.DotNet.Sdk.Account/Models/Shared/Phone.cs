namespace PagSeguro.DotNet.Sdk.Account.Models.Shared
{
    /// <summary>
    /// Telefone de contato informado no cadastro da conta PagBank.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-account">ler documentação</see>
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Código de operadora local (DDD). 2 caracteres.
        /// </summary>
        public string? Area { get; set; }
        /// <summary>
        /// Código de operadora do País (DDI). 2 caracteres.
        /// </summary>
        public string? Country { get; set; }
        /// <summary>
        /// Número do telefone. De 8 a 9 caracteres.
        /// </summary>
        public string? Number { get; set; }
    }
}
