namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Taxas aplicadas ao valor da transação.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class FeesInfo
    {
        /// <summary>
        /// Juros repassados ao comprador.
        /// </summary>
        public Buyer? Buyer { get; set; }
    }
}
