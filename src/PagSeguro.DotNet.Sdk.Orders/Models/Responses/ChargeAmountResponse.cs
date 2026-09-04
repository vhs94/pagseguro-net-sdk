using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Valor da cobrança retornado pela API.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public class ChargeAmountResponse : ChargeAmountBase
    {
        /// <summary>
        /// Resumo dos valores da cobrança.
        /// </summary>
        public Summary? Summary { get; set; }
    }
}
