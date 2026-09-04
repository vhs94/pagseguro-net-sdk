namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Juros cobrados do comprador no parcelamento.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class Interest
    {
        /// <summary>
        /// Valor total dos juros, em centavos.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Quantidade de parcelas às quais os juros se aplicam.
        /// </summary>
        public int Installments { get; set; }
    }
}
