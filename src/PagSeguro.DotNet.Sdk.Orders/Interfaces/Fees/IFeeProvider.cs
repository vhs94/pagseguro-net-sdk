using Flurl;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Helpers;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees
{
    /// <summary>
    /// Simulação das taxas de venda e dos planos de parcelamento com repasse de juros.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public interface IFeeProvider : IProvider
    {
        /// <summary>
        /// Parâmetros da simulação em construção no builder.
        /// </summary>
        FeeRequest Entity { get; set; }

        /// <summary>
        /// Reinicia o builder, descartando os parâmetros já informados.
        /// </summary>
        void Reset();

        /// <summary>
        /// Define os seis primeiros dígitos do cartão (BIN).
        /// </summary>
        IFeeProvider WithCreditCardBin(int creditCardBin);
        /// <summary>
        /// Define a quantidade máxima de parcelas permitidas.
        /// </summary>
        IFeeProvider WithMaxInstallments(int maxInstallments);
        /// <summary>
        /// Define a quantidade de parcelas sem juros
        /// custeadas pelo vendedor.
        /// </summary>
        IFeeProvider WithMaxInstallmentsNoInterest(int maxInstallmentsNoInterest);
        /// <summary>
        /// Define o valor original da transação, em centavos.
        /// </summary>
        IFeeProvider WithValue(int amountValue);
        /// <summary>
        /// Carrega parâmetros já montados no builder, substituindo o conteúdo atual.
        /// </summary>
        IFeeProvider Load(FeeRequest entity);
        /// <summary>
        /// Retorna os parâmetros montados e reinicia o builder.
        /// </summary>
        FeeRequest Build();
        /// <summary>
        /// Consulta as taxas de venda das transações e exibe o repasse dos juros.
        /// Corresponde a GET /charges/fees/calculate.
        /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
        /// </summary>
        Task<FeeResponse> CalculateAsync();
    }
}
