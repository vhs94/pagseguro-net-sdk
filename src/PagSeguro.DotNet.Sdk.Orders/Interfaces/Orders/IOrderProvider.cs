using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders
{
    /// <summary>
    /// Builder e operações de um pedido, incluindo a escolha do meio de pagamento
    /// usado para pagá-lo.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public interface IOrderProvider : IProvider
    {
        /// <summary>
        /// Define as informações do cliente que está realizando o pedido.
        /// </summary>
        IOrderProvider WithCustomer(Customer customer);
        /// <summary>
        /// Adiciona um item ao pedido.
        /// </summary>
        IOrderProvider WithItem(ItemRequest itemRequest);
        /// <summary>
        /// Adiciona vários itens ao pedido.
        /// </summary>
        IOrderProvider WithItems(ICollection<ItemRequest> itemRequests);
        /// <summary>
        /// Adiciona uma URL de webhook notificada a cada alteração
        /// de status das cobranças do pedido.
        /// </summary>
        IOrderProvider WithNotificationUrl(string notificationUrl);
        /// <summary>
        /// Adiciona várias URLs de webhook notificadas a cada alteração
        /// de status das cobranças do pedido.
        /// </summary>
        IOrderProvider WithNotificationUrls(ICollection<string> notificationUrls);
        /// <summary>
        /// Adiciona um QR Code Pix ao pedido.
        /// </summary>
        IOrderProvider WithQrCode(QrCodeRequest qrCodeRequest);
        /// <summary>
        /// Adiciona vários QR Codes Pix ao pedido.
        /// </summary>
        IOrderProvider WithQrCodes(ICollection<QrCodeRequest> qrCodeRequests);
        /// <summary>
        /// Define o identificador único atribuído para o pedido.
        /// De 1 a 64 caracteres.
        /// </summary>
        IOrderProvider WithReferenceId(string referenceId);
        /// <summary>
        /// Define as informações de entrega do pedido.
        /// </summary>
        IOrderProvider WithShipping(Shipping shipping);
        /// <summary>
        /// Carrega um pedido já montado no builder, substituindo o conteúdo atual.
        /// </summary>
        IOrderProvider Load(OrderRequest orderRequest);
        /// <summary>
        /// Retorna o pedido montado e reinicia o builder.
        /// </summary>
        OrderRequest Build();
        /// <summary>
        /// Continua a montagem do pedido para pagamento com boleto.
        /// </summary>
        IBankSlipOrderProvider WithBankSlip();
        /// <summary>
        /// Continua a montagem do pedido para pagamento com cartão de crédito.
        /// </summary>
        ICreditCardOrderProvider WithCreditCard();
        /// <summary>
        /// Continua a montagem do pedido para pagamento com
        /// cartão de crédito e autenticação 3DS.
        /// </summary>
        ICreditCardWith3DsAuthOrderProvider WithCreditCardAnd3DsAuthentication();
        /// <summary>
        /// Continua a montagem do pedido para pagamento com
        /// cartão de débito e autenticação 3DS.
        /// </summary>
        IDebitCardWith3DsAuthOrderProvider WithDebitCardAnd3DsAuthentication();
        /// <summary>
        /// Cria o pedido sem processar nenhuma cobrança.
        /// Corresponde a POST /orders.
        /// <see href="https://developer.pagbank.com.br/reference/criar-pedido">ler documentação</see>
        /// </summary>
        Task<OrderResponse> CreateAsync();
        /// <summary>
        /// Consulta um pedido a partir do identificador fornecido pelo PagBank.
        /// Corresponde a GET /orders/{order_id}.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-pedido">ler documentação</see>
        /// </summary>
        Task<OrderResponse> GetByIdAsync(string orderId);
    }
}
