namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Resumo dos valores da cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public class Summary
    {
        /// <summary>
        /// Valor total da cobrança, em centavos.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Valor que foi pago na cobrança, em centavos.
        /// </summary>
        public int Paid { get; set; }
        /// <summary>
        /// Valor que foi devolvido da cobrança, em centavos.
        /// </summary>
        public int Refunded { get; set; }
    }
}
