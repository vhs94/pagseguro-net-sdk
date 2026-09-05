using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    /// <summary>
    /// Builder e operações de uma cobrança paga com cartão de débito e autenticação 3DS.
    /// A autenticação 3DS é obrigatória para cartão de débito.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public interface IDebitCardWith3DsAuthChargeProvider
    {
        /// <summary>
        /// Cobrança em construção no builder.
        /// </summary>
        ChargeByDebitCardWith3DsAuthRequest ChargeRequest { get; set; }

        /// <summary>
        /// Define o meio de pagamento com cartão de débito e
        /// autenticação 3DS da cobrança.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider AddPaymentMethod(
            DebitCardWith3DsAuthPaymentMethodRequest debitCardWith3DsAuthPaymentMethodRequest);
        /// <summary>
        /// Define o valor da cobrança.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider WithAmount(ChargeAmountRequest chargeAmountRequest);
        /// <summary>
        /// Define a descrição da cobrança. De 1 a 64 caracteres.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider WithDescription(string description);
        /// <summary>
        /// Define o identificador da cobrança usado pelas operações de
        /// captura e cancelamento.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider WithId(string chargeId);
        /// <summary>
        /// Define pares de chave e valor personalizados associados à cobrança.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider WithMetadata(IDictionary<string, string> metadata);
        /// <summary>
        /// Adiciona uma URL de webhook notificada a cada alteração de
        /// status da cobrança.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider WithNotificationUrl(string notificationUrl);
        /// <summary>
        /// Adiciona várias URLs de webhook notificadas a cada alteração
        /// de status da cobrança.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider WithNotificationUrls(ICollection<string> notificationUrls);
        /// <summary>
        /// Define o identificador único atribuído para a cobrança.
        /// De 1 a 64 caracteres.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider WithReferenceId(string referenceId);
        /// <summary>
        /// Carrega uma cobrança já montada no builder, substituindo o conteúdo atual.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider Load(ChargeByDebitCardWith3DsAuthRequest chargeRequest);
        /// <summary>
        /// Retorna a cobrança montada e reinicia o builder.
        /// </summary>
        ChargeByDebitCardWith3DsAuthRequest Build();
        /// <summary>
        /// Cria e processa a cobrança.
        /// Corresponde a POST /charges.
        /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
        /// </summary>
        Task<ChargeByDebitCardWith3DsAuthResponse> ChargeAsync();
        /// <summary>
        /// Consulta uma cobrança a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /charges/{charge_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByDebitCardWith3DsAuthResponse> GetByIdAsync(string chargeId);
        /// <summary>
        /// Devolve o valor pago ao comprador, tanto para desfazer uma
        /// pré-autorização quanto para reembolsar um pagamento capturado.
        /// O valor é informado em centavos e permite reembolso parcial.
        /// Corresponde a POST /charges/{charge_id}/cancel.
        /// <see href="https://developer.pagbank.com.br/reference/cancelar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByDebitCardWith3DsAuthResponse> CancelAsync(int amountValue);
    }
}
