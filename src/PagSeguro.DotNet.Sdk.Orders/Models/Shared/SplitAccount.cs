namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Conta PagBank que participa da divisão do pagamento.
    /// <see href="https://developer.pagbank.com.br/docs/config-split">ler documentação</see>
    /// </summary>
    public class SplitAccount
    {
        /// <summary>
        /// Identificador da conta recebedora. Tem 41 caracteres, no formato
        /// ACCO_XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX.
        /// </summary>
        public string? Id { get; set; }
    }
}
