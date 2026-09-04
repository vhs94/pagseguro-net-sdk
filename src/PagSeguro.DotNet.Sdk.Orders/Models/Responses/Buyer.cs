namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Encargos repassados ao comprador no parcelamento.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class Buyer
    {
        /// <summary>
        /// Juros cobrados do comprador.
        /// </summary>
        public Interest? Interest { get; set; }
    }
}
