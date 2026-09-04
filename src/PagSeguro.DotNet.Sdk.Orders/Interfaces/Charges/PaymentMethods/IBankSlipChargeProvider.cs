using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods
{
    /// <summary>
    /// Builder e operações de uma cobrança paga com boleto.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public interface IBankSlipChargeProvider : IProvider
    {
        /// <summary>
        /// Cobrança em construção no builder.
        /// </summary>
        ChargeByBankSlipRequest ChargeRequest { get; set; }

        /// <summary>
        /// Define os dados do boleto a ser gerado para a cobrança.
        /// </summary>
        IBankSlipChargeProvider AddBankSlip(BankSlipRequest bankSlipRequest);
        /// <summary>
        /// Define o valor da cobrança.
        /// </summary>
        IBankSlipChargeProvider WithAmount(ChargeAmountRequest chargeAmountRequest);
        /// <summary>
        /// Define a descrição da cobrança. De 1 a 64 caracteres.
        /// </summary>
        IBankSlipChargeProvider WithDescription(string description);
        /// <summary>
        /// Define o identificador da cobrança usado pelas operações de
        /// captura e cancelamento.
        /// </summary>
        IBankSlipChargeProvider WithId(string chargeId);
        /// <summary>
        /// Adiciona uma URL de webhook notificada a cada alteração de
        /// status da cobrança.
        /// </summary>
        IBankSlipChargeProvider WithNotificationUrl(string notificationUrl);
        /// <summary>
        /// Adiciona várias URLs de webhook notificadas a cada alteração
        /// de status da cobrança.
        /// </summary>
        IBankSlipChargeProvider WithNotificationUrls(ICollection<string> notificationUrls);
        /// <summary>
        /// Define o identificador único atribuído para a cobrança.
        /// De 1 a 64 caracteres.
        /// </summary>
        IBankSlipChargeProvider WithReferenceId(string referenceId);
        /// <summary>
        /// Carrega uma cobrança já montada no builder, substituindo o conteúdo atual.
        /// </summary>
        IBankSlipChargeProvider Load(ChargeByBankSlipRequest chargeRequest);
        /// <summary>
        /// Retorna a cobrança montada e reinicia o builder.
        /// </summary>
        ChargeByBankSlipRequest Build();
        /// <summary>
        /// Cria e processa a cobrança.
        /// Corresponde a POST /charges.
        /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
        /// </summary>
        Task<ChargeByBankSlipResponse> ChargeAsync();
        /// <summary>
        /// Consulta uma cobrança a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /charges/{charge_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByBankSlipResponse> GetByIdAsync(string chargeId);
        /// <summary>
        /// Devolve o valor pago ao comprador, tanto para desfazer uma
        /// pré-autorização quanto para reembolsar um pagamento capturado.
        /// O valor é informado em centavos e permite reembolso parcial.
        /// Corresponde a POST /charges/{charge_id}/cancel.
        /// <see href="https://developer.pagbank.com.br/reference/cancelar-pagamento">ler documentação</see>
        /// </summary>
        Task<ChargeByBankSlipResponse> CancelAsync(int amountValue);
    }
}
