namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Resposta da autorização para pagamentos com cartão.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public class CardPaymentResponse : PaymentResponse
    {
        /// <summary>
        /// NSU da autorização, caso o pagamento seja aprovado pelo emissor.
        /// </summary>
        public string? Reference { get; set; }
    }
}
