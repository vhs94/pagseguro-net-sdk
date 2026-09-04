using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    /// <summary>
    /// Criação e consulta de pedidos pagos com boleto.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public interface IBankSlipOrderProvider : IProvider
    {
        /// <summary>
        /// Adiciona uma cobrança ao pedido.
        /// </summary>
        IBankSlipOrderProvider AddCharge(ChargeByBankSlipRequest chargeRequest);
        /// <summary>
        /// Adiciona várias cobranças ao pedido.
        /// </summary>
        IBankSlipOrderProvider AddCharges(ICollection<ChargeByBankSlipRequest> chargeRequests);
        /// <summary>
        /// Carrega um pedido com cobranças já montado no builder.
        /// </summary>
        IBankSlipOrderProvider Load(ChargedOrderRequest<ChargeByBankSlipRequest> chargedRequest);
        /// <summary>
        /// Carrega um pedido já montado no builder, preservando as
        /// cobranças adicionadas.
        /// </summary>
        IBankSlipOrderProvider Load(OrderRequest orderRequest);
        /// <summary>
        /// Retorna o pedido com as cobranças montado e reinicia o builder.
        /// </summary>
        ChargedOrderRequest<ChargeByBankSlipRequest> Build();
        /// <summary>
        /// Cria o pedido e processa as cobranças associadas em uma única chamada.
        /// Corresponde a POST /orders.
        /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByBankSlipResponse>> CreateAsync();
        /// <summary>
        /// Consulta um pedido a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /orders/{order_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByBankSlipResponse>> GetByIdAsync(string orderId);
        /// <summary>
        /// Paga um pedido criado anteriormente.
        /// Corresponde a POST /orders/{order_id}/pay.
        /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByBankSlipResponse>> PayAsync(string orderId);
    }
}
