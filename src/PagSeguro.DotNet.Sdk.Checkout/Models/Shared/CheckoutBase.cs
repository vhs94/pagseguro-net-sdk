using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Checkout.Models.Shared
{
    /// <summary>
    /// Dados comuns de um checkout, compartilhados entre a criação e a consulta.
    /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
    /// </summary>
    public abstract class CheckoutBase
    {
        /// <summary>
        /// Identificador único atribuído para o checkout.
        /// </summary>
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }

        /// <summary>
        /// Data de expiração do checkout.
        /// </summary>
        [JsonPropertyName("expiration_date")]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Define se o comprador pode alterar os próprios dados na página de checkout.
        /// Quando false, o objeto customer passa a ser obrigatório.
        /// </summary>
        [JsonPropertyName("customer_modifiable")]
        public bool CustomerModifiable { get; set; }

        /// <summary>
        /// Dados do cliente pré-preenchidos na página de checkout.
        /// </summary>
        public CheckoutCustomer? Customer { get; set; }

        /// <summary>
        /// Itens exibidos na página de checkout.
        /// </summary>
        public ICollection<CheckoutItem> Items { get; set; }

        /// <summary>
        /// Valor adicional cobrado no checkout, em centavos.
        /// </summary>
        [JsonPropertyName("additional_amount")]
        public int AdditionalAmount { get; set; }

        /// <summary>
        /// Valor de desconto aplicado no checkout, em centavos.
        /// </summary>
        [JsonPropertyName("discount_amount")]
        public int DiscountAmount { get; set; }

        /// <summary>
        /// Meios de pagamento habilitados no checkout.
        /// </summary>
        [JsonPropertyName("payment_methods")]
        public ICollection<CheckoutPaymentMethod> PaymentMethods { get; set; }

        /// <summary>
        /// Nome exibido na fatura do cliente.
        /// </summary>
        [JsonPropertyName("soft_descriptor")]
        public string? SoftDescriptor { get; set; }

        /// <summary>
        /// URL para a qual o comprador é redirecionado após o pagamento.
        /// </summary>
        [JsonPropertyName("redirect_url")]
        public string? RedirectUrl { get; set; }

        /// <summary>
        /// URL do botão de retorno exibido na página de checkout.
        /// </summary>
        [JsonPropertyName("return_url")]
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// URLs de webhook notificadas a cada alteração de status do checkout.
        /// </summary>
        [JsonPropertyName("notification_urls")]
        public ICollection<string> NotificationUrls { get; set; }

        /// <summary>
        /// URLs de webhook notificadas a cada alteração de status do pagamento.
        /// </summary>
        [JsonPropertyName("payment_notification_urls")]
        public ICollection<string> PaymentNotificationUrls { get; set; }

        /// <summary>
        /// Inicializa as coleções do checkout.
        /// </summary>
        protected CheckoutBase()
        {
            Items = [];
            PaymentMethods = [];
            NotificationUrls = [];
            PaymentNotificationUrls = [];
        }
    }
}
