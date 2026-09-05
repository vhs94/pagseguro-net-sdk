namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Pagamento gerado para um recebedor da divisão.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-divisao-do-pagamento">ler documentação</see>
    /// </summary>
    public class SplitPaymentReference
    {
        /// <summary>Identificador do pagamento do recebedor.</summary>
        public string? Id { get; set; }
    }
}
