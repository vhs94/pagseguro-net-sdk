using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Resposta do adquirente que processou o pagamento.</summary>
    public class PaymentProvider
    {
        /// <summary>Nome do adquirente.</summary>
        public string? Name { get; set; }

        /// <summary>Identificador da transação no adquirente.</summary>
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        /// <summary>Código de retorno do adquirente.</summary>
        public string? Code { get; set; }

        /// <summary>Mensagem de retorno do adquirente.</summary>
        public string? Message { get; set; }
    }
}
