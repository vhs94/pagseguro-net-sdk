using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    /// <summary>
    /// Criação e consulta de pedidos pagos com cartão de crédito e autenticação 3DS.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public interface ICreditCardWith3DsAuthOrderProvider
    {
        /// <summary>
        /// Adiciona uma cobrança ao pedido.
        /// </summary>
        ICreditCardWith3DsAuthOrderProvider AddCharge(ChargeByCreditCardWith3DsAuthRequest chargeRequest);
        /// <summary>
        /// Adiciona várias cobranças ao pedido.
        /// </summary>
        ICreditCardWith3DsAuthOrderProvider AddCharges(ICollection<ChargeByCreditCardWith3DsAuthRequest> chargeRequests);
        /// <summary>
        /// Carrega um pedido com cobranças já montado no builder.
        /// </summary>
        ICreditCardWith3DsAuthOrderProvider Load(ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> chargedRequest);
        /// <summary>
        /// Carrega um pedido já montado no builder, preservando as
        /// cobranças adicionadas.
        /// </summary>
        ICreditCardWith3DsAuthOrderProvider Load(OrderRequest orderRequest);
        /// <summary>
        /// Retorna o pedido com as cobranças montado e reinicia o builder.
        /// </summary>
        ChargedOrderRequest<ChargeByCreditCardWith3DsAuthRequest> Build();
        /// <summary>
        /// Cria o pedido e processa as cobranças associadas em uma única chamada.
        /// Corresponde a POST /orders.
        /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>> CreateAsync();
        /// <summary>
        /// Consulta um pedido a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /orders/{order_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>> GetByIdAsync(string orderId);
        /// <summary>
        /// Paga um pedido criado anteriormente.
        /// Corresponde a POST /orders/{order_id}/pay.
        /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByCreditCardWith3DsAuthResponse>> PayAsync(string orderId);
    }
}
