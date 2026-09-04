using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    /// <summary>
    /// Builder e operações de uma cobrança paga com cartão de crédito e autenticação 3DS.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public interface ICreditCardWith3DsAuthChargeProvider : IProvider
    {
        /// <summary>
        /// Cobrança em construção no builder.
        /// </summary>
        ChargeByCreditCardWith3DsAuthRequest ChargeRequest { get; set; }

        /// <summary>
        /// Define o meio de pagamento com cartão de crédito e
        /// autenticação 3DS da cobrança.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider AddPaymentMethod(
            CreditCardWith3DsAuthPaymentMethodRequest creditCardWith3DsAuthPaymentMethodRequest);
        /// <summary>
        /// Define o valor da cobrança.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider WithAmount(ChargeAmountRequest chargeAmountRequest);
        /// <summary>
        /// Define a descrição da cobrança. De 1 a 64 caracteres.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider WithDescription(string description);
        /// <summary>
        /// Define o identificador da cobrança usado pelas operações de
        /// captura e cancelamento.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider WithId(string chargeId);
        /// <summary>
        /// Define pares de chave e valor personalizados associados à cobrança.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider WithMetadata(IDictionary<string, string> metadata);
        /// <summary>
        /// Adiciona uma URL de webhook notificada a cada alteração de
        /// status da cobrança.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider WithNotificationUrl(string notificationUrl);
        /// <summary>
        /// Adiciona várias URLs de webhook notificadas a cada alteração
        /// de status da cobrança.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider WithNotificationUrls(ICollection<string> notificationUrls);
        /// <summary>
        /// Define o identificador único atribuído para a cobrança.
        /// De 1 a 64 caracteres.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider WithReferenceId(string referenceId);
        /// <summary>
        /// Carrega uma cobrança já montada no builder, substituindo o conteúdo atual.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider Load(ChargeByCreditCardWith3DsAuthRequest chargeRequest);
        /// <summary>
        /// Retorna a cobrança montada e reinicia o builder.
        /// </summary>
        ChargeByCreditCardWith3DsAuthRequest Build();
        /// <summary>
        /// Cria e processa a cobrança.
        /// Corresponde a POST /charges.
        /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
        /// </summary>
        Task<ChargeByCreditCardWith3DsAuthResponse> ChargeAsync();
        /// <summary>
        /// Consulta uma cobrança a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /charges/{charge_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByCreditCardWith3DsAuthResponse> GetByIdAsync(string chargeId);
        /// <summary>
        /// Devolve o valor pago ao comprador, tanto para desfazer uma
        /// pré-autorização quanto para reembolsar um pagamento capturado.
        /// O valor é informado em centavos e permite reembolso parcial.
        /// Corresponde a POST /charges/{charge_id}/cancel.
        /// <see href="https://developer.pagbank.com.br/reference/cancelar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByCreditCardWith3DsAuthResponse> CancelAsync(int amountValue);
        /// <summary>
        /// Captura uma transação previamente pré-autorizada.
        /// O valor é informado em centavos e permite captura parcial.
        /// Corresponde a POST /charges/{charge_id}/capture.
        /// <see href="https://developer.pagbank.com.br/reference/capturar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByCreditCardWith3DsAuthResponse> CaptureAsync(int amountValue);
    }
}
