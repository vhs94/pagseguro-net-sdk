namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Contrato comum das cobranças que podem ser enviadas em um pedido.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public interface IChargeRequest
    {
        /// <summary>
        /// Valor da cobrança.
        /// </summary>
        public ChargeAmountRequest? Amount { get; set; }
    }
}
