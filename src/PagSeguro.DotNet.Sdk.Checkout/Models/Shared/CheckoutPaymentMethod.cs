using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Checkout.Models.Shared
{
    /// <summary>
    /// Meio de pagamento habilitado no checkout.
    /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
    /// </summary>
    public class CheckoutPaymentMethod
    {
        /// <summary>
        /// Construtor sem parâmetros, usado na desserialização da resposta.
        /// </summary>
        public CheckoutPaymentMethod()
        {
        }

        /// <summary>
        /// Cria o meio de pagamento a partir do tipo desejado.
        /// </summary>
        /// <param name="type">Meio de pagamento a ser habilitado.</param>
        public CheckoutPaymentMethod(CheckoutPaymentMethodType type)
            => Type = type.ToDescription();

        /// <summary>
        /// Tipo do meio de pagamento.
        /// Valores possíveis: CREDIT_CARD, DEBIT_CARD, BOLETO e PIX.
        /// </summary>
        public string? Type { get; set; }
    }
}
