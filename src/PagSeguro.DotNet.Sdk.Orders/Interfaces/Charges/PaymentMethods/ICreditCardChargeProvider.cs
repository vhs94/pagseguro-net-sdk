using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    /// <summary>
    /// Builder e operações de uma cobrança paga com cartão de crédito.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public interface ICreditCardChargeProvider : IProvider
    {
        /// <summary>
        /// Cobrança em construção no builder.
        /// </summary>
        ChargeByCreditCardRequest ChargeRequest { get; set; }

        /// <summary>
        /// Define o meio de pagamento com cartão de crédito da cobrança.
        /// </summary>
        ICreditCardChargeProvider AddPaymentMethod(CreditCardPaymentMethodRequest creditCardPaymentMethodRequest);
        /// <summary>
        /// Define o valor da cobrança.
        /// </summary>
        ICreditCardChargeProvider WithAmount(ChargeAmountRequest chargeAmountRequest);
        /// <summary>
        /// Define a descrição da cobrança. De 1 a 64 caracteres.
        /// </summary>
        ICreditCardChargeProvider WithDescription(string description);
        /// <summary>
        /// Define o identificador da cobrança usado pelas operações de
        /// captura e cancelamento.
        /// </summary>
        ICreditCardChargeProvider WithId(string chargeId);
        /// <summary>
        /// Define pares de chave e valor personalizados associados à cobrança.
        /// </summary>
        ICreditCardChargeProvider WithMetadata(IDictionary<string, string> metadata);
        /// <summary>
        /// Adiciona uma URL de webhook notificada a cada alteração de
        /// status da cobrança.
        /// </summary>
        ICreditCardChargeProvider WithNotificationUrl(string notificationUrl);
        /// <summary>
        /// Adiciona várias URLs de webhook notificadas a cada alteração
        /// de status da cobrança.
        /// </summary>
        ICreditCardChargeProvider WithNotificationUrls(ICollection<string> notificationUrls);
        /// <summary>
        /// Define o identificador único atribuído para a cobrança.
        /// De 1 a 64 caracteres.
        /// </summary>
        ICreditCardChargeProvider WithReferenceId(string referenceId);
        /// <summary>
        /// Carrega uma cobrança já montada no builder, substituindo o conteúdo atual.
        /// </summary>
        ICreditCardChargeProvider Load(ChargeByCreditCardRequest chargeRequest);
        /// <summary>
        /// Retorna a cobrança montada e reinicia o builder.
        /// </summary>
        ChargeByCreditCardRequest Build();
        /// <summary>
        /// Cria e processa a cobrança.
        /// Corresponde a POST /charges.
        /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
        /// </summary>
        Task<ChargeByCreditCardResponse> ChargeAsync();
        /// <summary>
        /// Consulta uma cobrança a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /charges/{charge_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByCreditCardResponse> GetByIdAsync(string chargeId);
        /// <summary>
        /// Devolve o valor pago ao comprador, tanto para desfazer uma
        /// pré-autorização quanto para reembolsar um pagamento capturado.
        /// O valor é informado em centavos e permite reembolso parcial.
        /// Corresponde a POST /charges/{charge_id}/cancel.
        /// <see href="https://developer.pagbank.com.br/reference/cancelar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByCreditCardResponse> CancelAsync(int amountValue);
        /// <summary>
        /// Captura uma transação previamente pré-autorizada.
        /// O valor é informado em centavos e permite captura parcial.
        /// Corresponde a POST /charges/{charge_id}/capture.
        /// <see href="https://developer.pagbank.com.br/reference/capturar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByCreditCardResponse> CaptureAsync(int amountValue);
    }
}
