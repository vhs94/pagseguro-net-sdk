using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Dados comuns das cobranças pagas com cartão.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public abstract class ChargeByCardRequest : ChargeByCardBase, IChargeRequest
    {
        /// <summary>
        /// Divisão do valor da cobrança entre várias contas PagBank. Só tem
        /// efeito em contas habilitadas como marketplace.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SplitRequest? Splits { get; set; }

        /// <summary>
        /// Valor da cobrança.
        /// </summary>
        public ChargeAmountRequest? Amount { get; set; }
    }
}
