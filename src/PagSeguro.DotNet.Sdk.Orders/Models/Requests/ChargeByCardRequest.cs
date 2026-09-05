using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Dados comuns das cobranças pagas com cartão.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public abstract class ChargeByCardRequest : ChargeByCardBase, IChargeRequest
    {
        /// <summary>
        /// Valor da cobrança.
        /// </summary>
        public ChargeAmountRequest? Amount { get; set; }
    }
}
