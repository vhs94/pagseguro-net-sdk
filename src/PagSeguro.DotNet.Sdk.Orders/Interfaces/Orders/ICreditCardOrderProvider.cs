using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    /// <summary>
    /// Criação e consulta de pedidos pagos com cartão de crédito.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public interface ICreditCardOrderProvider
    {
        /// <summary>
        /// Adiciona uma cobrança ao pedido.
        /// </summary>
        ICreditCardOrderProvider AddCharge(ChargeByCreditCardRequest chargeRequest);
        /// <summary>
        /// Adiciona várias cobranças ao pedido.
        /// </summary>
        ICreditCardOrderProvider AddCharges(ICollection<ChargeByCreditCardRequest> chargeRequests);
        /// <summary>
        /// Carrega um pedido com cobranças já montado no builder.
        /// </summary>
        ICreditCardOrderProvider Load(ChargedOrderRequest<ChargeByCreditCardRequest> chargedRequest);
        /// <summary>
        /// Carrega um pedido já montado no builder, preservando as
        /// cobranças adicionadas.
        /// </summary>
        ICreditCardOrderProvider Load(OrderRequest orderRequest);
        /// <summary>
        /// Retorna o pedido com as cobranças montado e reinicia o builder.
        /// </summary>
        ChargedOrderRequest<ChargeByCreditCardRequest> Build();
        /// <summary>
        /// Cria o pedido e processa as cobranças associadas em uma única chamada.
        /// Corresponde a POST /orders.
        /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByCreditCardResponse>> CreateAsync();
        /// <summary>
        /// Consulta um pedido a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /orders/{order_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByCreditCardResponse>> GetByIdAsync(string orderId);
        /// <summary>
        /// Paga um pedido criado anteriormente.
        /// Corresponde a POST /orders/{order_id}/pay.
        /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByCreditCardResponse>> PayAsync(string orderId);
    }
}
