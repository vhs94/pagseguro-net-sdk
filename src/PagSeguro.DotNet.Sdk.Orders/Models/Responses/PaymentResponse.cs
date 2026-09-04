namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Resposta da autorização enviada pelo emissor do meio de pagamento.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Código PagBank que indica o motivo da resposta de autorização.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Mensagem amigável descrevendo o motivo da não aprovação ou autorização.
        /// Segue o Normativo 21 da ABECS.
        /// </summary>
        public string? Message { get; set; }
    }
}
