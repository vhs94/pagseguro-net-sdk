using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    /// <summary>
    /// Criação e consulta de pedidos pagos com cartão de débito e autenticação 3DS.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-3ds-validacao-externa">ler documentação</see>
    /// </summary>
    public interface IDebitCardWith3DsAuthOrderProvider : IProvider
    {
        /// <summary>
        /// Adiciona uma cobrança ao pedido.
        /// </summary>
        IDebitCardWith3DsAuthOrderProvider AddCharge(ChargeByDebitCardWith3DsAuthRequest chargeRequest);
        /// <summary>
        /// Adiciona várias cobranças ao pedido.
        /// </summary>
        IDebitCardWith3DsAuthOrderProvider AddCharges(ICollection<ChargeByDebitCardWith3DsAuthRequest> chargeRequests);
        /// <summary>
        /// Carrega um pedido com cobranças já montado no builder.
        /// </summary>
        IDebitCardWith3DsAuthOrderProvider Load(ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> chargedRequest);
        /// <summary>
        /// Carrega um pedido já montado no builder, preservando as
        /// cobranças adicionadas.
        /// </summary>
        IDebitCardWith3DsAuthOrderProvider Load(OrderRequest orderRequest);
        /// <summary>
        /// Retorna o pedido com as cobranças montado e reinicia o builder.
        /// </summary>
        ChargedOrderRequest<ChargeByDebitCardWith3DsAuthRequest> Build();
        /// <summary>
        /// Cria o pedido e processa as cobranças associadas em uma única chamada.
        /// Corresponde a POST /orders.
        /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>> CreateAsync();
        /// <summary>
        /// Consulta um pedido a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /orders/{order_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>> GetByIdAsync(string orderId);
        /// <summary>
        /// Paga um pedido criado anteriormente.
        /// Corresponde a POST /orders/{order_id}/pay.
        /// <see href="https://developer.pagbank.com.br/reference/pagar-pedido">ler documentação</see>
        /// </summary>
        Task<ChargedOrderResponse<ChargeByDebitCardWith3DsAuthResponse>> PayAsync(string orderId);
    }
}
