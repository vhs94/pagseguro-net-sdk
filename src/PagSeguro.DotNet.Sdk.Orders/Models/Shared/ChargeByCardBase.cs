namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns de uma cobrança paga com cartão.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public abstract class ChargeByCardBase : ChargeBase
    {
        /// <summary>
        /// Pares de chave e valor personalizados, associados à cobrança.
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }

        public ChargeByCardBase() => Metadata = new Dictionary<string, string>();
    }
}
