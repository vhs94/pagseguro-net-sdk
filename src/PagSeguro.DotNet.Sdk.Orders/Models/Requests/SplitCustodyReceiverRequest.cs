using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Recebedor cuja custódia será liberada.
    /// <see href="https://developer.pagbank.com.br/reference/liberar-divisao-de-pagamento-com-custodia">ler documentação</see>
    /// </summary>
    public class SplitCustodyReceiverRequest
    {
        /// <summary>Conta do recebedor que terá o valor liberado.</summary>
        public SplitAccount? Account { get; set; }
    }
}
