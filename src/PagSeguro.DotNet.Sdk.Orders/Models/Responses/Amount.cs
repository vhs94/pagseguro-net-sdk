namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Valor total de um plano de parcelamento e as taxas aplicadas.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class Amount
    {
        /// <summary>
        /// Valor total, em centavos.
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Código de moeda no padrão ISO.
        /// </summary>
        public string? Currency { get; set; }
        /// <summary>
        /// Taxas aplicadas ao valor.
        /// </summary>
        public FeesInfo? Fees { get; set; }
    }
}
