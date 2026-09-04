namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Portador do cartão.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public class Holder
    {
        /// <summary>
        /// Nome do portador. De 1 a 30 caracteres.
        /// </summary>
        public string? Name { get; set; }
    }
}
