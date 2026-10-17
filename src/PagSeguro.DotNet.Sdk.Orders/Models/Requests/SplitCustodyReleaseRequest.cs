namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Dados enviados para liberar valores retidos em custódia.
    /// <see href="https://developer.pagbank.com.br/reference/liberar-divisao-de-pagamento-com-custodia">ler documentação</see>
    /// </summary>
    public class SplitCustodyReleaseRequest
    {
        /// <summary>Recebedores que terão a custódia liberada.</summary>
        public ICollection<SplitCustodyReceiverRequest> Receivers { get; set; } = [];
    }
}
