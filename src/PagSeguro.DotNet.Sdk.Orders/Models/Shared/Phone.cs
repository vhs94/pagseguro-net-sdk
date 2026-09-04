namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Telefone de contato do cliente.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Código de operadora do País (DDI). 2 caracteres.
        /// </summary>
        public string? Country { get; set; }
        /// <summary>
        /// Código de operadora local (DDD). 2 caracteres.
        /// </summary>
        public string? Area { get; set; }
        /// <summary>
        /// Número do telefone. De 8 a 9 caracteres.
        /// </summary>
        public string? Number { get; set; }
        /// <summary>
        /// Tipo do telefone. Valores possíveis: MOBILE, BUSINESS e HOME.
        /// </summary>
        public string? Type { get; set; }
    }
}
